﻿namespace BanHangThoiTrangMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateNewdatabaseTBVoucher : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Vouchers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 150),
                        Value = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Quantity = c.Int(nullable: false),
                        CreateBy = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Vouchers");
        }
    }
}
