using NUnit.Framework;
using Specify.Stories;
using Specs.Library.ApiSample.Data;
using Specs.Library.ApiSample.Helpers;

namespace Specs.Component.ApiSample
{
    [TestFixture]
    public abstract class ScenarioFor<TSut, TStory> : Specify.ScenarioFor<TSut, TStory>
        where TSut : class
        where TStory : Story, new()
    {
         protected IDb Db => Container.Get<IDb>();

        [ExecuteScenario]
        public override void Specify()
        {
            base.Specify();
        }
    }
}