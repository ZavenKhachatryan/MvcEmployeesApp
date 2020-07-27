using MyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class UserDataAccess : IUserDataAccess
    {
        private readonly DataContext data;

        public UserDataAccess(DataContext data)
        {
            this.data = data;
        }

        public User GetUser(User user)
        {
            return data.Users.FirstOrDefault(u => u.UserName == user.UserName && u.UserPassword == user.UserPassword);
        }
    }
}
