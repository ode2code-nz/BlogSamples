using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.CI.AzurePipelines;
using Nuke.Common.Tools.Docker;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using Nuke.Common.Tooling;

[CheckBuildProjectConfigurations]
[ShutdownDotNetAfterServerBuild]
[GitHubActions("gh-ci",
    GitHubActionsImage.UbuntuLatest,
    AutoGenerate = true,
    OnPushBranches = new[] { "main", "feature/*" },
    OnPullRequestBranches = new[] { "feature/*" },
    InvokedTargets = new[] { nameof(Test) }
    )]

[AzurePipelines("az-ci",
    AzurePipelinesImage.UbuntuLatest,
    AutoGenerate = true,
    TriggerBranchesInclude = new[] { "main", "feature/*" },
    InvokedTargets = new[] { nameof(Test)},
    NonEntryTargets = new[]
    {
        nameof(Restore),
        nameof(Compile),
        nameof(CreateDockerSql),
        nameof(RemoveDockerSql),

        nameof(IntegrationTest),
        nameof(UnitTest),
        nameof(AcceptanceTest)


    },
    ExcludedTargets = new[] { nameof(Pack),nameof(Clean) }
    )]

//
class Build : NukeBuild
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    public static int Main() => Execute<Build>(x => x.Test);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution] readonly Solution Solution;
    [GitRepository] readonly GitRepository GitRepository;

    AbsolutePath SourceDirectory => RootDirectory / "src/app";
    AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";

    private readonly string DockerImage= "mcr.microsoft.com/mssql/server:2019-latest";
    private readonly string Password= "Password_01";
    private readonly string PortNumber= "1433";
    private readonly string ImageName= "sql1";
    private readonly string ImageHostName = "sql1";

    private readonly string DbServer = "localhost";
    private readonly string DatabaseName = "ApiSampleDb-Test";
    private readonly string UserId = "sa";
    

    private string ConnectionString => 
        $"Server={DbServer},{PortNumber};Database={DatabaseName};User Id = {UserId};Password={Password};" +
        $"MultipleActiveResultSets=True;Trusted_Connection=False;Persist Security Info=true";


Target Clean => _ => _
        .Executes(() =>
        {
            SourceDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
            EnsureCleanDirectory(ArtifactsDirectory);
        });

    Target Restore => _ => _
        .DependsOn(Clean)
        .Executes(() =>
        {
            DotNetRestore(s => s
                .SetProjectFile(Solution));
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .EnableNoRestore()
                );
        });

    Target CreateDockerSql => _ => _
        .After(Compile)
        .Executes(() =>
        {
            DockerTasks.DockerRun(x =>
            x.SetImage(DockerImage)
               .SetEnv(new string[] { "ACCEPT_EULA=Y", $"SA_PASSWORD={Password}" })
               .SetPublish($"{PortNumber}:1433")
               .SetDetach(true)
               .SetName($"{ImageName}")
               .SetHostname($"{ImageHostName}")
               .AddCapAdd("SYS_PTRACE"));
        });

    Target Test => _ => _
        .Triggers(UnitTest, IntegrationTest, AcceptanceTest, RemoveDockerSql);

    Target UnitTest => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            DotNetTest(_ => _
                .SetConfiguration(Configuration)
                .SetNoBuild(InvokedTargets.Contains(Compile))
                .ResetVerbosity()
                .SetProjectFile(Solution.GetProject("Unit")));
        });

    Target IntegrationTest => _ => _
        .After(UnitTest)
        .DependsOn(CreateDockerSql)
        .Executes(() =>
        {
            DotNetTest(_ => _
                .SetConfiguration(Configuration)
                .SetProcessEnvironmentVariable("ConnectionStrings:AppDB", ConnectionString)
                .SetNoBuild(InvokedTargets.Contains(Compile))
                .ResetVerbosity()
                .SetProjectFile(Solution.GetProject("Integration")));
        });

    Target AcceptanceTest => _ => _
        .After(IntegrationTest)
        .DependsOn(CreateDockerSql)
        .Executes(() =>
        {
            DotNetTest(_ => _
                .SetConfiguration(Configuration)
                .SetNoBuild(InvokedTargets.Contains(Compile))
                .SetProcessEnvironmentVariable("ConnectionStrings:AppDB", ConnectionString)
                .ResetVerbosity()
                .SetProjectFile(Solution.GetProject("Component")));
        });


    Target RemoveDockerSql => _ => _
        .After(AcceptanceTest,IntegrationTest, CreateDockerSql)
            .Executes(() =>
            {
                DockerTasks.Docker("stop sql1");
                DockerTasks.Docker("container rm sql1");
            });
    Target Pack => _ => _
        .DependsOn(Test)
        .Executes(() =>
        {
            DotNetPack(s => s
                .SetProject(Solution)
                .SetConfiguration(Configuration)
                .SetIncludeSymbols(true)
                .SetOutputDirectory(ArtifactsDirectory)
                .SetNoBuild(InvokedTargets.Contains(Compile))
                .EnableNoRestore());
        });
}

