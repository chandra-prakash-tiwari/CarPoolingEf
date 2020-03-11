using CarPoolingEf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingEf.Services.Interfaces
{
    public interface IUserService
    {
        bool AddNewUser(Models.Client.User user);

        Models.Client.User Authentication(Models.Client.Login credentials);

        bool DeleteUser(string id);

        bool UpdateUser(Models.Client.User updateRequest, string id);
        Models.Client.User GetUser(string id);

        bool CheckUserName(string userName);
    }
}
