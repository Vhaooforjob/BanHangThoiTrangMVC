namespace BanHangThoiTrangMVC.Migrations
{
    using BanHangThoiTrangMVC.Models.EF;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BanHangThoiTrangMVC.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BanHangThoiTrangMVC.Models.ApplicationDbContext context)
        {
            context.Voucher.AddOrUpdate(
                new Voucher { Code = "DC50", Value = 50000, StartDate = new DateTime(2024, 3, 20), EndDate = new DateTime(2024, 4, 26), Quantity = 2 }
            );

            // Lưu các thay đổi vào cơ sở dữ liệu
            context.SaveChanges();
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
