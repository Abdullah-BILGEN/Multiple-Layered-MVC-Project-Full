using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace BilgeShop.WebUI.Extensions
{

	// Cookie de  tutulan verilerin kontrolleriini yapmak için kullanılacak olan methodların toplanacağı class
	public static class ClaimsPrincipalExtensions
	{

		// this -> bu sayede method sondan çağrılır 
		//User.IsLogged() tarzında.

		public static bool IsLogged(this ClaimsPrincipal user)
		{ 
		  if(user.Claims.FirstOrDefault(x => x.Type == "id") != null)
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
			if (user.Claims.FirstOrDefault(x=> x.Type == "userType")?.Value=="Admin")
				return true;
			else 
				return false;	
		
		
		}

	}
}
