using System.ComponentModel.DataAnnotations.Schema;

namespace CarPoolingEf.Models
{
    [ComplexType]
    public class Place
    {
        public string LandMark { get; set; }

        public string City { get; set; }

        public int Pincode { get; set; }
    }
}
