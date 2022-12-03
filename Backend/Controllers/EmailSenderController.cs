using Backend.Data;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailSenderController : ControllerBase
    {

        private readonly IEmailSender _emailSender;
        
        private readonly CGDbContext _DbContext;
        public EmailSenderController(IEmailSender emailSender, CGDbContext DbContext)
        {
            _emailSender = emailSender;
            _DbContext = DbContext;
        }


        [HttpPost, Route("SendEmail")]
        #region Send Mail API
        ///<summary>Send Mail</summary> 
        public async Task<IActionResult> SendEmailAsync(string recipientEmail, string recipientFirstName, string Subject, string Body)
        {

            try
            {
                string messageStatus = await _emailSender.SendEmailAsync(recipientEmail, recipientFirstName, Subject, Body);
                return Ok(messageStatus);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }
        #endregion


        [HttpPost, Route("SendOTP")]
        public async Task<IActionResult> SendOTP(int id)
        {
            string Subject = "OTP for Email Verification";
            var user = _DbContext.User.Where(g => g.UserId == id).FirstOrDefault();
            string recipientEmail = user.CorpMail;
            string recipientFirstName = user.FirstName;
            var otp = GenerateOTP();
            string Body = $"Here is your otp for verification<br/>" +
                         $"Your OTP is: {otp}  <br/>";

              
            try
            {
                
                string messageStatus = await _emailSender.SendEmailAsync(recipientEmail, recipientFirstName, Subject, Body);
                user = _DbContext.User.First(a => a.UserId == id);
                user.OTP =Convert.ToInt32(otp);
                _DbContext.SaveChanges();
                return Ok(messageStatus);
            }


            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

    

        //For generating random number
        private string GenerateOTP()
        {
            string OTPLength = "6";
            string NewPassword = "";

            string allowedChars = "";
            allowedChars = "1,2,3,4,5,6,7,8,9";
            //allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";
            //allowedChars += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";

            char[] sep = { ',' };
            string[] arr = allowedChars.Split(sep);
            string IDString = "";
            string temp = "";
            Random rand = new Random();
            for (int i = 0; i < Convert.ToInt32(OTPLength); i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                IDString += temp;
                NewPassword = IDString;
            }
            return NewPassword;
        }



    }
}
