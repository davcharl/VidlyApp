namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'02394433-88e3-4bfa-b769-1e9c19c9e545', N'guest@vidly.com', 0, N'ADjnjf2C6eFUAkhFIxXFhykTBmWV1WXZiwcS/WrftwoGCg1SjLQUPxFf9znuDSpj3w==', N'34903082-83bc-45fa-8b44-9dfe3deb3546', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'cb7ef0ec-cb1b-46bb-87a4-1a2582e7bba2', N'admin@vidly.com', 0, N'AB7L397TRweto8BIixvfDkEldFHpnlOqa7iNQoX6KdBowJVO1blnjr+3c4J4aT7IKg==', N'0b86e17b-2b38-48a6-b136-32a97c4ec1f3', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')

                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'cd160148-a003-49b6-9859-8525e3d3b8f7', N'CanManageMovies')

                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'cb7ef0ec-cb1b-46bb-87a4-1a2582e7bba2', N'cd160148-a003-49b6-9859-8525e3d3b8f7')
                ");

        }
        
        public override void Down()
        {
        }
    }
}
