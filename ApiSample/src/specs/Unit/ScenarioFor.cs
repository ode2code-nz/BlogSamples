using NUnit.Framework;
using Specs.Library.ApiSample.Helpers;

namespace Specs.Unit.ApiSample
{
    /// <summary>
    /// The base class for scenarios without a story (normally unit test scenarios).
    /// </summary>
    /// <typeparam name="TSut">The type of the t sut.</typeparam>
    [TestFixture]
    public abstract class ScenarioFor<TSut> : Specify.ScenarioFor<TSut>
        where TSut : class
    {
        [ExecuteScenario]
        public override void Specify()
        {
            base.Specify();
        }
    }
}