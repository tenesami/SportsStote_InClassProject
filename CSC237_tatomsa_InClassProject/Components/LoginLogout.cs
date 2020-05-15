using CSC237_tatomsa_InClassProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CSC237_tatomsa_InClassProject.Components
{
    public class LoginLogout : ViewComponent
    {
        private SignInManager<User> signInManager;
        public LoginLogout(SignInManager<User> signInMngr) => signInManager = signInMngr;

        /***********************
         * User parameter sent to Invoke() method is the user property of the View class,
         * which is of type System.Security.Claims.ClaimsPrinciple
         * ********************/
         public IViewComponentResult Invoke(ClaimsPrincipal user)
        {
            if (signInManager.IsSignedIn(user))
                return View(true);
            else
                return View(false);
        }
    }
}
