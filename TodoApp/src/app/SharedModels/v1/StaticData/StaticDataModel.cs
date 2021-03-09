namespace Todo.SharedModels.v1.StaticData
{
    public class StaticDataModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
    }

    public class CompanyModel : StaticDataModel { public int CounterpartyId => Id; }

    public class LocationModel : StaticDataModel {}

    public class CounterpartyModel : StaticDataModel { public int CounterpartyId => Id; }
}
