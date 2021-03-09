using NUnit.Framework;
using Specify.Stories;
using Specs.Library.ToDo.Data;
using Specs.Library.ToDo.Helpers;

namespace Specs.Component.ToDo
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