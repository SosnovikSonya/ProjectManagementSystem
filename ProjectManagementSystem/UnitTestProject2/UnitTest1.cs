using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectManagementSystem.Models.Interfaces;
using ViewModels = ProjectManagementSystem.Views.ViewModels.UserModels;
using Implementations = ProjectManagementSystem.Models.Implementations;
using ProjectManagementSystem.Models.Implementations;
using AutoMapper;
using ProjectManagementSystem.Mapping;
using ProjectManagementSystem.DependencyResolving;

namespace UnitTestProject2
{
    [TestClass]
    public class UnitTest1
    {
        private IUser user;
        private ViewModels.User viewUser;

        [TestInitialize]
        public void Init()
        {
            DependencyContainer.RegisterDependencies();
            Mapper.Initialize(m =>
            {
                m.AddProfile<ViewToModelMapper>();
                m.AddProfile<ModelToRepositoryMapper>();
            });
            user = new Implementations.User
            {
                Email = "email123",
                Password = "qwe123",
                FirstName = "Misha",
                LastName = "Ass",
                UserRole = new UserRole
                {
                    Id = 1,
                    RoleName = "Admin"
                }
            };
            viewUser = new ViewModels.User
            {
                Email = "email123",
                Password = "qwe123",
                FirstName = "Misha",
                LastName = "Ass",
                Role = "Admin"
            };
        }

        [TestMethod]
        public void ModelToViewUserMapping()
        {
            viewUser = Mapper.Map<ViewModels.User>(user);
            Assert.AreEqual(user.Email, viewUser.Email, "Email is incorrect");
            Assert.AreEqual(user.Password, viewUser.Password, "Password is incorrect");
            Assert.AreEqual(user.FirstName, viewUser.FirstName, "FirstName is incorrect");
            Assert.AreEqual(user.LastName, viewUser.LastName, "LastName is incorrect");
            Assert.AreEqual(user.UserRole.RoleName, viewUser.Role, "UserRole is incorrect");
        }

        [TestMethod]
        public void ViewToModelUserMapping()
        {
            user = Mapper.Map<IUser>(viewUser);
            Assert.AreEqual(viewUser.Email, user.Email, "Email is incorrect");
            Assert.AreEqual(viewUser.Password, user.Password, "Password is incorrect");
            Assert.AreEqual(viewUser.FirstName, user.FirstName, "FirstName is incorrect");
            Assert.AreEqual(viewUser.LastName, user.LastName, "LastName is incorrect");
            Assert.AreEqual(viewUser.Role, user.UserRole.RoleName, "UserRole is incorrect");

        }
    }
}
