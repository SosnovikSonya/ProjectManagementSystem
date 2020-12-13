using ProjectManagementSystem.Views.ViewModels.UserModels;
using System.Collections.Generic;

namespace ProjectManagementSystem
{
    public static class AuthContext
    {
        private static Dictionary<string, User> Users { get; set; } = new Dictionary<string, User>();

        public static User GetCurrentUser(string email)
        {
            lock (Users)
            {
                return Users[email];
            }
        }

        public static void SetCurrentUser(User user)
        {
            lock (Users)
            {
                Users.Add(user.Email, user);
            }
        }

        public static void Logout(string email)
        {
            lock (Users)
            {
                Users.Remove(email);
            }
        }
    }
}
