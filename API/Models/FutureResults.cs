using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("Future_Results")]
    public class FutureResults
    {
        [Key]
        public int Id { get; set; }
        public string Country { get; set; }
        public int Year{ get; set; }
        [Column("Gender_Female")]
        public int GenderFemale { get; set; }
        [Column("Gender_Male")]
        public int GenderMale { get; set; }
        [Column("Gender_Total")]
        public int GenderTotal { get; set; }
        [Column("Age_Under18")]
        public int AgeUnder18 { get; set; }
        [Column("Age_Over18")]
        public int AgeOver18 { get; set; }
        [Column("Age_Total")]
        public int AgeTotal { get; set; }
        [Column("VALUE")]
        public double Value { get; set; }
        [Column("Total_Year_Country_Sum")]
        public double TotalYearCountrySum { get; set; }
        [Column("Safety_Index")]
        public double SafetyIndex { get; set; }
        [Column("Safety_Percentage")]
        public double SaftyPercentage { get; set; }
        

    }
}
