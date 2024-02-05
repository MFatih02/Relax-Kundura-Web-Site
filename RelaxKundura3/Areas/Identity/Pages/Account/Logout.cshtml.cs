// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using RelaxKundura3.Abstract;
using RelaxKundura3.Services;

namespace RelaxKundura3.Areas.Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;
        private readonly ISepetService _sepetService;

        public LogoutModel(SignInManager<IdentityUser> signInManager, ILogger<LogoutModel> logger, ISepetService sepetService)
        {
            _signInManager = signInManager;
            _logger = logger;
            _sepetService = sepetService;
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            _sepetService.SepetiSil();
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                // This needs to be a redirect so that the browser performs a new
                // request and the identity for the user gets updated.
                return RedirectToPage();
            }
        }
    }
}
