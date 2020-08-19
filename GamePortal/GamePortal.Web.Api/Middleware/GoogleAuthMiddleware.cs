using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GamePortal.Web.Api.Middleware
{
    public class GoogleAuthMiddleware : OwinMiddleware
    {
        public GoogleAuthMiddleware(OwinMiddleware next) : base(next)
        {

        }
        public override Task Invoke(IOwinContext context)
        {
            context.Authentication.Challenge(new AuthenticationProperties
            {
                RedirectUri = "/external/google"
            }
            , "MyGoogle");
            return Task.CompletedTask;
        }
    }
}