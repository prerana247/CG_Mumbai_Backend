using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using Backend.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginController : ControllerBase
    {
        private IConfiguration _config;
        private readonly CGDbContext _DbContext;

        public UserLoginController(CGDbContext context, IConfiguration config)
        {
            _DbContext = context;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        #region Login Method API
        ///<summary>Login Method to use Generated Token </summary> 
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            try
            {
                var user = Authenticate(userLogin);
                if (user != null)
                {
                    var Token = Generate(user);
                    var role = new { token = Token, Role = user.Role };
                    return Ok(role);
                }
                return NotFound("User Not found");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Server error {e.Message}");
            }
        }
        #endregion
       [HttpGet ("/NewUser")]
        public async Task<ActionResult<User>> GetUser(string Mail1, string Mail2)
        {
            try
            {
                NewUserLogin user = new NewUserLogin()
                {
                    PersonalMail = Mail1,
                    CorpMail = Mail2
                };

                var NewUser = NewAuthenticate(user);
                if (NewUser != null)
                {
                    var vOTP = VerifyOTP(user);
                    return NewUser;
                }
                else
                {
                    return NotFound("User Not Found");
                }



            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Server error {e.Message}");
            }
        }


        [HttpGet("/VerifyUser")]
        public async Task<ActionResult<User>> VerifyUser(string Mail1, int otp)
        {
            //_DbContext.Entry(User).State = EntityState.Modified;
            //_DbContext.SaveChanges();
            try
            {
                //User newUser = new User();
                NewUserLogin user = new NewUserLogin()
                {
                    CorpMail = Mail1,
                    OTP = otp
                };

                var vOTP = VerifyOTP(user);
                if (vOTP != null)
                {
                    //newUser.isVerified = true;
                    
                    return vOTP;

                }
                
                else
                {
                    return NotFound("Enter Valid OTP");
                }

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Server error {e.Message}");
            }
        }






        #region Generate Token method
        ///<summary>Generate Token method</summary> 
        private string Generate(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {

                  //new Claim(ClaimTypes.GivenName,user.Staff.FirstName),
                  //new Claim(ClaimTypes.Surname,user.Staff.LastName),
                  new Claim(ClaimTypes.SerialNumber,Convert.ToString(user.UserId)),
                  new Claim(ClaimTypes.Role,user.Role),
                  new Claim(ClaimTypes.Email,user.CorpMail)
            };

            var Token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], claims,
                expires: DateTime.Now.AddMinutes(60), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
        #endregion

        #region Authenticate Method
        ///<summary>Authenticate method</summary> 
        private User Authenticate([FromBody]UserLogin userLogin)
        {
            var Currentuser = _DbContext.User.FirstOrDefault(o => o.CorpMail == userLogin.CorpMail && o.Password == userLogin.Password);

            if (Currentuser != null)
            {
                return Currentuser;
            }
            return null;
        }
        #endregion

        #region NewAuthenticate Method
        ///<summary>Authenticate method</summary> 
        private User NewAuthenticate(NewUserLogin userLogin)
        {
            var Currentuser = _DbContext.User.FirstOrDefault(o => o.CorpMail == userLogin.CorpMail && o.PersonalMail == userLogin.PersonalMail);

            if (Currentuser != null)
            {
                return Currentuser;
            }
            return null;
        }
        #endregion


        #region VerifyOTP Method
        ///<summary>VerifyOTP method</summary> 
        private User VerifyOTP(NewUserLogin userLogin)
        {

            var Currentuser = _DbContext.User.FirstOrDefault(o => o.CorpMail == userLogin.CorpMail && o.OTP == userLogin.OTP);
            //User newUser = new User();

            //var v = true;

            if (Currentuser != null)
            {
                //newUser.isVerified = v;
                //_DbContext.SaveChanges();
                return Currentuser;

                
            }
            return null;
        }
        #endregion

        

    }
}
