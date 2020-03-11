using AutoMapper;
using CarPoolingEf.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarPoolingEf.Services.Services
{
    public class CarServices :  ICarService
    {
        private CarPoolingEfContext Db { get; set; }

        private MapperConfiguration DTCConfig { get; set; }

        private MapperConfiguration CTDConfig { get; set; }

        private IMapper DTCMapper { get; set; }

        private IMapper CTDMapper { get; set; }

        public CarServices(CarPoolingEfContext context) 
        {
            this.Db = context;

            this.CTDConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Models.Client.Car, Models.Data.Car>();
            });

            this.DTCConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Models.Data.Car, Models.Client.Car>();
            });

            this.CTDMapper = this.CTDConfig.CreateMapper();

            this.DTCMapper = this.DTCConfig.CreateMapper();
        }

        public bool AddNewCar(Models.Client.Car car,string ownerId)
        {
            if (ownerId != null)
            {
                car.Id = Guid.NewGuid().ToString();
                car.OwnerId = ownerId;
                this.Db.Cars.Add(this.CTDMapper.Map<Models.Client.Car, Models.Data.Car>(car));
                return this.Db.SaveChanges() > 0;
            }

            return false;
        }

        public List<Models.Client.Car> GetCarsByUser(string id)
        {
            return this.DTCMapper.Map<List<Models.Data.Car>, List<Models.Client.Car>>(this.Db.Cars?.Where(a => a.OwnerId == id).Select(a => a).ToList());
        }

        public Models.Client.Car GetCar(string id)
        {
            return this.DTCMapper.Map<Models.Data.Car, Models.Client.Car>(this.Db.Cars?.FirstOrDefault(a => a.Id == id));
        }
    }
}
