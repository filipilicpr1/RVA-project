using Server.Interfaces.DataInitializerInterfaces;
using Server.Interfaces.UnitOfWorkInterfaces;
using Server.Models;

namespace Server.DataInitializers
{
    public class BusDataInitializer : IBusDataInitializer
    {
        private readonly IUnitOfWork _unitOfWork;
        public BusDataInitializer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void InitializeBusData()
        {
            List<Bus> buses = _unitOfWork.Buses.GetAllSync();
            if(buses.Count != 0 && buses.Find(b => String.Equals(b.Name, "Autobus 2")) != null)
            {
                return;
            }
            Bus bus = new Bus() { Name = "Autobus 2", Label = "M2 III" };
            _unitOfWork.Buses.Add(bus);
            bus.Manufacturer = _unitOfWork.Manufacturers.FindByNameSync("Volvo");
            bus.BusLine = _unitOfWork.BusLines.FindByLabelSync("806E");
            _unitOfWork.SaveSync();
        }
    }
}
