using BusinessLayer.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Repository.Implementation
{
    public class Auth : Attribute, IAuthorizationFilter
    {
        private readonly string _role;
        public Auth(string role)
        {
            _role = role;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var jwtService = context.HttpContext.RequestServices.GetService<IJwtService>();
            if (jwtService == null)
            {
                return;
            }

            var token = context.HttpContext.Session.GetString("jwttoken");

            if (token == null || !jwtService.ValidateToken(token, out JwtSecurityToken jwtToken))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Patient", action = "PatientLogin" }));
                return;

            }

            var roleClaim = jwtToken.Claims.Where(a => a.Type == "role").FirstOrDefault();
            var rolevalue = roleClaim.Value;
            if (rolevalue == null)  
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Patient", action = "PatientLogin" }));
                return;
            }

            if(string.IsNullOrWhiteSpace(_role) || rolevalue != _role)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Patient", action = "AccessDenied" }));
                return;
            }
           
        }
    }
}
