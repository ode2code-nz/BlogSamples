using DryIoc;
using Specify.Configuration;
using TestStack.BDDfy.Configuration;

namespace Specs.Unit.Todo
{
    /// <summary>
    /// The startup class to configure Specify with the default DryIoc container. 
    /// Make any changes to the default configuration settings in this file.
    /// </summary>
    public class UnitBootstrapper : DefaultBootstrapper
    {
        public UnitBootstrapper()
        {
            HtmlReport.ReportHeader = "Todo";
            HtmlReport.ReportDescription = "Unit Specs";
            HtmlReport.OutputFileName = "Todo-UnitSpecs.html";
            Configurator.BatchProcessors.HtmlReport.Disable();
        }

        /// <summary>
        /// Register any additional items into the DryIoc container or leave it as it is. 
        /// </summary>
        /// <param name="container">The <see cref="DryIoc"/> container.</param>
        public override void ConfigureContainer(Container container)
        {
            // container.Register<IPerScenarioAction, LoggingAction>();
        }
    }
}
