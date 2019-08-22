namespace Carpet.Web.Areas.Identity.Pages.Account
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    using Carpet.Common.Constants;
    using Carpet.Data.Common.Repositories;
    using Carpet.Data.Models;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;

    [AllowAnonymous]
#pragma warning disable SA1649 // File name should match first type name
    public class RegisterModel : PageModel
#pragma warning restore SA1649 // File name should match first type name
    {
        private readonly SignInManager<CarpetUser> signInManager;
        private readonly UserManager<CarpetUser> userManager;
        private readonly ILogger<RegisterModel> logger;
        private readonly IEmailSender emailSender;
        private readonly IDeletableEntityRepository<Employee> employeeRepository;

        public RegisterModel(
            UserManager<CarpetUser> userManager,
            SignInManager<CarpetUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IDeletableEntityRepository<Employee> employeeRepository)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.emailSender = emailSender;
            this.employeeRepository = employeeRepository;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public void OnGet(string returnUrl = null)
        {
            this.ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            var isRoot = !this.userManager.Users.Any();

            returnUrl = returnUrl ?? this.Url.Content("~/");
            if (this.ModelState.IsValid)
            {
                var user = new CarpetUser
                {
                    UserName = this.Input.Email,
                    Email = this.Input.Email,
                    FirstName = this.Input.FirstName,
                    LastName = this.Input.LastName,
                    PhoneNumber = this.Input.PhoneNumber,
                };

                var customer = new Customer
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    User = user,
                    PhoneNumber = user.PhoneNumber,
                    PickUpAddress = "Sofia",
                };

                user.Customers.Add(customer);

                var result = await this.userManager.CreateAsync(user, this.Input.Password);
                if (result.Succeeded)
                {
                    if (isRoot)
                    {
                        await this.userManager.AddToRoleAsync(user, GlobalConstants.AdministratorRoleName);

                        var employee = new Employee
                        {
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            User = user,
                            PhoneNumber = user.PhoneNumber,
                            Salary = 1.0m,
                            RoleName = GlobalConstants.AdministratorRoleName,
                        };

                        await this.employeeRepository.AddAsync(employee);

                        await this.employeeRepository.SaveChangesAsync();
                    }

                    this.logger.LogInformation("User created a new account with password.");

                    var code = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = this.Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: this.Request.Scheme);

                    await this.emailSender.SendEmailAsync(
                        this.Input.Email,
                        "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await this.signInManager.SignInAsync(user, isPersistent: false);
                    return this.LocalRedirect(returnUrl);
                }

                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return this.Page();
        }

        public class InputModel
        {
            [Required(ErrorMessage = UserConstants.ErrorFieldRequired)]
            [MinLength(UserConstants.FirstNameMinValue, ErrorMessage = UserConstants.ErrorFieldFirstNameLength)]
            [RegularExpression(UserConstants.NameValidation, ErrorMessage = UserConstants.ErrorFieldFirstNameRegex)]
            [Display(Name = UserConstants.DisplayNameFirstName)]
            public string FirstName { get; set; }

            [Required(ErrorMessage = UserConstants.ErrorFieldRequired)]
            [MinLength(UserConstants.LastNameMinValue, ErrorMessage = UserConstants.ErrorFieldLastNameLength)]
            [RegularExpression(UserConstants.NameValidation, ErrorMessage = UserConstants.ErrorFieldLastNameRegex)]
            [Display(Name = UserConstants.DisplayNameLastName)]
            public string LastName { get; set; }

            [Required(ErrorMessage = UserConstants.ErrorFieldRequired)]
            [RegularExpression(UserConstants.PhoneValidation)]
            [Display(Name = UserConstants.DisplayNamePhoneNumber)]
            public string PhoneNumber { get; set; }

            [Required(ErrorMessage = UserConstants.ErrorFieldRequired)]
            [EmailAddress]
            [Display(Name = UserConstants.DisplayNameEmail)]
            public string Email { get; set; }

            [Required(ErrorMessage = UserConstants.ErrorFieldRequired)]
            [StringLength(UserConstants.PasswordMaxLength, ErrorMessage = UserConstants.ErrorFieldPasswordLength, MinimumLength = UserConstants.PasswordMinLength)]
            [DataType(DataType.Password)]
            [Display(Name = UserConstants.DisplayNamePassword)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = UserConstants.DisplayNameConfirmPassword)]
            [Compare("Password", ErrorMessage = UserConstants.ErrorFieldPasswordMismatch)]
            public string ConfirmPassword { get; set; }
        }
    }
}
