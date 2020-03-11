using AutoMapper;
using CarPoolingEf.Services.Interfaces;
using System;
using CarPoolingEf;
using System.Collections.Generic;
using System.Linq;

namespace CarPoolingEf.Services.Services
{
    public class CarServices :  ICarService
    {
        private CarPoolingEfContext Db { get; set; }

        public CarServices(CarPoolingEfContext context) 
        {
            this.Db = context;
        }

        public bool AddNewCar(Models.Client.Car car,string ownerId)
        {
            if (ownerId != null)
            {
                car.Id = Guid.NewGuid().ToString();
                car.OwnerId = ownerId;
                var abc=AppService.Mapping<Models.Client.Car, Models.Data.Car>();
                this.Db.Cars.Add(AppService.Mapping<Models.Client.Car, Models.Data.Car>().Map<Models.Client.Car, Models.Data.Car>(car));
                return this.Db.SaveChanges() > 0;
            }

            return false;
        }

        public List<Models.Client.Car> GetCarsByUser(string id)
        {
            return AppService.Mapping<Models.Data.Car, Models.Client.Car>().Map<List<Models.Data.Car>, List<Models.Client.Car>>(this.Db.Cars?.Where(a => a.OwnerId == id).Select(a => a).ToList());
        }

        public Models.Client.Car GetCar(string id)
        {
            return AppService.Mapping<Models.Data.Car, Models.Client.Car>().Map<Models.Data.Car, Models.Client.Car>(this.Db.Cars?.FirstOrDefault(a => a.Id == id));
        }
    }
}
