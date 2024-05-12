using Warsztat.Data.Entity;

namespace Warsztat.DTO.Response
{
    public class CarsResponse
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Registration { get; set; }
        public DateTime? ProductionYear { get; set; }
        public double? Capacity { get; set; }
        public bool Active { get; set; }
        public Clients Owner { get; set; }
        public string DictionaryType_Id { get; set; }
        public string DictionaryEngine_Id { get; set; }
        public string DictionaryStatus_Id { get; set; }
        public int[] dictIds { get; set; }
        public CarsResponse(Cars car, Clients client, int typeId, int engineId, int statusId)
        {
            Id = car.Id;
            Brand = car.Brand;
            Model = car.Model;
            Registration = car.Registration;
            ProductionYear = car.ProductionYear;
            Capacity = car.Capacity;
            Active = car.Active;
            Owner = client;
            dictIds = new int[] { typeId, engineId, statusId };
        }
        public CarsResponse(Cars car, Clients client, Dictionary type, Dictionary engine, Dictionary status)
        {
            Id = car.Id;
            Brand = car.Brand;
            Model = car.Model;
            Registration = car.Registration;
            ProductionYear = car.ProductionYear;
            Capacity = car.Capacity;
            Active = car.Active;
            Owner = client;
            DictionaryType_Id = type.Value;
            DictionaryEngine_Id = engine.Value;
            DictionaryStatus_Id = status.Value;
        }
    }
}
