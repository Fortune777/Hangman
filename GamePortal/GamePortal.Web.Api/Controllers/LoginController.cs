using APerepechko.HangMan.Logic.Services.Contracts;
using Microsoft.Owin.Security;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GamePortal.Web.Api.Controllers
{
    public class LoginController : ApiController
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpGet, Route("external/google")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> GoogleLogin()
        {
            var provider = Request.GetOwinContext().Authentication;
            var loginInfo = await provider.GetExternalLoginInfoAsync();

            if (loginInfo == null) return BadRequest();

            await _userService.RegisterExternalUser(loginInfo);
            return Ok();
        }
    }
}
