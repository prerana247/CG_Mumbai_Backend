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
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Mobile no. cannot be empty")]
        [MinLength(10)]
        [MaxLength(10)]
        public string MobileNumber { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "You must specify a password betwwen 4 and 8")]
        public string Password { get; set; }


        [Required]
        [Column(TypeName = "nvarchar(24)")]
        public Roles Role{ get; set; }




    }
}
