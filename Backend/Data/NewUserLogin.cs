using System.ComponentModel.DataAnnotations;

namespace Backend.Data
{
    public class NewUserLogin
    {

        [DataType("varchar(30)")]
        [Required(ErrorMessage = "Email cannot be empty")]
        public string CorpMail { get; set; }

        [DataType("varchar(30)")]
        [Required(ErrorMessage = "Email cannot be empty")]
        public string PersonalMail { get; set; }


        public int OTP { get; set; }
    }
}
