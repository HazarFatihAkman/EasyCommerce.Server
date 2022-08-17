using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyCommerce.Server.Shared.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CargoCompanies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebSite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CargoPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Available = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargoCompanies", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    KeyWords = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImgSrc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Available = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdentityNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Available = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrefixKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Integer = table.Column<int>(type: "int", nullable: false),
                    String = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Decimal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Double = table.Column<double>(type: "float", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Taxes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Percent = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taxes", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationRolesUser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KeyWords = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Barcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    ImgSrc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Available = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AddressDetail = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Available = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Address_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CreditCards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Cvv = table.Column<int>(type: "int", nullable: false),
                    ExpMonth = table.Column<int>(type: "int", nullable: false),
                    ExpYears = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCards", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_CreditCards_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaxId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PriceType = table.Column<int>(type: "int", nullable: false),
                    SaleChannel = table.Column<int>(type: "int", nullable: false),
                    TaxFreePrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    DiscountValue = table.Column<double>(type: "float", nullable: false),
                    IsValidPrice = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Prices_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prices_Taxes_TaxId",
                        column: x => x.TaxId,
                        principalTable: "Taxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CargoCompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    GaveToCargoDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentType = table.Column<int>(type: "int", nullable: false),
                    CargoFollowNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Orders_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_CargoCompanies_CargoCompanyId",
                        column: x => x.CargoCompanyId,
                        principalTable: "CargoCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    Open = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Carts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Carts_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Carts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Available", "Description", "ImgSrc", "KeyWords", "ParentId", "Title" },
                values: new object[,]
                {
                    { new Guid("723de3b8-6c07-4e8c-b1fc-1844482d9c4d"), true, "test 0", "test 0", "test 0", null, "Main -> 1 Category" },
                    { new Guid("723de3b8-6c07-4e8c-b1fc-184b482d9c4d"), true, "test 0", "test 0", "test 0", null, "Main -> 0 Category" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Available", "Email", "FirstName", "IdentityNumber", "LastName", "Password", "PhoneNumber" },
                values: new object[] { new Guid("8a270105-a039-45b0-90e9-62e6886894e2"), true, "akman.hazar.fatih@gmail.com", "Hazar Fatih", "30025022756", "Akman", "123", "5536803185" });

            migrationBuilder.InsertData(
                table: "Taxes",
                columns: new[] { "Id", "Name", "Percent" },
                values: new object[] { new Guid("ea730e99-db32-42a2-9a2e-36e10fac7f92"), "standard tax", 18.0 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ApplicationRolesUser", "Email", "FirstName", "LastName", "Password", "Pin" },
                values: new object[] { new Guid("5b645a88-9509-4a45-a0a6-b5ee24f5785f"), 3, "admin@gmail.com", "Admin", "Easy Commerce", "Admin123!", "1" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Available", "Description", "ImgSrc", "KeyWords", "ParentId", "Title" },
                values: new object[] { new Guid("240f393a-1635-478d-8c89-70326d1ee533"), true, "test 1", "test 1", "test 1", new Guid("723de3b8-6c07-4e8c-b1fc-1844482d9c4d"), "Sub Category / Main 1" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Available", "Description", "ImgSrc", "KeyWords", "ParentId", "Title" },
                values: new object[] { new Guid("7cf92a91-ba8d-495d-a1fa-2a6fad2012a8"), true, "test 1", "test 1", "test 1", new Guid("723de3b8-6c07-4e8c-b1fc-184b482d9c4d"), "Sub Category" });

            migrationBuilder.InsertData(
                table: "CreditCards",
                columns: new[] { "Id", "CustomerId", "Cvv", "ExpMonth", "ExpYears", "Name", "Number" },
                values: new object[] { new Guid("da36a422-efa1-4900-9fe4-faa9e9af1c9b"), new Guid("8a270105-a039-45b0-90e9-62e6886894e2"), 151, 12, 25, "Save card 1", "5400360000000003" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Available", "Description", "ImgSrc", "KeyWords", "ParentId", "Title" },
                values: new object[] { new Guid("0de9d83e-5e4c-4bd1-8ef1-2d4e32f921bc"), true, "test 2", "test 2", "test 2", new Guid("7cf92a91-ba8d-495d-a1fa-2a6fad2012a8"), "Sub -> 0 Category" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Available", "Description", "ImgSrc", "KeyWords", "ParentId", "Title" },
                values: new object[] { new Guid("35286247-b1ea-4e7e-88fb-a64ce16230ac"), true, "test 2", "test 2", "test 2", new Guid("240f393a-1635-478d-8c89-70326d1ee533"), "Sub -> 0 Category / Main 1" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Available", "Barcode", "CategoryId", "Description", "ImgSrc", "KeyWords", "Stock", "Title" },
                values: new object[] { new Guid("0ccc0e7e-bdec-4e75-b9b3-aa831952311d"), false, "1", new Guid("7cf92a91-ba8d-495d-a1fa-2a6fad2012a8"), "test", "test", "test", 10, "Product 1" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Available", "Description", "ImgSrc", "KeyWords", "ParentId", "Title" },
                values: new object[] { new Guid("5ed874e6-7151-4cc9-a925-dd249e3a4881"), true, "test 32", "test 3", "test 3", new Guid("0de9d83e-5e4c-4bd1-8ef1-2d4e32f921bc"), "Sub -> 1 Category" });

            migrationBuilder.InsertData(
                table: "Prices",
                columns: new[] { "Id", "DiscountValue", "IsValidPrice", "PriceType", "ProductId", "SaleChannel", "TaxFreePrice", "TaxId" },
                values: new object[] { new Guid("5d8e7a2a-88a4-4748-a41d-cbcb39286e70"), 0.0, false, 3, new Guid("0ccc0e7e-bdec-4e75-b9b3-aa831952311d"), 0, 20.5m, new Guid("ea730e99-db32-42a2-9a2e-36e10fac7f92") });

            migrationBuilder.InsertData(
                table: "Prices",
                columns: new[] { "Id", "DiscountValue", "IsValidPrice", "PriceType", "ProductId", "SaleChannel", "TaxFreePrice", "TaxId" },
                values: new object[] { new Guid("aba0000f-eb0b-4aeb-923e-0f5f9ccfa989"), 8.0, true, 2, new Guid("0ccc0e7e-bdec-4e75-b9b3-aa831952311d"), 0, 20.5m, new Guid("ea730e99-db32-42a2-9a2e-36e10fac7f92") });

            migrationBuilder.CreateIndex(
                name: "IX_Address_CustomerId",
                table: "Address",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "Unique_Address",
                table: "Address",
                columns: new[] { "AddressName", "CustomerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Carts_CustomerId",
                table: "Carts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_OrderId",
                table: "Carts",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ProductId",
                table: "Carts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentId",
                table: "Categories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Title",
                table: "Categories",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Unique_CreditCard",
                table: "CreditCards",
                columns: new[] { "CustomerId", "Number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Email",
                table: "Customers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AddressId",
                table: "Orders",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CargoCompanyId",
                table: "Orders",
                column: "CargoCompanyId");

            migrationBuilder.CreateIndex(
                name: "Unique_Order",
                table: "Orders",
                columns: new[] { "CustomerId", "CartId", "AddressId", "CargoCompanyId" },
                unique: true,
                filter: "[CustomerId] IS NOT NULL AND [CartId] IS NOT NULL AND [AddressId] IS NOT NULL AND [CargoCompanyId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_TaxId",
                table: "Prices",
                column: "TaxId");

            migrationBuilder.CreateIndex(
                name: "Unique_Price",
                table: "Prices",
                columns: new[] { "ProductId", "TaxId", "TaxFreePrice", "PriceType" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "CreditCards");

            migrationBuilder.DropTable(
                name: "Prices");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Taxes");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "CargoCompanies");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
