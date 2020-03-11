using CarPoolingEf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingEf.Services.Interfaces
{
    public interface ICarService
    {
        bool AddNewCar(Models.Client.Car car, string ownerId);

        List<Models.Client.Car> GetCarsByUser(string id);

        Models.Client.Car GetCar(string id);
    }
}
