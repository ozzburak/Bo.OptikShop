using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient.Server;
using OptikShop.Business.Dtos;
using OptikShop.Business.Service;
using OptikShop.WebUI.Models;
using System.Security.Claims;
using OptikShop.Data.Enums;
using OptikShop.WebUI.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace OptikShop.WebUI.Controllers
{
    
    public class LoginController : Controller
    {
        private readonly IUserService _user;
        public LoginController(IUserService user)
        {
            _user = user;
        }
      


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }


            var addUserDto = new AddUserDto()
            {
                Firstname = model.FirstName.Trim(),
                LastName = model.LastName.Trim(),
                Email = model.Email.Trim().ToLower(),
                Password = model.Password.Trim(),
                ConfirmPassword = model.ConfirmPassword.Trim(),
            };

            var result = _user.AddUser(addUserDto);

            if (result.IsSucceed)
            {
                return RedirectToAction("LoginForm", "Login");
            }
            else
            {
                ViewBag.ErrorMessage = result.Message;
                return View(model);
            }

        }
        [HttpGet]
        
        public IActionResult LoginForm()
        {

            return View();
        }
        [HttpPost]
       public async Task<IActionResult> LoginForm(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Register", "Login");
            }

            var loginUserDto = new LoginUserDto()
            {
                Email = model.Email,
                Password = model.Password,
            };
            

            var userInfo = _user.LoginUser(loginUserDto);

            if (userInfo == null)
            {
                return RedirectToAction("Index", "Home");
            }
        
          


            var claims = new List<Claim>();

			claims.Add(new Claim("id", userInfo.Id.ToString()));
			claims.Add(new Claim("email", userInfo.Email));
			claims.Add(new Claim("firstName", userInfo.FirstName));
			claims.Add(new Claim("lastName", userInfo.LastName));
			claims.Add(new Claim("userType", userInfo.UserType.ToString()));

            claims.Add(new Claim(ClaimTypes.Role, userInfo.UserType.ToString()));

			var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			// Claims içerisindeki verilerle bir oturum açılacağını söylüyorum.

			var autProperties = new AuthenticationProperties
			{
				AllowRefresh = true, // yenilenebilir enerji kaynakları
				ExpiresUtc = new DateTimeOffset(DateTime.Now.AddHours(48)) // oturum 48 saat geçerli
			};

			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity), autProperties);

			return RedirectToAction("Index", "Home", new { area = "Admin" });

			
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}	





