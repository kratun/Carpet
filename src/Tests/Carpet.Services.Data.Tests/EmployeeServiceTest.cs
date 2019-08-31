namespace Carpet.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Carpet.Common.Constants;
    using Carpet.Data;
    using Carpet.Data.Models;
    using Carpet.Data.Repositories;
    using Carpet.Services.Data.EmployeeService;
    using Carpet.Web.InputModels.Administration.Customers;
    using Carpet.Web.InputModels.Administration.Employees.Delete;
    using Carpet.Web.InputModels.Administration.Employees.Edit;
    using Carpet.Web.ViewModels.Administration.Customers;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class EmployeeServiceTest : BaseServiceTests
    {
        private IEmployeesService EmployeesServiceMock =>
           this.ServiceProvider.GetRequiredService<IEmployeesService>();

        [Fact]
        public async Task CreateAsyncReturnsCorrect()
        {
            var id = Guid.NewGuid().ToString();
            var user = new CarpetUser
            {
                Id = id,
                FirstName = "Иван",
                LastName = "Иванов",
                PhoneNumber = "0888777444",
                Email = "abv@abv.bg",
                UserName = "abv@abv.bg",
            };
            var roleAdminId = Guid.NewGuid().ToString();
            var role = new CarpetRole { Id = roleAdminId, Name = GlobalConstants.AdministratorRoleName };
            var roleOperatorId = Guid.NewGuid().ToString();
            var roleOperator = new CarpetRole { Id = roleOperatorId, Name = GlobalConstants.OperatorRoleName };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "CreateAsyncReturnsCorrect")
                .Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Users.Add(user);
            dbContext.Roles.Add(role);
            dbContext.Roles.Add(roleOperator);
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Employee>(dbContext);
            var service = new EmployeesService(repository, dbContext);

            var userFrom = dbContext.Users.First();

            var employeeId = Guid.NewGuid().ToString();

            var employee = new EmployeeCreateInputModel
            {
                Id = id,
                FirstName = "Иван",
                LastName = "Иванов",
                PhoneNumber = "0888777444",
                Salary = 1000m,
                RoleName = GlobalConstants.OperatorRoleName,
            };

            var employeeFromDbFirst = await service.CreateAsync(employee);
            await service.DeleteByIdAsync(employeeFromDbFirst.Id);
            var employeeFromDb = await service.CreateAsync(employee);
            Assert.NotEqual(employeeFromDb.Id, employeeFromDbFirst.Id);
            Assert.Equal(employeeFromDb.PhoneNumber, employeeFromDbFirst.PhoneNumber);
            Assert.Equal(employeeFromDb.PhoneNumber, employeeFromDbFirst.PhoneNumber);
            Assert.Equal(employee.PhoneNumber, employeeFromDb.PhoneNumber);
        }

        [Fact]
        public async Task CreateAsyncWithExistNumberReturnsError()
        {
            var id = Guid.NewGuid().ToString();
            var user = new CarpetUser
            {
                Id = id,
                FirstName = "Иван",
                LastName = "Иванов",
                PhoneNumber = "0888777444",
                Email = "abv@abv.bg",
                UserName = "abv@abv.bg",
            };
            var roleAdminId = Guid.NewGuid().ToString();
            var role = new CarpetRole { Id = roleAdminId, Name = GlobalConstants.AdministratorRoleName };
            var roleOperatorId = Guid.NewGuid().ToString();
            var roleOperator = new CarpetRole { Id = roleOperatorId, Name = GlobalConstants.OperatorRoleName };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "CreateAsyncWithExistNumberReturnsError")
                .Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Users.Add(user);
            dbContext.Roles.Add(role);
            dbContext.Roles.Add(roleOperator);
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Employee>(dbContext);
            var service = new EmployeesService(repository, dbContext);

            var wrongId = Guid.NewGuid().ToString();

            var employee = new EmployeeCreateInputModel
            {
                Id = id,
                FirstName = "Иван",
                LastName = "Иванов",
                PhoneNumber = "0888777444",
                Salary = 1000m,
                RoleName = GlobalConstants.OperatorRoleName,
            };

            var resultPhoneNumber = await service.CreateAsync(employee);

            var exceptionPhoneNumber = await Assert.ThrowsAsync<ArgumentException>(() => service.CreateAsync(employee));

            Assert.Equal(string.Format(string.Format(EmployeeConstants.ArgumentExceptionPhoneNumberExist, resultPhoneNumber.PhoneNumber)), exceptionPhoneNumber.Message);
        }

        [Fact]
        public async Task CreateAsyncWithWrongUserIdReturnsError()
        {
            var id = Guid.NewGuid().ToString();
            var user = new CarpetUser
            {
                Id = id,
                FirstName = "Иван",
                LastName = "Иванов",
                PhoneNumber = "0888777444",
                Email = "abv@abv.bg",
                UserName = "abv@abv.bg",
            };
            var roleAdminId = Guid.NewGuid().ToString();
            var role = new CarpetRole { Id = roleAdminId, Name = GlobalConstants.AdministratorRoleName };
            var roleOperatorId = Guid.NewGuid().ToString();
            var roleOperator = new CarpetRole { Id = roleOperatorId, Name = GlobalConstants.OperatorRoleName };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "CreateAsyncWithWrongUserIdReturnsError")
                .Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Users.Add(user);
            dbContext.Roles.Add(role);
            dbContext.Roles.Add(roleOperator);
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Employee>(dbContext);
            var service = new EmployeesService(repository, dbContext);

            var wrongId = Guid.NewGuid().ToString();

            var employee = new EmployeeCreateInputModel
            {
                Id = wrongId,
                FirstName = "Иван",
                LastName = "Иванов",
                PhoneNumber = "0888777444",
                Salary = 1000m,
                RoleName = GlobalConstants.OperatorRoleName,
            };

            var exceptionUserId = await Assert.ThrowsAsync<NullReferenceException>(() => service.CreateAsync(employee));

            Assert.Equal(string.Format(EmployeeConstants.NullReferenceUserId, employee.Id), exceptionUserId.Message);
        }

        [Fact]
        public async Task CreateAsyncWithWrongRoleReturnsError()
        {
            var id = Guid.NewGuid().ToString();
            var user = new CarpetUser
            {
                Id = id,
                FirstName = "Иван",
                LastName = "Иванов",
                PhoneNumber = "0888777444",
                Email = "abv@abv.bg",
                UserName = "abv@abv.bg",
            };
            var roleAdminId = Guid.NewGuid().ToString();
            var role = new CarpetRole { Id = roleAdminId, Name = GlobalConstants.AdministratorRoleName };
            var roleOperatorId = Guid.NewGuid().ToString();
            var roleOperator = new CarpetRole { Id = roleOperatorId, Name = GlobalConstants.OperatorRoleName };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "CreateAsyncWithWrongRoleReturnsError")
                .Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Users.Add(user);
            dbContext.Roles.Add(role);
            dbContext.Roles.Add(roleOperator);
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Employee>(dbContext);
            var service = new EmployeesService(repository, dbContext);

            var wrongId = Guid.NewGuid().ToString();

            var employeeWrongRole = new EmployeeCreateInputModel
            {
                Id = id,
                FirstName = "Иван",
                LastName = "Иванов",
                PhoneNumber = "0111777444",
                Salary = 1000m,
                RoleName = "Wrong",
            };

            var exceptionRole = await Assert.ThrowsAsync<ArgumentNullException>(() => service.CreateAsync(employeeWrongRole));

            Assert.Equal(string.Format(EmployeeConstants.ArgumentExceptionRoleNotExist, employeeWrongRole.RoleName), exceptionRole.Message);
        }

        [Fact]
        public async Task EditByIdAsyncWithWrongIdReturnsError()
        {
            var id = Guid.NewGuid().ToString();
            var user = new CarpetUser
            {
                Id = id,
                FirstName = "Иван",
                LastName = "Иванов",
                PhoneNumber = "0888777444",
                Email = "abv@abv.bg",
                UserName = "abv@abv.bg",
            };
            var roleAdminId = Guid.NewGuid().ToString();
            var role = new CarpetRole { Id = roleAdminId, Name = GlobalConstants.AdministratorRoleName };
            var roleOperatorId = Guid.NewGuid().ToString();
            var roleOperator = new CarpetRole { Id = roleOperatorId, Name = GlobalConstants.OperatorRoleName };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "EditByIdAsyncWithWrongIdReturnsError")
                .Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Users.Add(user);
            dbContext.Roles.Add(role);
            dbContext.Roles.Add(roleOperator);
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Employee>(dbContext);
            var service = new EmployeesService(repository, dbContext);

            var wrongId = Guid.NewGuid().ToString();

            var employee = new EmployeeCreateInputModel
            {
                Id = id,
                FirstName = "Иван",
                LastName = "Иванов",
                PhoneNumber = "0888777444",
                Salary = 1000m,
                RoleName = GlobalConstants.OperatorRoleName,
            };

            var employeeEdit = new EmployeeEditInputModel
            {
                Id = wrongId,
                FirstName = "Иван",
                LastName = "Иванов",
                PhoneNumber = "0888777444",
                Salary = 1000m,
                RoleName = GlobalConstants.OperatorRoleName,
            };

            var result = await service.CreateAsync(employee);

            var exception = await Assert.ThrowsAsync<NullReferenceException>(() => service.EditByIdAsync(wrongId, employeeEdit));

            Assert.Equal(string.Format(string.Format(EmployeeConstants.NullReferenceId, wrongId)), exception.Message);
        }

        [Fact]
        public async Task EditByIdAsyncWithWrongUserReturnsError()
        {
            var id = Guid.NewGuid().ToString();
            var user = new CarpetUser
            {
                Id = id,
                FirstName = "Иван",
                LastName = "Иванов",
                PhoneNumber = "0888777444",
                Email = "abv@abv.bg",
                UserName = "abv@abv.bg",
            };
            var roleAdminId = Guid.NewGuid().ToString();
            var role = new CarpetRole { Id = roleAdminId, Name = GlobalConstants.AdministratorRoleName };
            var roleOperatorId = Guid.NewGuid().ToString();
            var roleOperator = new CarpetRole { Id = roleOperatorId, Name = GlobalConstants.OperatorRoleName };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "EditByIdAsyncWithWrongUserReturnsError")
                .Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Users.Add(user);
            dbContext.Roles.Add(role);
            dbContext.Roles.Add(roleOperator);
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Employee>(dbContext);
            var service = new EmployeesService(repository, dbContext);

            var wrongId = Guid.NewGuid().ToString();

            var employee = new EmployeeCreateInputModel
            {
                Id = id,
                FirstName = "Иван",
                LastName = "Иванов",
                PhoneNumber = "0888777444",
                Salary = 1000m,
                RoleName = GlobalConstants.OperatorRoleName,
            };

            var result = await service.CreateAsync(employee);

            var employeeEdit = new EmployeeEditInputModel
            {
                Id = result.Id,
                FirstName = "Иван",
                LastName = "Иванов",
                PhoneNumber = "0111777444",
                Salary = 1000m,
                RoleName = GlobalConstants.OperatorRoleName,
            };

            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => service.EditByIdAsync(result.Id, employeeEdit));

            Assert.Equal(string.Format(EmployeeConstants.ArgumentExceptionPhoneNumberNotExist, employeeEdit.PhoneNumber), exception.Message);
        }

        [Fact]
        public async Task EditByIdAsyncWithWrongRoleReturnsError()
        {
            var id = Guid.NewGuid().ToString();
            var user = new CarpetUser
            {
                Id = id,
                FirstName = "Иван",
                LastName = "Иванов",
                PhoneNumber = "0888777444",
                Email = "abv@abv.bg",
                UserName = "abv@abv.bg",
            };
            var roleAdminId = Guid.NewGuid().ToString();
            var role = new CarpetRole { Id = roleAdminId, Name = GlobalConstants.AdministratorRoleName };
            var roleOperatorId = Guid.NewGuid().ToString();
            var roleOperator = new CarpetRole { Id = roleOperatorId, Name = GlobalConstants.OperatorRoleName };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "EditByIdAsyncWithWrongRoleReturnsError")
                .Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Users.Add(user);
            dbContext.Roles.Add(role);
            dbContext.Roles.Add(roleOperator);
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Employee>(dbContext);
            var service = new EmployeesService(repository, dbContext);

            var wrongId = Guid.NewGuid().ToString();

            var employee = new EmployeeCreateInputModel
            {
                Id = id,
                FirstName = "Иван",
                LastName = "Иванов",
                PhoneNumber = "0888777444",
                Salary = 1000m,
                RoleName = GlobalConstants.OperatorRoleName,
            };

            var result = await service.CreateAsync(employee);

            var employeeEdit = new EmployeeEditInputModel
            {
                Id = result.Id,
                FirstName = "Иван",
                LastName = "Иванов",
                PhoneNumber = "0888777444",
                Salary = 1000m,
                RoleName = "Wrong",
            };

            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => service.EditByIdAsync(result.Id, employeeEdit));

            Assert.Equal(string.Format(EmployeeConstants.ArgumentExceptionRoleNotExist, employeeEdit.RoleName), exception.Message);
        }

        [Fact]
        public async Task EditByIdAsyncReturnsCorrect()
        {
            var id = Guid.NewGuid().ToString();
            var user = new CarpetUser
            {
                Id = id,
                FirstName = "Иван",
                LastName = "Иванов",
                PhoneNumber = "0888777444",
                Email = "abv@abv.bg",
                UserName = "abv@abv.bg",
            };
            var roleAdminId = Guid.NewGuid().ToString();
            var role = new CarpetRole { Id = roleAdminId, Name = GlobalConstants.AdministratorRoleName };
            var roleOperatorId = Guid.NewGuid().ToString();
            var roleOperator = new CarpetRole { Id = roleOperatorId, Name = GlobalConstants.OperatorRoleName };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "EditByIdAsyncReturnsCorrect")
                .Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Users.Add(user);
            dbContext.Roles.Add(role);
            dbContext.Roles.Add(roleOperator);
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Employee>(dbContext);
            var service = new EmployeesService(repository, dbContext);

            var wrongId = Guid.NewGuid().ToString();

            var employee = new EmployeeCreateInputModel
            {
                Id = id,
                FirstName = "Иван",
                LastName = "Иванов",
                PhoneNumber = "0888777444",
                Salary = 1000m,
                RoleName = GlobalConstants.OperatorRoleName,
            };

            var result = await service.CreateAsync(employee);

            var employeeEdit = new EmployeeEditInputModel
            {
                Id = result.Id,
                FirstName = "Edit",
                LastName = "Иванов",
                PhoneNumber = "0888777444",
                Salary = 1000m,
                RoleName = GlobalConstants.OperatorRoleName,
            };

            var actual = await service.EditByIdAsync(result.Id, employeeEdit);

            Assert.Equal(actual.FirstName, employeeEdit.FirstName);
        }

        [Fact]
        public async Task DeleteByIdAsyncWithWrongIdReturnsError()
        {
            var id = Guid.NewGuid().ToString();
            var user = new CarpetUser
            {
                Id = id,
                FirstName = "Иван",
                LastName = "Иванов",
                PhoneNumber = "0888777444",
                Email = "abv@abv.bg",
                UserName = "abv@abv.bg",
            };
            var roleAdminId = Guid.NewGuid().ToString();
            var role = new CarpetRole { Id = roleAdminId, Name = GlobalConstants.AdministratorRoleName };
            var roleOperatorId = Guid.NewGuid().ToString();
            var roleOperator = new CarpetRole { Id = roleOperatorId, Name = GlobalConstants.OperatorRoleName };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "DeleteByIdAsyncWithWrongIdReturnsError")
                .Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Users.Add(user);
            dbContext.Roles.Add(role);
            dbContext.Roles.Add(roleOperator);
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Employee>(dbContext);
            var service = new EmployeesService(repository, dbContext);

            var wrongId = Guid.NewGuid().ToString();

            var employee = new EmployeeCreateInputModel
            {
                Id = id,
                FirstName = "Иван",
                LastName = "Иванов",
                PhoneNumber = "0888777444",
                Salary = 1000m,
                RoleName = GlobalConstants.OperatorRoleName,
            };

            var employeeDelete = new EmployeeDeleteInputModel
            {
                Id = wrongId,
            };

            var result = await service.CreateAsync(employee);

            var exception = await Assert.ThrowsAsync<NullReferenceException>(() => service.DeleteByIdAsync(employeeDelete.Id));

            Assert.Equal(string.Format(string.Format(EmployeeConstants.NullReferenceId, wrongId)), exception.Message);
        }

        [Fact]
        public async Task DeleteByIdAsyncReturnsCorrect()
        {
            var id = Guid.NewGuid().ToString();
            var user = new CarpetUser
            {
                Id = id,
                FirstName = "Иван",
                LastName = "Иванов",
                PhoneNumber = "0888777444",
                Email = "abv@abv.bg",
                UserName = "abv@abv.bg",
            };
            var roleAdminId = Guid.NewGuid().ToString();
            var role = new CarpetRole { Id = roleAdminId, Name = GlobalConstants.AdministratorRoleName };
            var roleOperatorId = Guid.NewGuid().ToString();
            var roleOperator = new CarpetRole { Id = roleOperatorId, Name = GlobalConstants.OperatorRoleName };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "DeleteByIdAsyncReturnsCorrect")
                .Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Users.Add(user);
            dbContext.Roles.Add(role);
            dbContext.Roles.Add(roleOperator);
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Employee>(dbContext);
            var service = new EmployeesService(repository, dbContext);

            var wrongId = Guid.NewGuid().ToString();

            var employee = new EmployeeCreateInputModel
            {
                Id = id,
                FirstName = "Иван",
                LastName = "Иванов",
                PhoneNumber = "0888777444",
                Salary = 1000m,
                RoleName = GlobalConstants.OperatorRoleName,
            };

            var result = await service.CreateAsync(employee);

            var actual = await service.DeleteByIdAsync(result.Id);

            var employees = await service.GetAllAsync<EmployeeIndexViewModel>().ToListAsync();

            Assert.Equal(result.Id, actual.Id);
            Assert.Empty(employees);
        }

        [Fact]
        public async Task GetByIdAsyncReturnsCorrect()
        {
            var id = Guid.NewGuid().ToString();
            var user = new CarpetUser
            {
                Id = id,
                FirstName = "Иван",
                LastName = "Иванов",
                PhoneNumber = "0888777444",
                Email = "abv@abv.bg",
                UserName = "abv@abv.bg",
            };
            var roleAdminId = Guid.NewGuid().ToString();
            var role = new CarpetRole { Id = roleAdminId, Name = GlobalConstants.AdministratorRoleName };
            var roleOperatorId = Guid.NewGuid().ToString();
            var roleOperator = new CarpetRole { Id = roleOperatorId, Name = GlobalConstants.OperatorRoleName };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetByIdAsyncReturnsCorrect")
                .Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Users.Add(user);
            dbContext.Roles.Add(role);
            dbContext.Roles.Add(roleOperator);
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Employee>(dbContext);
            var service = new EmployeesService(repository, dbContext);

            var employee = new EmployeeCreateInputModel
            {
                Id = id,
                FirstName = "Иван",
                LastName = "Иванов",
                PhoneNumber = "0888777444",
                Salary = 1000m,
                RoleName = GlobalConstants.OperatorRoleName,
            };

            var result = await service.CreateAsync(employee);

            var actual = await service.GetByIdAsync<EmployeeIndexViewModel>(result.Id);

            var employees = await service.GetAllAsync<EmployeeIndexViewModel>().ToListAsync();

            Assert.Equal(result.Id, actual.Id);
            Assert.Single(employees);
        }

        [Fact]
        public async Task GetByUsernameAsyncReturnsCorrect()
        {
            var id = Guid.NewGuid().ToString();
            var user = new CarpetUser
            {
                Id = id,
                FirstName = "Иван",
                LastName = "Иванов",
                PhoneNumber = "0888777444",
                Email = "abv@abv.bg",
                UserName = "abv@abv.bg",
            };
            var roleAdminId = Guid.NewGuid().ToString();
            var role = new CarpetRole { Id = roleAdminId, Name = GlobalConstants.AdministratorRoleName };
            var roleOperatorId = Guid.NewGuid().ToString();
            var roleOperator = new CarpetRole { Id = roleOperatorId, Name = GlobalConstants.OperatorRoleName };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetByUsernameAsyncReturnsCorrect")
                .Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Users.Add(user);
            dbContext.Roles.Add(role);
            dbContext.Roles.Add(roleOperator);
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Employee>(dbContext);
            var service = new EmployeesService(repository, dbContext);

            var employee = new EmployeeCreateInputModel
            {
                Id = id,
                FirstName = "Иван",
                LastName = "Иванов",
                PhoneNumber = "0888777444",
                Salary = 1000m,
                RoleName = GlobalConstants.OperatorRoleName,
            };

            var result = await service.CreateAsync(employee);

            var username = await dbContext.Employees.FirstOrDefaultAsync(x => x.Id == result.Id);

            var actual = await service.GetIdByUserNameAsync(username.User.UserName);

            var employees = await service.GetAllAsync<EmployeeIndexViewModel>().ToListAsync();

            Assert.Equal(result.Id, actual);
            Assert.Single(employees);
        }

        [Fact]
        public async Task GetNotHiredAsyncReturnsCorrect()
        {
            var id = Guid.NewGuid().ToString();
            var user = new CarpetUser
            {
                Id = id,
                FirstName = "Иван",
                LastName = "Иванов",
                PhoneNumber = "0888777444",
                Email = "abv@abv.bg",
                UserName = "abv@abv.bg",
            };
            var roleAdminId = Guid.NewGuid().ToString();
            var role = new CarpetRole { Id = roleAdminId, Name = GlobalConstants.AdministratorRoleName };
            var roleOperatorId = Guid.NewGuid().ToString();
            var roleOperator = new CarpetRole { Id = roleOperatorId, Name = GlobalConstants.OperatorRoleName };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetNotHiredAsyncReturnsCorrect")
                .Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Users.Add(user);
            dbContext.Roles.Add(role);
            dbContext.Roles.Add(roleOperator);
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Employee>(dbContext);
            var service = new EmployeesService(repository, dbContext);

            var employee = new EmployeeCreateInputModel
            {
                Id = id,
                FirstName = "Иван",
                LastName = "Иванов",
                PhoneNumber = "0888777444",
                Salary = 1000m,
                RoleName = GlobalConstants.OperatorRoleName,
            };

            var actual = await service.GetNotHiredUserAsync(id);

            Assert.Equal(id, actual.Id);
        }
    }
}
