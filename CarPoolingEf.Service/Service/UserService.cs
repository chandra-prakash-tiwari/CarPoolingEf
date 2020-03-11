using AutoMapper;
using CarPoolingEf.Services.Interfaces;
using System;
using System.Linq;

namespace CarPoolingEf.Services.Services
{
    public class UserService : IUserService
    {
        private CarPoolingEfContext Db { get; set; }

        private MapperConfiguration DTCConfig { get; set; }

        private MapperConfiguration CTDConfig { get; set; }

        private IMapper DTCMapper { get; set; }

        private IMapper CTDMapper { get; set; }

        public UserService(CarPoolingEfContext context)
        {
            this.Db = context;

            this.CTDConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Models.Client.User, Models.Data.User>();
            });

            this.DTCConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap< Models.Data.User, Models.Client.User>();
            });

            this.CTDMapper = this.CTDConfig.CreateMapper();

            this.DTCMapper = this.DTCConfig.CreateMapper();
        }

        public bool AddNewUser(Models.Client.User user)
        {
            user.Id = Guid.NewGuid().ToString();
            this.Db.Users.Add(this.CTDMapper.Map<Models.Client.User, Models.Data.User>(user));
            return this.Db.SaveChanges() > 0;
        }

        public Models.Client.User Authentication(Models.Client.Login credentials)
        {
            return this.DTCMapper.Map<Models.Data.User, Models.Client.User>(this.Db.Users?.FirstOrDefault(a => a.UserName == credentials.UserName && a.Password == credentials.Password));
        }

        public bool DeleteUser(string id)
        {
            this.Db.Users?.Remove(this.Db.Users?.FirstOrDefault(a => a.Id == id));
            return this.Db.SaveChanges() > 0;
        }

        public bool UpdateUser(Models.Client.User updateRequest, string id)
        {
            Models.Data.User oldDetails = this.Db.Users?.FirstOrDefault(a => a.Id == id);
            if (oldDetails != null)
            {
                oldDetails.Name = updateRequest.Name;
                oldDetails.Address = updateRequest.Address;
                oldDetails.Mobile = updateRequest.Mobile;

                return this.Db.SaveChanges() > 0;
            }

            return false;
        }

        public Models.Client.User GetUser(string id)
        {
            return this.DTCMapper.Map < Models.Data.User, Models.Client.User > (this.Db.Users?.FirstOrDefault(a => a.Id == id));
        }

        public bool CheckUserName(string userName)
        {
            return this.Db.Users?.FirstOrDefault(a => a.UserName == userName) != null; 
        }
    }
}
