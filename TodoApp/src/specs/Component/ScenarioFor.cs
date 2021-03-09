using NUnit.Framework;
using Specify.Stories;
using Specs.Library.Todo.Data;
using Specs.Library.Todo.Helpers;

namespace Specs.Component.Todo
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