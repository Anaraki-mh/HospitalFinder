using HospitalFinder.API.DTOs;
using HospitalFinder.Services;
using HospitalFinder.WebEssentials;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace HospitalFinder.API.Controllers
{
    [Route("api/Account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        #region Properties and fields

        private IAccountService _accountService { get; }

        #endregion


        #region Constructor

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        #endregion


        #region Methods

        [HttpPost("Signup")]
        public async Task<ActionResult<SignUpResponseDto>> SignUp(SignUpRequestDto signUpModel)
        {
            var signupSuccessful = await _accountService.SignUp(signUpModel.Email, signUpModel.Password);

            if (!signupSuccessful)
            {
                return CreatedAtRoute(nameof(SignUp), new ErrorDto
                {
                    Error = "Invalid email address.",
                    Solution = "Please use a different email address to sign up."
                });
            }

            return CreatedAtRoute(nameof(SignUp), new SignUpResponseDto
            {
                Email= signUpModel.Email,
                PasswordHash = Hash.SHA256(signUpModel.Password),
            });
        }

        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginRequestDto loginModel)
        {
            var isLockedOut = await _accountService.IsIPLockedOut(HttpContext.Connection.RemoteIpAddress.ToString() ?? "");

            if (isLockedOut)
                return Forbid();

            (var user, var loginSuccessful) = await _accountService.Login(loginModel.Email, loginModel.Password);

            if (user is null)
            {
                return NotFound(new ErrorDto
                {
                    Error = "User not found.",
                    Solution = "Please make sure the email is typed correctly."
                });
            }

            if (!loginSuccessful)
            {
                await _accountService.Lockout(HttpContext.Connection.RemoteIpAddress.ToString());
                return Unauthorized(new ErrorDto
                {
                    Error = "Wrong password.",
                    Solution = "Please make sure the password is typed correctly. Entering a wrong password 5 times will result in a 1 hour lockout."
                });
            }

            var token = await _accountService.GenerateToken(user);

            return Ok(new LoginResponseDto
            {
                Token = token,
                TokenExpirationDateTime = DateTime.UtcNow.AddHours(1),
            });
        }

        #endregion

    }
}
