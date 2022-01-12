using Microsoft.AspNetCore.Identity;
using Models;
using System.Linq;
using System.IO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using Newtonsoft.Json;
using System.Reflection;

namespace Data.Seeder
{
   public class Seeder
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userMgr;
        private readonly RoleManager<IdentityRole> _roleMgr;

        public Seeder( AppDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userMgr = userManager;
            _roleMgr = roleManager;
        }

        public async Task SeedMe()
        {
            try
            {
                _context.Database.EnsureCreated();
                var roles = new string[] { "Applicant", "Admin" };

                if (!_roleMgr.Roles.Any())
                {
                    foreach (var role in roles)
                    {
                        await _roleMgr.CreateAsync(new IdentityRole(role));
                    }
                }
                var dir = Directory.GetCurrentDirectory();
               // var path = Path.
                var data = File.ReadAllText(dir+"/Seeder.json");
                var listOfUsers = JsonConvert.DeserializeObject<List<AppUser>>(data);
                if (!_userMgr.Users.Any())
                {
                  var  role = "";
                    var counter = 0;
                    foreach (var user in listOfUsers)
                    {
                        user.UserName = user.Email;

                        role = counter < 1 ? roles[0] : roles[1];
                       var result = await _userMgr.CreateAsync(user, "P@ssw0rd");

                        if (result.Succeeded)
                        {
                          await  _userMgr.AddToRoleAsync(user, role);
                        }
                        counter++;
                    }
                }
            }catch(DbException dbex)
            {
                throw new Exception(dbex.Message);
            }

        }
    }
}
