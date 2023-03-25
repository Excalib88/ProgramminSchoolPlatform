using Microsoft.EntityFrameworkCore.Migrations;

namespace ProgrammingSchool.Web.Migrations;

public class AddRolesAndAdminUser : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("insert into \"AspNetRoles\" (\"Id\", \"Name\", \"NormalizedName\") values (1, 'Member', 'MEMBER'),(2, 'Moderator', 'MODERATOR'),(3, 'Administrator', 'ADMINISTRATOR')");
        migrationBuilder.Sql("insert into \"AspNetUserRoles\" (\"UserId\", \"RoleId\") values (1,3)");
    }
}