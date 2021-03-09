using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FastMember;
using ApiSample.Api.Common;
using ApiSample.Api.Common.Mappings;
using ApiSample.Api.Features.v1.MasterTradingAgreements;
using Specs.Library.ApiSample.Builders.Entities;

namespace Specs.Unit.ApiSample.Application.Common.Mappings
{
    public class AutoMapperSpecs : ScenarioFor<IMapper>
    {
        List<Type> _mapFromTypes;
        private List<Type> _mapToAndFromTypes;

        public void Given_the_classes_that_need_to_be_mapped()
        {
            _mapFromTypes = AssemblyScanner.AllTypesThatImplementInterface(typeof(IMapFrom<>));
            _mapToAndFromTypes = AssemblyScanner.AllTypesThatImplementInterface(typeof(IMapToAndFrom<>));
        }

        public void When_AutoMapper_is_configured()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.Advanced.AllowAdditiveTypeMapCreation = true;
                cfg.AddProfile<MappingProfile>();
            });

            SUT = configurationProvider.CreateMapper();

        }

        public void Then_the_configuration_is_valid()
        {
            SUT.ConfigurationProvider.AssertConfigurationIsValid();
        }

        public void AndThen_AutoMapper_knows_how_to_map_the_nonEntity_source_to_destination_for_From_Types()
        {
            foreach (var destination in _mapFromTypes)
            {
                var source = destination.GetInterfaces()
                    .Single(x => x.Name == "IMapFrom`1")
                    .GetGenericArguments()[0];

                // Entities cannot be created using FastMember CreateNew so are skipped
                if (TypeAccessor.Create(source).CreateNewSupported)
                {
                    var instance = TypeAccessor.Create(source).CreateNew();
                    SUT.Map(instance, source, destination);
                }
            }
        }

        public void AndThen_AutoMapper_knows_how_to_map_the_Entity_source_to_destination_for_From_Types()
        {
            SUT.Map<MasterTradingAgreementDto>(new MasterTradingAgreementBuilder().Build());
            var contractSchedule = new ContractScheduleBuilder().Build();
            SUT.Map<ContractScheduleDto>(contractSchedule);
        }

        public void AndThen_AutoMapper_knows_how_to_map_the_source_to_destination_for_ToAndFrom_Types()
        {
            foreach (var destination in _mapToAndFromTypes)
            {
                var interfaces = destination.GetInterfaces().Single(x => x.Name == "IMapToAndFrom`1");
                var source = destination.GetInterfaces()
                    .Single(x => x.Name == "IMapToAndFrom`1")
                    .GetGenericArguments()[0];

                var instance = TypeAccessor.Create(source).CreateNew();

                SUT.Map(instance, source, destination);
            }
        }

        public void AndThen_AutoMapper_knows_how_to_map_the_destination_to_source_ToAndFrom_Types()
        {
            foreach (var destination in _mapToAndFromTypes)
            {
                var source = destination.GetInterfaces()
                    .Single(x => x.Name == "IMapToAndFrom`1")
                    .GetGenericArguments()[0];

                var instance = TypeAccessor.Create(destination).CreateNew();

                SUT.Map(instance, destination, source);
            }
        }
    }
}