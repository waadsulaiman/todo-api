﻿using Microsoft.AspNetCore.Mvc;
using Todo.Api.Data.Data.Models.Login;
using Todo.Api.Data.Data.Models.Register;
using Todo.Api.Data.Data.Models.Token;
using Todo.Api.Data.Services;

namespace Todo.Api.Rest.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _service;

        public AuthController(AuthService service)
        {
            _service = service;
        }

        #region SignInAsync
        /// <summary>
        /// Used for signing a user in.
        /// </summary>
        /// <param name="request">Represents the required data for signing a user in.</param>
        /// <returns>
        /// A <see cref="Token"/> if operation was a success.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> SignInAsync([FromBody] LoginRequest request)
        {
            var result = await _service.SignInAsync(request);

            if (result.Success)
            {
                return Ok(result.Result);
            }

            return result.Failure.ErrorCode switch
            {
                StatusCodes.Status404NotFound => NotFound(result.Failure.ErrorMessage),
                _ => BadRequest(result.Failure.ErrorMessage),
            };
        }
        #endregion

        #region RegisterAsync
        /// <summary>
        /// Used for registering a user.
        /// </summary>
        /// <param name="request">Represents the required data for registering a user.</param>
        /// <returns>
        /// A <see cref="Token"/> if operation was a success.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request)
        {
            var result = await _service.RegisterAsync(request);

            if (result.Success)
            {
                return new ObjectResult(result.Result)
                {
                    StatusCode = StatusCodes.Status200OK,
                };
            }

            return BadRequest(result.Failure.ErrorMessage);
        }
        #endregion
    }
}
