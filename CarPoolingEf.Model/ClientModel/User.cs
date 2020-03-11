using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPoolingEf.Models.Client
{
    public class User
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string DrivingLicence { get; set; }

        public float Rating { get; set; }
    }
}
