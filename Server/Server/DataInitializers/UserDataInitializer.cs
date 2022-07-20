using Server.Interfaces.DataInitializerInterfaces;
using Server.Interfaces.UnitOfWorkInterfaces;
using Server.Models;

namespace Server.DataInitializers
{
    public class UserDataInitializer : IUserDataInitializer
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserDataInitializer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void InitializeUserData()
        {
            List<User> users = _unitOfWork.Users.GetAllSync();
            if (users.Count != 0 && users.Find(u => String.Equals(u.Username,"admin")) != null)
            {
                return;
            }
            _unitOfWork.Users.AddSync(new User()
            {
                Name = "Admin",
                LastName = "Admin",
                Username = "admin",
                Password = BCrypt.Net.BCrypt.HashPassword("admin"),
                UserType = Enums.EUserType.ADMIN
            });
            _unitOfWork.SaveSync();   
        }
    }
}
