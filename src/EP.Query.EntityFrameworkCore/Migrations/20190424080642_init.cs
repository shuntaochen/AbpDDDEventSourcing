using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EP.Query.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "datasource_folders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    parent_id = table.Column<int>(type: "int(11)", nullable: true),
                    name = table.Column<string>(type: "longtext", nullable: true),
                    level = table.Column<int>(type: "int(11)", nullable: false),
                    datasource = table.Column<int>(type: "int(11)", nullable: false),
                    tenant_id = table.Column<int>(type: "int(11)", nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    creator_user_id = table.Column<long>(type: "bigint(20)", nullable: true),
                    last_modifier_user_id = table.Column<long>(type: "bigint(20)", nullable: true),
                    last_modification_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    DataSourceFolderId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_datasource_folders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_datasource_folders_datasource_folders_DataSourceFolderId",
                        column: x => x.DataSourceFolderId,
                        principalTable: "datasource_folders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "datasouces",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    datasource_folder_id = table.Column<int>(type: "int(11)", nullable: false),
                    name = table.Column<string>(type: "longtext", nullable: true),
                    type = table.Column<int>(type: "int(11)", nullable: false),
                    sourcecontent = table.Column<string>(type: "longtext", nullable: true),
                    remark = table.Column<string>(type: "longtext", nullable: true),
                    tenant_id = table.Column<int>(type: "int(11)", nullable: true),
                    creator_user_id = table.Column<long>(type: "bigint(20)", nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    last_modifier_user_id = table.Column<long>(type: "bigint(20)", nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_datasouces", x => x.id);
                    table.ForeignKey(
                        name: "FK_datasouces_datasource_folders_datasource_folder_id",
                        column: x => x.datasource_folder_id,
                        principalTable: "datasource_folders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "datasource_fields",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    datasource_id = table.Column<int>(type: "int(11)", nullable: false),
                    name = table.Column<string>(type: "longtext", nullable: true),
                    type = table.Column<string>(type: "longtext", nullable: true),
                    display_text = table.Column<string>(type: "longtext", nullable: true),
                    filter = table.Column<string>(type: "longtext", nullable: true),
                    tenant_id = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_datasource_fields", x => x.id);
                    table.ForeignKey(
                        name: "FK_datasource_fields_datasouces_datasource_id",
                        column: x => x.datasource_id,
                        principalTable: "datasouces",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_datasouces_datasource_folder_id",
                table: "datasouces",
                column: "datasource_folder_id");

            migrationBuilder.CreateIndex(
                name: "IX_datasource_fields_datasource_id",
                table: "datasource_fields",
                column: "datasource_id");

            migrationBuilder.CreateIndex(
                name: "IX_datasource_folders_DataSourceFolderId",
                table: "datasource_folders",
                column: "DataSourceFolderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "datasource_fields");

            migrationBuilder.DropTable(
                name: "datasouces");

            migrationBuilder.DropTable(
                name: "datasource_folders");
        }
    }
}
