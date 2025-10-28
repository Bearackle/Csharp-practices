using Microsoft.AspNet.Identity;
using MyEcommerce.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEcommerce.Infrastructure
{
    public class CurrentUser : ICurrentUser
    {
        public string UserId => HttpContext.Current?.User?.Identity?.GetUserId();

        public bool IsAuthenticated => HttpContext.Current?.User?.Identity?.IsAuthenticated ?? false;
    }
}