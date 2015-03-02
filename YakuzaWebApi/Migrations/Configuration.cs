namespace YakuzaWebApi.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using YakuzaWebApi.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<YakuzaWebApi.Models.YakuzaWebApiContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(YakuzaWebApi.Models.YakuzaWebApiContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.GameSessions.AddOrUpdate(p => p.Id,
                new GameSession
                {
                    Id = 0,
                    Users = null,
                    GameEvents = null,
                    IsActive = true,
                    IsNight = false

                });
            context.GameEvents.AddOrUpdate(g => g.GameSessionId,
                new GameEvent
                {
                    GameSessionId = 0,
                    TextContent = "cycki",
                    Date = DateTime.Now,
                    isOnlyForMafia = false
                });
            context.Users.AddOrUpdate(u => u.Username,
                new User
                {
                    Username = "Marian",
                    RoleName = "Lubi Anal",
                    isInMafia = true,
                    isKilled = false
                });
        }
    }
}
