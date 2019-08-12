namespace Carpet.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddDbSets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Employee_CreatorId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_AspNetUsers_UserId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_AspNetUsers_UserId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Employee_CreatorId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Customer_CustomerId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_OrderStatus_StatusId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDeliveryVehicleEmployee_Order_OrderId",
                table: "OrderDeliveryVehicleEmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDeliveryVehicleEmployee_VehicleEmployee_VehicleEmployeeId",
                table: "OrderDeliveryVehicleEmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Item_ItemId",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Order_OrderId",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderPickUpVehicleEmployee_Order_OrderId",
                table: "OrderPickUpVehicleEmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderPickUpVehicleEmployee_VehicleEmployee_VehicleEmployeeId",
                table: "OrderPickUpVehicleEmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleEmployee_Employee_CarrierId",
                table: "VehicleEmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleEmployee_Employee_EmployeeId",
                table: "VehicleEmployee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderStatus",
                table: "OrderStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderPickUpVehicleEmployee",
                table: "OrderPickUpVehicleEmployee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItem",
                table: "OrderItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDeliveryVehicleEmployee",
                table: "OrderDeliveryVehicleEmployee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Item",
                table: "Item");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employee",
                table: "Employee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                table: "Customer");

            migrationBuilder.RenameTable(
                name: "OrderStatus",
                newName: "OrderStatuses");

            migrationBuilder.RenameTable(
                name: "OrderPickUpVehicleEmployee",
                newName: "OrderPickUpVehicleEmployees");

            migrationBuilder.RenameTable(
                name: "OrderItem",
                newName: "OrderItems");

            migrationBuilder.RenameTable(
                name: "OrderDeliveryVehicleEmployee",
                newName: "OrderDeliveryVehicleEmployees");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Orders");

            migrationBuilder.RenameTable(
                name: "Item",
                newName: "Items");

            migrationBuilder.RenameTable(
                name: "Employee",
                newName: "Employees");

            migrationBuilder.RenameTable(
                name: "Customer",
                newName: "Customers");

            migrationBuilder.RenameIndex(
                name: "IX_OrderPickUpVehicleEmployee_VehicleEmployeeId",
                table: "OrderPickUpVehicleEmployees",
                newName: "IX_OrderPickUpVehicleEmployees_VehicleEmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderPickUpVehicleEmployee_OrderId",
                table: "OrderPickUpVehicleEmployees",
                newName: "IX_OrderPickUpVehicleEmployees_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderPickUpVehicleEmployee_IsDeleted",
                table: "OrderPickUpVehicleEmployees",
                newName: "IX_OrderPickUpVehicleEmployees_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItems",
                newName: "IX_OrderItems_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItem_ItemId",
                table: "OrderItems",
                newName: "IX_OrderItems_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItem_IsDeleted",
                table: "OrderItems",
                newName: "IX_OrderItems_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDeliveryVehicleEmployee_VehicleEmployeeId",
                table: "OrderDeliveryVehicleEmployees",
                newName: "IX_OrderDeliveryVehicleEmployees_VehicleEmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDeliveryVehicleEmployee_OrderId",
                table: "OrderDeliveryVehicleEmployees",
                newName: "IX_OrderDeliveryVehicleEmployees_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDeliveryVehicleEmployee_IsDeleted",
                table: "OrderDeliveryVehicleEmployees",
                newName: "IX_OrderDeliveryVehicleEmployees_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Order_StatusId",
                table: "Orders",
                newName: "IX_Orders_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_IsDeleted",
                table: "Orders",
                newName: "IX_Orders_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Order_CustomerId",
                table: "Orders",
                newName: "IX_Orders_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_CreatorId",
                table: "Orders",
                newName: "IX_Orders_CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Item_IsDeleted",
                table: "Items",
                newName: "IX_Items_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Employee_UserId",
                table: "Employees",
                newName: "IX_Employees_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Employee_IsDeleted",
                table: "Employees",
                newName: "IX_Employees_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Customer_UserId",
                table: "Customers",
                newName: "IX_Customers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Customer_IsDeleted",
                table: "Customers",
                newName: "IX_Customers_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Customer_CreatorId",
                table: "Customers",
                newName: "IX_Customers_CreatorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderStatuses",
                table: "OrderStatuses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderPickUpVehicleEmployees",
                table: "OrderPickUpVehicleEmployees",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItems",
                table: "OrderItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDeliveryVehicleEmployees",
                table: "OrderDeliveryVehicleEmployees",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Items",
                table: "Items",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Employees_CreatorId",
                table: "Customers",
                column: "CreatorId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_AspNetUsers_UserId",
                table: "Customers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_AspNetUsers_UserId",
                table: "Employees",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDeliveryVehicleEmployees_Orders_OrderId",
                table: "OrderDeliveryVehicleEmployees",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDeliveryVehicleEmployees_VehicleEmployee_VehicleEmployeeId",
                table: "OrderDeliveryVehicleEmployees",
                column: "VehicleEmployeeId",
                principalTable: "VehicleEmployee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Items_ItemId",
                table: "OrderItems",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderPickUpVehicleEmployees_Orders_OrderId",
                table: "OrderPickUpVehicleEmployees",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderPickUpVehicleEmployees_VehicleEmployee_VehicleEmployeeId",
                table: "OrderPickUpVehicleEmployees",
                column: "VehicleEmployeeId",
                principalTable: "VehicleEmployee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Employees_CreatorId",
                table: "Orders",
                column: "CreatorId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderStatuses_StatusId",
                table: "Orders",
                column: "StatusId",
                principalTable: "OrderStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleEmployee_Employees_CarrierId",
                table: "VehicleEmployee",
                column: "CarrierId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleEmployee_Employees_EmployeeId",
                table: "VehicleEmployee",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Employees_CreatorId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_AspNetUsers_UserId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_AspNetUsers_UserId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDeliveryVehicleEmployees_Orders_OrderId",
                table: "OrderDeliveryVehicleEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDeliveryVehicleEmployees_VehicleEmployee_VehicleEmployeeId",
                table: "OrderDeliveryVehicleEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Items_ItemId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderPickUpVehicleEmployees_Orders_OrderId",
                table: "OrderPickUpVehicleEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderPickUpVehicleEmployees_VehicleEmployee_VehicleEmployeeId",
                table: "OrderPickUpVehicleEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Employees_CreatorId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderStatuses_StatusId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleEmployee_Employees_CarrierId",
                table: "VehicleEmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleEmployee_Employees_EmployeeId",
                table: "VehicleEmployee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderStatuses",
                table: "OrderStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderPickUpVehicleEmployees",
                table: "OrderPickUpVehicleEmployees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItems",
                table: "OrderItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDeliveryVehicleEmployees",
                table: "OrderDeliveryVehicleEmployees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Items",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.RenameTable(
                name: "OrderStatuses",
                newName: "OrderStatus");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Order");

            migrationBuilder.RenameTable(
                name: "OrderPickUpVehicleEmployees",
                newName: "OrderPickUpVehicleEmployee");

            migrationBuilder.RenameTable(
                name: "OrderItems",
                newName: "OrderItem");

            migrationBuilder.RenameTable(
                name: "OrderDeliveryVehicleEmployees",
                newName: "OrderDeliveryVehicleEmployee");

            migrationBuilder.RenameTable(
                name: "Items",
                newName: "Item");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "Employee");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "Customer");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_StatusId",
                table: "Order",
                newName: "IX_Order_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_IsDeleted",
                table: "Order",
                newName: "IX_Order_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_CustomerId",
                table: "Order",
                newName: "IX_Order_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_CreatorId",
                table: "Order",
                newName: "IX_Order_CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderPickUpVehicleEmployees_VehicleEmployeeId",
                table: "OrderPickUpVehicleEmployee",
                newName: "IX_OrderPickUpVehicleEmployee_VehicleEmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderPickUpVehicleEmployees_OrderId",
                table: "OrderPickUpVehicleEmployee",
                newName: "IX_OrderPickUpVehicleEmployee_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderPickUpVehicleEmployees_IsDeleted",
                table: "OrderPickUpVehicleEmployee",
                newName: "IX_OrderPickUpVehicleEmployee_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItem",
                newName: "IX_OrderItem_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_ItemId",
                table: "OrderItem",
                newName: "IX_OrderItem_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_IsDeleted",
                table: "OrderItem",
                newName: "IX_OrderItem_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDeliveryVehicleEmployees_VehicleEmployeeId",
                table: "OrderDeliveryVehicleEmployee",
                newName: "IX_OrderDeliveryVehicleEmployee_VehicleEmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDeliveryVehicleEmployees_OrderId",
                table: "OrderDeliveryVehicleEmployee",
                newName: "IX_OrderDeliveryVehicleEmployee_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDeliveryVehicleEmployees_IsDeleted",
                table: "OrderDeliveryVehicleEmployee",
                newName: "IX_OrderDeliveryVehicleEmployee_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Items_IsDeleted",
                table: "Item",
                newName: "IX_Item_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_UserId",
                table: "Employee",
                newName: "IX_Employee_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_IsDeleted",
                table: "Employee",
                newName: "IX_Employee_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_UserId",
                table: "Customer",
                newName: "IX_Customer_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_IsDeleted",
                table: "Customer",
                newName: "IX_Customer_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_CreatorId",
                table: "Customer",
                newName: "IX_Customer_CreatorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderStatus",
                table: "OrderStatus",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderPickUpVehicleEmployee",
                table: "OrderPickUpVehicleEmployee",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItem",
                table: "OrderItem",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDeliveryVehicleEmployee",
                table: "OrderDeliveryVehicleEmployee",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Item",
                table: "Item",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employee",
                table: "Employee",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                table: "Customer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Employee_CreatorId",
                table: "Customer",
                column: "CreatorId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_AspNetUsers_UserId",
                table: "Customer",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_AspNetUsers_UserId",
                table: "Employee",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Employee_CreatorId",
                table: "Order",
                column: "CreatorId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Customer_CustomerId",
                table: "Order",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_OrderStatus_StatusId",
                table: "Order",
                column: "StatusId",
                principalTable: "OrderStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDeliveryVehicleEmployee_Order_OrderId",
                table: "OrderDeliveryVehicleEmployee",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDeliveryVehicleEmployee_VehicleEmployee_VehicleEmployeeId",
                table: "OrderDeliveryVehicleEmployee",
                column: "VehicleEmployeeId",
                principalTable: "VehicleEmployee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Item_ItemId",
                table: "OrderItem",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Order_OrderId",
                table: "OrderItem",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderPickUpVehicleEmployee_Order_OrderId",
                table: "OrderPickUpVehicleEmployee",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderPickUpVehicleEmployee_VehicleEmployee_VehicleEmployeeId",
                table: "OrderPickUpVehicleEmployee",
                column: "VehicleEmployeeId",
                principalTable: "VehicleEmployee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleEmployee_Employee_CarrierId",
                table: "VehicleEmployee",
                column: "CarrierId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleEmployee_Employee_EmployeeId",
                table: "VehicleEmployee",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
