using System;
using System.Threading.Tasks;
using FluentAssertions;
using LazyCache;
using ApiSample.Domain.Model;
using ApiSample.Infrastructure.Caching;
using Specs.Library.ApiSample.Builders.Entities;

namespace Specs.Integration.ApiSample.Infrastructure.Caching
{
    public class ReturnsNewItemIfAddToCacheSucceeds : ScenarioFor<ICache>
    {
        private DateRange _cachedItem = new DateRangeBuilder();
        private string _key = "key";
        private DateRange _result;
        private Func<Task<DateRange>> _factory;
        private DateRange DateRange = new DateRange(new DateTime(2019, 2, 3));

        public void Given_there_is_no_item_in_the_cache_and_the_retrieval_of_the_new_item_succeeds()
        {
            _factory = () => Task.Run(() => DateRange);
        }

        public async Task When_the_item_is_requested_from_the_cache()
        {
            _result = await SUT.GetOrAddAsync<DateRange>(_key, _factory);
        }

        public void Then_the_new_item_is_returned()
        {
            _result.Should().Be(DateRange);
        }

        public void TearDown()
        {
            The<IAppCache>().Remove(_key);
        }
    }

    public class ReturnsExistingCachedItemIfAddToCacheFails :ScenarioFor<ICache>
    {
        private DateRange _cachedItem = new DateRangeBuilder();
        private string _key = "key";
        private DateRange _result;
        private Func<Task<DateRange>> _factory;

        public void Given_there_is_an_existing_item_in_the_cache()
        {
            The<IAppCache>().Add(_key, _cachedItem);
        }

        public void AndGiven_the_retrieval_of_new_item_fails()
        {
            _factory = () => Task.Run(() => GetDateRange());
        }

        public async Task When_the_item_is_requested_from_the_cache()
        {
            _result = await SUT.GetOrAddAsync<DateRange>(_key, _factory);
        }

        public void Then_the_existing_item_is_returned()
        {
            _result.Should().Be(_cachedItem);
        }

        public void TearDown()
        {
            The<IAppCache>().Remove(_key);
        }

        private DateRange GetDateRange()
        {
            throw new Exception();
        }
    }

    public class ReturnsNullIfNoExistingCacheItemAndAddToCacheFails : ScenarioFor<ICache>
    {
        private string _key = "key";
        private DateRange _result;
        private Func<Task<DateRange>> _factory;

        public void Given_there_i_no_existing_item_in_Cache_and_the_retrieval_of_new_item_fails()
        {
            _factory = () => Task.Run(() => GetDateRange());
        }

        public async Task When_the_item_is_requested_from_the_cache()
        {
            _result = await SUT.GetOrAddAsync<DateRange>(_key, _factory);
        }

        public void Then_null_is_returned()
        {
            _result.Should().BeNull();
        }

        public void TearDown()
        {
            The<IAppCache>().Remove(_key);
        }

        private DateRange GetDateRange()
        {
            throw new Exception();
        }
    }
}