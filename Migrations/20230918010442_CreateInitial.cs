using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreProgram_.Migrations
{
    public partial class CreateInitial : Migration
    {
        //Використовуються команди CreateTable для створення таблиць бази даних. У міграції створюються дві таблиці: Clients і Baskets.
        //Метод міграції Up відповідає за створення таблиць та відповідних зв'язків.
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NumberPhone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                //Встановлюються зв'язки між таблицями через зовнішні ключі.
                //У таблиці Baskets є зовнішній ключ ClientID, який посилається на ClientID в таблиці Clients.
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientID);
                });

            migrationBuilder.CreateTable(
                name: "Baskets",
                columns: table => new
                {
                    BasketID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientID = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Baskets", x => x.BasketID);
                    table.ForeignKey(
                        name: "FK_Baskets_Clients_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Clients",
                        principalColumn: "ClientID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_ClientID",
                table: "Baskets",
                column: "ClientID");
        }
        //Метод міграції Down містить логіку для відкату міграції. У цьому випадку, він видаляє створені таблиці, які були створені у методі Up.
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Baskets");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
//використовується для збереження структури бази даних. Під час виконання програми ця міграція буде застосована до бази даних
//для створення необхідних таблиць та зв'язків між ними.
