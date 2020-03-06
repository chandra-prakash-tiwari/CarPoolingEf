using CarPoolingEf;
using CarPoolingEf.Models;
using CarPoolingEf.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingEf.Services.Services
{
    public class UserServices : IUserServices
    {
        private CarPoolingEfContext Db { get; set; }

        public UserServices(CarPoolingEfContext context)
        {
            this.Db = context;
        }

        public bool AddNewUser(User user)
        {
            user.Id = Guid.NewGuid().ToString();
            this.Db.Users.Add(user);
            return this.Db.SaveChanges() > 0;
        }

        public User Authentication(Login credentials)
        {
            return this.Db.Users?.FirstOrDefault(a => a.UserName == credentials.UserName && a.Password == credentials.Password);
        }

        public bool DeleteUser(string id)
        {
            this.Db.Users?.Remove(this.Db.Users?.FirstOrDefault(a => a.Id == id));
            return this.Db.SaveChanges() > 0;
        }

        public bool UpdateUser(User updateRequest, string id)
        {
            User oldDetails= this.Db.Users?.FirstOrDefault(a => a.Id == id);
            if (oldDetails != null)
            {
                oldDetails.Name = updateRequest.Name;
                oldDetails.Address = updateRequest.Address;
                oldDetails.Mobile = updateRequest.Mobile;

                return this.Db.SaveChanges() > 0;
            }

            return false;
        }

        public User GetUser(string id)
        {
            return this.Db.Users?.FirstOrDefault(a => a.Id == id);
        }

        public bool CheckUserName(string userName)
        {
            return this.Db.Users?.FirstOrDefault(a => a.UserName == userName) != null; 
        }
    }
}
