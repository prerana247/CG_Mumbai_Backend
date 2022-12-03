using System.ComponentModel.DataAnnotations;

namespace Backend.Data
{
    public class UserLogin
    {


        [DataType("varchar(30)")]
        [Required(ErrorMessage = "Email cannot be empty")]
       public string CorpMail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "You must specify a password betwwen 4 and 8")]
        public string Password { get; set; }

        [DataType("varchar(30)")]
        [Required(ErrorMessage = "Email cannot be empty")]
        public string PersonalMail { get; set; }


        public int OTP { get; set; }

    }

}
