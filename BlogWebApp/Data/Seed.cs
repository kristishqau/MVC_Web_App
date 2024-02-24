using BlogWebApp.Models;
using Microsoft.AspNetCore.Identity;

namespace BlogWebApp.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                    if (!context.Posts.Any())
                    {
                        context.Posts.AddRange(new List<Post>()
                    {
                        new Post()
                        {
                            Title = "Post 1",
                            Description = "This is the description of the first post.",
                            CreatedDate = DateTime.Now,
                            ImageUrl = "https://www.eacea.ec.europa.eu/sites/default/files/styles/ewcms_metatag_image/public/2022-09/ImagesPPPA_Sport_01.jpg?itok=lazvigrY",
                            Category = Category.Sport,
                            Comments = new List<Comment>
                            {
                                new Comment { Text = "First comment on the first post."},
                                new Comment { Text = "Second comment on the first post."}
                            }
                         },
                        new Post()
                        {
                            Title = "Post 2",
                            Description = "This is the description of the second post.",
                            CreatedDate = DateTime.Now,
                            ImageUrl = "https://hyderus.com/wp-content/uploads/2021/06/event-page-identity-politics_0.jpeg",
                            Category = Category.Politics,
                            Comments = new List<Comment>
                            {
                                new Comment { Text = "First comment on the second post."},
                                new Comment { Text = "Second comment on the second post." }
                            }
                         },
                        new Post()
                        {
                            Title = "Post 3",
                            Description = "This is the description of the third post.",
                            CreatedDate = DateTime.Now,
                            ImageUrl = "https://corporate.walmart.com/content/corporate/en_us/purpose/sustainability/planet/nature/jcr:content/par/image_2_0.img.png/1693432526985.png",
                            Category = Category.Nature,
                            Comments = new List<Comment>
                            {
                                new Comment { Text = "First comment on the second post."},
                                new Comment { Text = "Second comment on the second post." }
                            }
                        }
                        });
                        context.SaveChanges();
                    }
            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.Editor))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Editor));
                if (!await roleManager.RoleExistsAsync(UserRoles.Member))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Member));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminEmail = "admin@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        FullName = "Admin",
                        UserName = "admin",
                        Email = adminEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "Admin1234!");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string editorEmail = "editor@gmail.com";

                var editorUser = await userManager.FindByEmailAsync(editorEmail);
                if (editorUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        FullName = "Editor",
                        UserName = "editor",
                        Email = editorEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "Editor1234!");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.Editor);
                }

                string memberEmail = "member@gmail.com";

                var memberUser = await userManager.FindByEmailAsync(memberEmail);
                if (memberUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        FullName = "Member",
                        UserName = "member",
                        Email = memberEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "Member1234!");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.Member);
                }
            }
        }
    }
}
