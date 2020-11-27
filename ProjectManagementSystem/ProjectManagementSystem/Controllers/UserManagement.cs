using AutoMapper;
using ProjectManagementSystem.Repository;
using ProjectManagementSystem.Views.ViewModels.UserModels;
using System.Linq;

namespace ProjectManagementSystem.Controllers
{
    public class UserManagement
    {
        public static User GetCurrentUser(IRepository repository, IMapper mapper, string email)
        {
            var dbUser = repository.Users.Search(user => user.Email == email).Single();
            return Mapper.Map<User>(dbUser);
        }
    }
}
