using AutoFixture;
using Specs.Library.ToDo.Builders.ObjectMothers;
using TestStack.Dossier;

namespace Specs.Library.ToDo.Builders
{
    public static class Get
    {
        public static Builder<T> BuilderFor<T>() where T : class
        {
            return Builder<T>.CreateNew();
        }

        public static T InstanceOf<T>() where T : class
        {
            return Builder<T>.CreateNew().Build();
        }

        public static Stubs StubFor { get; } = new Stubs();

        public static AnonymousValueFixture Any { get; } = new AnonymousValueFixture();

        public static T AutoFixtureValueFor<T>()
        {
            return new AnonymousValueFixture().Fixture.Create<T>();
        }

        public static SequentialMother SequenceOf { get; } = new SequentialMother();

        public static StaticDataMother StaticData { get; } = new StaticDataMother();

        public static Users User { get; } = new Users();
    }
}