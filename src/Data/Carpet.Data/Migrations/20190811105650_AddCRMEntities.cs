namespace Carpet.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddCRMEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: string.Empty);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: true),
                    Salary = table.Column<decimal>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employee_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    OrdinaryPrice = table.Column<decimal>(nullable: false),
                    ExpressAddOnPrice = table.Column<decimal>(nullable: false),
                    VacuumCleaningAddOnPrice = table.Column<decimal>(nullable: false),
                    FlavorAddOnPrice = table.Column<decimal>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicle",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    RegistrationNumber = table.Column<string>(nullable: true),
                    IsDamaged = table.Column<bool>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicle", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: false),
                    PickUpAddress = table.Column<string>(nullable: false),
                    DeliveryAddress = table.Column<string>(nullable: true),
                    CreatorId = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customer_Employee_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customer_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VehicleEmployee",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    VehicleId = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<string>(nullable: true),
                    CarrierId = table.Column<string>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleEmployee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleEmployee_Employee_CarrierId",
                        column: x => x.CarrierId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehicleEmployee_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehicleEmployee_Vehicle_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    IsExpress = table.Column<bool>(nullable: false),
                    HasFlavor = table.Column<bool>(nullable: false),
                    PickUpFor = table.Column<DateTime>(nullable: true),
                    PickUpForStartHour = table.Column<string>(nullable: true),
                    PickUpForEndHour = table.Column<string>(nullable: true),
                    PickUpOn = table.Column<DateTime>(nullable: true),
                    WashingOn = table.Column<DateTime>(nullable: true),
                    DeliveringFor = table.Column<DateTime>(nullable: true),
                    DeliveringForStartHour = table.Column<string>(nullable: true),
                    DeliveringForEndHour = table.Column<string>(nullable: true),
                    DeliverOn = table.Column<DateTime>(nullable: true),
                    ItemQuantitySetByUser = table.Column<int>(nullable: false),
                    IsPaid = table.Column<bool>(nullable: false),
                    PaidAmount = table.Column<decimal>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    StatusId = table.Column<int>(nullable: false),
                    CustomerId = table.Column<string>(nullable: true),
                    CreatorId = table.Column<string>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Employee_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_OrderStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "OrderStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderDeliveryVehicleEmployee",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    OrderId = table.Column<string>(nullable: true),
                    VehicleEmployeeId = table.Column<string>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDeliveryVehicleEmployee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDeliveryVehicleEmployee_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderDeliveryVehicleEmployee_VehicleEmployee_VehicleEmployeeId",
                        column: x => x.VehicleEmployeeId,
                        principalTable: "VehicleEmployee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    ItemId = table.Column<int>(nullable: false),
                    OrderId = table.Column<string>(nullable: true),
                    ItemWidth = table.Column<decimal>(nullable: false),
                    ItemHeight = table.Column<decimal>(nullable: false),
                    HasVacuumCleaning = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItem_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderPickUpVehicleEmployee",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    OrderId = table.Column<string>(nullable: true),
                    VehicleEmployeeId = table.Column<string>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPickUpVehicleEmployee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderPickUpVehicleEmployee_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderPickUpVehicleEmployee_VehicleEmployee_VehicleEmployeeId",
                        column: x => x.VehicleEmployeeId,
                        principalTable: "VehicleEmployee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CreatorId",
                table: "Customer",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_IsDeleted",
                table: "Customer",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_UserId",
                table: "Customer",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_IsDeleted",
                table: "Employee",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_UserId",
                table: "Employee",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_IsDeleted",
                table: "Item",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CreatorId",
                table: "Order",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerId",
                table: "Order",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_IsDeleted",
                table: "Order",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Order_StatusId",
                table: "Order",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDeliveryVehicleEmployee_IsDeleted",
                table: "OrderDeliveryVehicleEmployee",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDeliveryVehicleEmployee_OrderId",
                table: "OrderDeliveryVehicleEmployee",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDeliveryVehicleEmployee_VehicleEmployeeId",
                table: "OrderDeliveryVehicleEmployee",
                column: "VehicleEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_IsDeleted",
                table: "OrderItem",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ItemId",
                table: "OrderItem",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPickUpVehicleEmployee_IsDeleted",
                table: "OrderPickUpVehicleEmployee",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPickUpVehicleEmployee_OrderId",
                table: "OrderPickUpVehicleEmployee",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPickUpVehicleEmployee_VehicleEmployeeId",
                table: "OrderPickUpVehicleEmployee",
                column: "VehicleEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_IsDeleted",
                table: "Vehicle",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleEmployee_CarrierId",
                table: "VehicleEmployee",
                column: "CarrierId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleEmployee_EmployeeId",
                table: "VehicleEmployee",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleEmployee_IsDeleted",
                table: "VehicleEmployee",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleEmployee_VehicleId",
                table: "VehicleEmployee",
                column: "VehicleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDeliveryVehicleEmployee");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "OrderPickUpVehicleEmployee");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "VehicleEmployee");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "OrderStatus");

            migrationBuilder.DropTable(
                name: "Vehicle");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");
        }
    }
}
