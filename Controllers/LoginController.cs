using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoWebAPI.Data;
using TodoWebAPI.Models;
using TodoWebAPI.Common;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace TodoWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly TodoContext _context;
        private readonly AppSettings _appSettings;
        

        public LoginController(TodoContext context, 
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        // GET: api/Login
        [AllowAnonymous]
        [HttpPost]
        public ActionResult<User> Login(User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var u = _context.User.SingleOrDefault(e => e.UserName == user.UserName && e.Password == user.Password && e.IsBlocked == false);

            if (u == null)
                return NotFound();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                NotBefore = DateTime.UtcNow,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            string ExpiresIn = tokenDescriptor.Expires.ToString();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            LoggedUser oUser = new LoggedUser();

            oUser.UserName = u.UserName;
            oUser.Name = u.Name;
            oUser.IsAdmin = u.IsAdmin;
            oUser.Role = u.Role;
            oUser.UserID = u.UserId;
            oUser.MustChangePwd = u.MustChangePwd;

            user.Name = oUser.Name;
            user.Role = oUser.Role;
            user.IsAdmin = oUser.IsAdmin;
            user.UserId = oUser.UserID;
            user.Password = "";
            user.Token = tokenString;
            user.MustChangePwd = oUser.MustChangePwd;

            return user;
        }
  
    }
}
