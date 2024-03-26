namespace BanHangThoiTrangMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatetbVouchersBuilder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vouchers", "CreateBy", c => c.String());
            AddColumn("dbo.Vouchers", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Vouchers", "ModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Vouchers", "ModifiedBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vouchers", "ModifiedBy");
            DropColumn("dbo.Vouchers", "ModifiedDate");
            DropColumn("dbo.Vouchers", "CreateDate");
            DropColumn("dbo.Vouchers", "CreateBy");
        }
    }
}
