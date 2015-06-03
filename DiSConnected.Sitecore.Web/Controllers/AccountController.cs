using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using AutoMapper;
using DiSConnected.Sitecore.Web.Common.Classes.Dtos;
using Sitecore;
using Sitecore.Security.Accounts;
using Sitecore.Security.Authentication;

namespace DiSConnected.Sitecore.Web.Controllers
{
    /// <summary>
    /// This entire controller is shelled out for demo purposes, and should be modified to suit the needs of implementation
    /// Possible replacements to the 'Login' method would be to implement an OWIN auth server and use a token based approach
    /// To keep this inital project ligher weight, I opted for a aspxauth cookie provided by Forms auth.
    /// </summary>
    [RoutePrefix("content_delivery/api/account")]
    [Route("{action=Get}")]
    public class AccountController : ApiController
    {
        /// <summary>
        /// Simple object to hold login creds submitted to endpoint, username and password expected to be coming in as base64 enoded
        /// </summary>
        public class SimpleLogin
        {
            public string username;
            public string password;

            public string UsernameDecoded()
            {
                return AtoBDecoded(username);
            }

            public string PasswordDecoded()
            {
                return AtoBDecoded(password);
            }

            private string AtoBDecoded(string input)
            {
                byte[] fromBase64String = System.Convert.FromBase64String(input);
                return System.Text.Encoding.ASCII.GetString(fromBase64String);
            }
        }
       
        /// <summary>
        /// Async get of current user based on identity
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[Authorize]
        [Route("")]
        [Route("{id:max(-1)}")]
        public async Task<object> Get()
        {
            //todo: get current sitecore user
            var httpContext = HttpContext.Current;
            return await Task.Run(() => GetAsync(httpContext));
        }

        private object GetAsync(HttpContext currentContext)
        {
            HttpContext.Current = currentContext;
            var user = Mapper.Map<User, UserDto>(Context.User);
            return user;
        }

        /// <summary>
        /// Post method to login in and get a forms auth cookie, synchronous.  Current login is expecting username/pass to be base64 encrypted
        /// This is a very rudimentary or crude auth process.
        /// </summary>
        /// <param name="currentLogin"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        public object Login([FromBody]SimpleLogin currentLogin)
        {
            var currentUsername = currentLogin.UsernameDecoded();
            var currentPassword = currentLogin.PasswordDecoded();
            bool retVal = false;
            //todo: get an auth token and return to angular or whatever endpoint consuming medium, feel free to implement replacement at will
            bool login = AuthenticationManager.Login(currentUsername, currentPassword);
            if (login)
            {
                // sometimes used to persist user roles
                //string userData = string.Join("|", GetCustomUserRoles());

                //Opted for forms auth, since this is how sitecore handles logging into shell
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                  1,                                     // ticket version
                  currentUsername,                              // authenticated username
                  DateTime.Now,                          // issueDate
                  DateTime.Now.AddMinutes(30),
                  false,
                  "",
                  FormsAuthentication.FormsCookiePath);  // the path for the cookie

                // Encrypt the ticket using the machine key
                string encryptedTicket = FormsAuthentication.Encrypt(ticket);

                // Add the cookie to the request to save it
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                cookie.HttpOnly = true;
                HttpContext.Current.Response.Cookies.Add(cookie);

                // Your redirect logic
                retVal = true;
            }
            return retVal;
        }
    }
}