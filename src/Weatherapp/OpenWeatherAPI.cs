using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Weatherapp
{
    class OpenWeatherAPI
    {
        public class Weather
        {
            int Id { get; set; }
            string Name { get; set; }
            string Description { get; set; }
            string Icon { get; set; }



        }

        public class MainData
        {
            public double TempInKelvin { get; set; }

            public double FeelsLikeInKelvin { get; set; }

            public int Pressure { get; set; }

            public int Humidity { get; set; }

        }








  



    }
}
