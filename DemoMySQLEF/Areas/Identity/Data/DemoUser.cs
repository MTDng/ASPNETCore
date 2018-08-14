using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DemoMySQLEF.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the DemoUser class
    public class DemoUser : IdentityUser<Guid>
    {
        [PersonalData]
        public string Name { get; set; }
        [PersonalData]
        public DateTime DOB { get; set; }
    }
}
