using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWeather.Models
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext() : base("ApplicationContext") { }

        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }
    }
}