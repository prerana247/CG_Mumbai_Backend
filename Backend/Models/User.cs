using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class User
    {

        [Key]
        [Required]
        public int UserId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [DataType("varchar(30)")]
        [Required(ErrorMessage = "Email cannot be empty")]
        public string PersonalMail { get; set; }




        [DataType("varchar(30)")]
        [Required(ErrorMessage = "Email cannot be empty")]
        public string CorpMail { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Mobile no. cannot be empty")]
        [Phone]
        public string MobileNumber { get; set; }


        [Required]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DOJ { get; set; }


        [Required]
        [DataType("varchar(30)")]
        public string Grade { get; set; }


        [Required]
        public string Location { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(24)")]
        public string Role{ get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "You must specify a password betwwen 4 and 8")]
        public string Password { get; set; }


        public int OTP { get; set; }

        public bool isVerified { get; set; }





    }
}
