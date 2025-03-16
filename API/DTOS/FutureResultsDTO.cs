using System.ComponentModel.DataAnnotations.Schema;

namespace API.DTOS
{
    public class FutureResultsDTO
    {
        public string Country { get; set; }
        public int Year { get; set; }
        public int Gender_Female { get; set; }
        public int Gender_Male { get; set; }
        public int Gender_Total { get; set; }
        public int Age_Under18 { get; set; }
        public int Age_Over18 { get; set; }
        public int Age_Total { get; set; }
        public double Safty_Index { get; set; }
        public double Safty_Percentage { get; set; }

    }
}
