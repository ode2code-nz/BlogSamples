﻿using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Todo.Api.Features;
using Todo.Domain.Model.MasterTradingAgreements;
using Specs.Library.Todo.Builders.Entities;
using Specs.Library.Todo.Data;
using Specs.Library.Todo.Drivers.Api;

namespace Specs.Component.Todo.Features.MasterTradingAgreements.Delete
{
    public class ValidMasterTradingAgreementDelete : ScenarioFor<AsyncApiDriver, MasterTradingAgreementStory>
    {
        private ApiResponse _result;
        private MasterTradingAgreement _existingItem;

        public void Given_I_have_an_existing_MasterTradingAgreement()
        {
            _existingItem = new MasterTradingAgreementBuilder().Persist();
        }

        public async Task When_I_attempt_to_delete_it()
        {
            _result = await SUT.DeleteWithCheckAsync(ApiRoutes.MasterTradingAgreement.DeleteFor(_existingItem.Id));
        }

        public void Then_the_MasterTradingAgreement_should_be_deleted()
        {
            Db.Set<MasterTradingAgreement>().Should().HaveCount(0);
            _result.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }
    }
}