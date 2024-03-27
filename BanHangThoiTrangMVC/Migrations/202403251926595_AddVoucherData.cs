namespace BanHangThoiTrangMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVoucherData : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Vouchers", newName: "tb_Vouchers");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.tb_Vouchers", newName: "Vouchers");
        }
    }
}
