using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Data;
using Microsoft.Extensions.Logging;
using Amazon.Runtime.Internal.Util;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Microsoft.VisualStudio.Web.CodeGeneration;
using ILogger = Amazon.Runtime.Internal.Util.ILogger;

namespace AvisFormationsCore.WebUi.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        public ForgotPasswordModel(UserManager<IdentityUser> userManager, IEmailSender emailSender, ILogger<ForgotPasswordModel> logger)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _logger = (ILogger)logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public ILogger Get_logger()
        {
            return _logger;
        }

        public async Task<IActionResult> OnPostAsync(ILogger _logger)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null)
                {
                    // Aucun utilisateur trouvé avec cet e-mail
                    // Afficher un message d'erreur approprié
                    throw new KeyNotFoundException($"L'user  {Input.Email} n'existe pas.");
                }
                else 
                {
                    //if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                    //{
                    //    // Don't reveal that the user does not exist or is not confirmed
                    //    return RedirectToPage("./ForgotPasswordConfirmation");
                    //}

                    // For more information on how to enable account confirmation and password reset please 
                    // visit https://go.microsoft.com/fwlink/?LinkID=532713

                    //var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                    //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    //var callbackUrl = Url.Page(
                    //    "/Account/ResetPassword",
                    //    pageHandler: null,
                    //    values: new { area = "Identity", code },
                    //    protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(
                    //    Input.Email,
                    //    "Reset Password",
                    //    $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
                    var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                    var callbackUrl = Url.Page(
                        "/Account/ResetPassword",
                        pageHandler: null,
                        values: new { area = "Identity", code },
                        protocol: Request.Scheme);

                    string subject = "Reset Password";
                    string htmlMessage = $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.";

                    try
                    {
                        await _emailSender.SendEmailAsync(
                            Input.Email,
                            subject,
                            htmlMessage);
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex, "An error occurred in ForgotPassword");
                            //LogError(ex, "An error occurred in ForgotPassword");
                        //GetLogger(ex,
                        //"Error sending email to {Email}",
                        //Input.Email);

                        // Optionally, provide feedback to the user
                        // You can set a flag or add a ModelState error
                        ModelState.AddModelError(string.Empty, "An error occurred while sending the email. Please try again later.");
                    }


                    return RedirectToPage("./ForgotPasswordConfirmation");
                }
            }

            return Page();
        }
    }


}
