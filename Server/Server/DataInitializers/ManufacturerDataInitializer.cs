using Server.Interfaces.DataInitializerInterfaces;
using Server.Interfaces.UnitOfWorkInterfaces;
using Server.Models;

namespace Server.DataInitializers
{
    public class ManufacturerDataInitializer : IManufacturerDataInitializer
    {
        private readonly IUnitOfWork _unitOfWork;
        private List<string> manufacturerNames = new List<string>()
        {
            "Ikarbus","Mercedes-Benz","Volvo", "Iveco", "Renault", "Peugeot", "Citroen"
        };
        public ManufacturerDataInitializer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void InitializeManufacturerData()
        {
            List<Manufacturer> manufacturers = _unitOfWork.Manufacturers.GetAllSync();
            if(manufacturers.Count == 0)
            {
                _unitOfWork.Manufacturers.AddRangeSync(manufacturerNames.ConvertAll(m => new Manufacturer() { Name = m }));
                _unitOfWork.SaveSync();
            }
            if(manufacturers.Find(m => String.Equals(m.Name, "Volvo")) == null)
            {
                _unitOfWork.Manufacturers.AddSync(new Manufacturer() { Name = "Volvo" });
                _unitOfWork.SaveSync();
            }
        }
    }
}
