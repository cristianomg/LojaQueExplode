using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace Br.Com.LojaQueExplode.Api.Extensions
{
    public static class UserExtension
    {
        public static Guid GetUserID(this HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated)
                return Guid.Parse(context.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            else
                return Guid.Empty;
        }
    }
}
