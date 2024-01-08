using OptikShop.Data.Enums;
using System.Security.Claims;

namespace OptikShop.WebUI.Extensions
{
	public static class ClaimPrincipalExtensions
	{
		public static bool IsLogged(this ClaimsPrincipal user)
		{
			if (user.Claims.FirstOrDefault(x => x.Type == "id") != null)
				return true;
			else
				return false;
		}

		public static string GetUserFirstName(this ClaimsPrincipal user)
		{
			return user.Claims.FirstOrDefault(x => x.Type == "firstName")?.Value;
		}

		public static string GetUserLastName(this ClaimsPrincipal user)
		{
			return user.Claims.FirstOrDefault(x => x.Type == "lastName")?.Value;
		}

		public static bool IsAdmin(this ClaimsPrincipal user)
		{
			var userType = user.Claims.FirstOrDefault(x => x.Type == "userType")?.Value;

			if (userType == UserTypeEnum.admin.ToString())
				return true;
			else
				return false;


		}
	}
}
