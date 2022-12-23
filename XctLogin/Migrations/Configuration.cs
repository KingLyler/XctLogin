namespace XctLogin.Migrations
{
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using XctLogin.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<XctLogin.Data_Access_Layer.LoginContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(XctLogin.Data_Access_Layer.LoginContext context)
        {
            var users = new List<User>
            {
                new User { Id = 1, Username = "john", Password = "1234567", Name = "John Smith" },

                new User { Id = 2, Username = "mary", Password = "1111111", Name = "Mary Kim"},

                new User { Id = 3, Username = "johndoe", Password = "222222", Name = "John Doe"},

                new User { Id = 4, Username = "k", Password = " ", Name = "Admin"}
            };
            users.ForEach(s => context.Users.AddOrUpdate(p => p.Id, s));
            context.SaveChanges();
        }
    }
}
