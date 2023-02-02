using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentenBeheer.Areas.Identity.Data;
using StudentenBeheer.Models;

namespace StudentenBeheer.Data
{

    public static class SeedDatabase
    {

        public static void Initialize(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager)
        {
            using (var context = new ApplicationContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationContext>>()))
            {
                ApplicationUser user = null;
                context.Database.EnsureCreated();

                if (!context.Users.Any())
                {
                    user = new ApplicationUser
                    {
                        UserName = "Admin",
                        Firstname = "Soufian",
                        Lastname = "Dnoub",
                        Email = "administrator@studentenbeheer.be",
                        EmailConfirmed = true
                    };

                    userManager.CreateAsync(user, "Abc!98765");
                }

                if (!context.Roles.Any())
                {

                    context.Roles.AddRange(

                            new IdentityRole { Id = "User", Name = "User", NormalizedName = "user" },
                            new IdentityRole { Id = "Admin", Name = "Admin", NormalizedName = "admin" }

                            );

                    context.SaveChanges();
                }


                if (!context.Gender.Any() || !(context.Student.Any()))
                {
                    context.Gender.AddRange(

                       new Gender
                       {

                           ID = 'M',
                           Name = "Male"


                       },

                       new Gender
                       {

                           ID = 'F',
                           Name = "Female"

                          
                       },

                       new Gender
                       {
                           ID = 'X',
                           Name = "Not set"
                       }

                   );
                    context.SaveChanges();

                    context.Student.AddRange(

                               new Student
                               {
                                   Name = "Soufian",
                                   Lastname = "dnoub",
                                   Birthday = DateTime.Now,
                                   GenderId = 'M',
                                   Deleted = DateTime.MaxValue


                               },
                               new Student
                               {
                                   Name = "Bram",
                                   Lastname = "Habo",
                                   Birthday = DateTime.Now,
                                   GenderId = 'F',
                                   Deleted = DateTime.Now


                               },
                                 new Student
                                 {
                                     Name = "Waldo",
                                     Lastname = "Janssens",
                                     Birthday = DateTime.Now,
                                     GenderId = 'M',
                                     Deleted = DateTime.MaxValue


                                 },
                                   new Student
                                   {
                                       Name = "Febe",
                                       Lastname = "Kei",
                                       Birthday = DateTime.Now,
                                       GenderId = 'F',
                                       Deleted = DateTime.MaxValue


                                   }
                        );
                    context.SaveChanges();

                }

                if (!context.Module.Any())
                {
                    context.Module.AddRange(

                    new Module
                    {
                        Name = "Trends 3",
                        Omschrijving = "Een vak waar samenwerken een must is",
                        Deleted = DateTime.MaxValue
                    },
                     new Module
                     {
                         Name = "IT essentials",
                         Omschrijving = "Een vak waar we de basis van IT gaan beheersen",
                         Deleted = DateTime.Now
                     },
                      new Module
                      {
                          Name = "Project Intro",
                          Omschrijving = "Een vak waar we leren hoe projecten op een goed einde te brengen",
                          Deleted = DateTime.MaxValue
                      }

                    );

                    context.SaveChanges();

                }


                if (user != null)
                {
                    context.UserRoles.AddRange(

                        new IdentityUserRole<string> { UserId = user.Id, RoleId = "Admin" }
                        //new IdentityUserRole<string> { UserId = user.Id, RoleId = "User" }

                        );

                    context.SaveChanges();
                }


            }
          


        }
    }

}

