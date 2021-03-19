namespace PMG.App.Areas.Identity.Pages.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;
    using PMG.Domain;

    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<PMGUser> _signInManager;
        private readonly UserManager<PMGUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private const string usernameRegexPattern = @"[\w~\*\s]*";

        public RegisterModel(
            UserManager<PMGUser> userManager,
            SignInManager<PMGUser> signInManager,
            ILogger<RegisterModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }
        public class InputModel
        {
            [Required]
            [RegularExpression(usernameRegexPattern, ErrorMessage = "Username can contains letters, digits, *, ~, _,")]
            public string Username { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
            [Required]
            public DateTime DateOfBirth { get; set; }

            [Required]
            public string FirstName { get; set; }

            [Required]
            public string LastName { get; set; }
            [Required]
            public string UCN { get; set; }
            public string Role { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ViewData["Roles"] = new List<string>
            {
                "Teacher",
                "Student"
            };
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new PMGUser
                {
                    UserName = Input.Username,
                    Email = Input.Email,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    UCN = Input.UCN,
                    DateOfBirth = Input.DateOfBirth,
                    Role = Input.Role
                };

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (_userManager.Users.Count() == 1)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                }

                else if (Input.Role == "Teacher")
                {
                    await _userManager.AddToRoleAsync(user, "Teacher");
                }

                else if (Input.Role == "Student")
                {
                    Bookmark bookmark = new Bookmark();
                    user.Bookmark = bookmark;
                    await _userManager.AddToRoleAsync(user, "Student");
                }

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}