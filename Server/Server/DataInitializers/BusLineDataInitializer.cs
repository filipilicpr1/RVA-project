using Server.Interfaces.DataInitializerInterfaces;
using Server.Interfaces.UnitOfWorkInterfaces;
using Server.Models;

namespace Server.DataInitializers
{
    public class BusLineDataInitializer : IBusLineDataInitializer
    {
        private readonly IUnitOfWork _unitOfWork;
        public BusLineDataInitializer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void InitializeBusLineData()
        {
            List<BusLine> busLines = _unitOfWork.BusLines.GetAllSync();
            if(busLines.Count != 0 && busLines.Find(b => String.Equals(b.Label, "806E")) != null)
            {
                return;
            }
            BusLine busLine = new BusLine() { Label = "806E", BusLineType = Enums.EBusLineType.GRADSKA };
            _unitOfWork.BusLines.Add(busLine);
            busLine.Cities.Add(_unitOfWork.Cities.FindByNameSync("Beograd"));
            _unitOfWork.SaveSync();
        }
    }
}
