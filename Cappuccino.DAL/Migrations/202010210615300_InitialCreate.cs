using System;
using System.Data.Entity.Migrations;

namespace Cappuccino.DAL.Migrations
{
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SysActionButton",
                c => new
                {
                    ActionId = c.Int(nullable: false),
                    ButtonCode = c.String(nullable: false, maxLength: 50),
                    Location = c.Int(nullable: false),
                    ButtonClass = c.String(nullable: false, maxLength: 50),
                    ButtonIcon = c.String(nullable: false, maxLength: 50),
                })
                .PrimaryKey(t => t.ActionId)
                .ForeignKey("dbo.SysAction", t => t.ActionId)
                .Index(t => t.ActionId);

            CreateTable(
                "dbo.SysAction",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 50),
                    Code = c.String(nullable: false, maxLength: 50),
                    ParentId = c.Int(nullable: false),
                    Type = c.Int(nullable: false),
                    SortCode = c.Int(nullable: false),
                    CreateUserId = c.Int(nullable: false),
                    CreateTime = c.DateTime(nullable: false),
                    UpdateUserId = c.Int(nullable: false),
                    UpdateTime = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.SysActionMenu",
                c => new
                {
                    ActionId = c.Int(nullable: false),
                    Icon = c.String(maxLength: 50),
                    Url = c.String(maxLength: 100),
                })
                .PrimaryKey(t => t.ActionId)
                .ForeignKey("dbo.SysAction", t => t.ActionId)
                .Index(t => t.ActionId);

            CreateTable(
                "dbo.SysRole",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 50),
                    Code = c.String(nullable: false, maxLength: 50),
                    EnabledMark = c.Int(nullable: false),
                    Remark = c.String(maxLength: 250),
                    CreateUserId = c.Int(nullable: false),
                    CreateTime = c.DateTime(nullable: false),
                    UpdateUserId = c.Int(nullable: false),
                    UpdateTime = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.SysUser",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserName = c.String(nullable: false, maxLength: 50),
                    NickName = c.String(nullable: false, maxLength: 50),
                    PasswordHash = c.String(nullable: false, maxLength: 50),
                    PasswordSalt = c.String(nullable: false, maxLength: 50),
                    HeadIcon = c.String(nullable: false, maxLength: 50),
                    MobilePhone = c.String(maxLength: 11),
                    Email = c.String(nullable: false, maxLength: 50),
                    EnabledMark = c.Int(nullable: false),
                    CreateUserId = c.Int(nullable: false),
                    CreateTime = c.DateTime(nullable: false),
                    UpdateUserId = c.Int(nullable: false),
                    UpdateTime = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true);

            CreateTable(
                "dbo.SysUserAction",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.Int(nullable: false),
                    ActionId = c.Int(nullable: false),
                    HasPermisssin = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SysAction", t => t.ActionId, cascadeDelete: true)
                .ForeignKey("dbo.SysUser", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ActionId);

            CreateTable(
                "dbo.SysDict",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Code = c.String(nullable: false, maxLength: 50),
                    Name = c.String(nullable: false, maxLength: 50),
                    TypeId = c.Int(nullable: false),
                    SortCode = c.Int(nullable: false),
                    CreateUserId = c.Int(nullable: false),
                    CreateTime = c.DateTime(nullable: false),
                    UpdateUserId = c.Int(nullable: false),
                    UpdateTime = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SysDictType", t => t.TypeId, cascadeDelete: true)
                .Index(t => t.TypeId);

            CreateTable(
                "dbo.SysDictType",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Code = c.String(nullable: false, maxLength: 50),
                    Name = c.String(nullable: false, maxLength: 50),
                    SortCode = c.Int(nullable: false),
                    CreateUserId = c.Int(nullable: false),
                    CreateTime = c.DateTime(nullable: false),
                    UpdateUserId = c.Int(nullable: false),
                    UpdateTime = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.SysLogLogon",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    LogType = c.String(nullable: false, maxLength: 50),
                    Account = c.String(nullable: false, maxLength: 50),
                    RealName = c.String(nullable: false, maxLength: 50),
                    Description = c.String(nullable: false, maxLength: 200),
                    IPAddress = c.String(nullable: false, maxLength: 50),
                    IPAddressName = c.String(nullable: false, maxLength: 50),
                    CreateTime = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.SysRoleAction",
                c => new
                {
                    RoleId = c.Int(nullable: false),
                    ActionId = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.RoleId, t.ActionId })
                .ForeignKey("dbo.SysRole", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.SysAction", t => t.ActionId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.ActionId);

            CreateTable(
                "dbo.SysUserRole",
                c => new
                {
                    UserId = c.Int(nullable: false),
                    RoleId = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.SysUser", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.SysRole", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.SysDict", "TypeId", "dbo.SysDictType");
            DropForeignKey("dbo.SysUserAction", "UserId", "dbo.SysUser");
            DropForeignKey("dbo.SysUserAction", "ActionId", "dbo.SysAction");
            DropForeignKey("dbo.SysUserRole", "RoleId", "dbo.SysRole");
            DropForeignKey("dbo.SysUserRole", "UserId", "dbo.SysUser");
            DropForeignKey("dbo.SysRoleAction", "ActionId", "dbo.SysAction");
            DropForeignKey("dbo.SysRoleAction", "RoleId", "dbo.SysRole");
            DropForeignKey("dbo.SysActionMenu", "ActionId", "dbo.SysAction");
            DropForeignKey("dbo.SysActionButton", "ActionId", "dbo.SysAction");
            DropIndex("dbo.SysUserRole", new[] { "RoleId" });
            DropIndex("dbo.SysUserRole", new[] { "UserId" });
            DropIndex("dbo.SysRoleAction", new[] { "ActionId" });
            DropIndex("dbo.SysRoleAction", new[] { "RoleId" });
            DropIndex("dbo.SysDict", new[] { "TypeId" });
            DropIndex("dbo.SysUserAction", new[] { "ActionId" });
            DropIndex("dbo.SysUserAction", new[] { "UserId" });
            DropIndex("dbo.SysUser", new[] { "UserName" });
            DropIndex("dbo.SysActionMenu", new[] { "ActionId" });
            DropIndex("dbo.SysActionButton", new[] { "ActionId" });
            DropTable("dbo.SysUserRole");
            DropTable("dbo.SysRoleAction");
            DropTable("dbo.SysLogLogon");
            DropTable("dbo.SysDictType");
            DropTable("dbo.SysDict");
            DropTable("dbo.SysUserAction");
            DropTable("dbo.SysUser");
            DropTable("dbo.SysRole");
            DropTable("dbo.SysActionMenu");
            DropTable("dbo.SysAction");
            DropTable("dbo.SysActionButton");
        }
    }
}
