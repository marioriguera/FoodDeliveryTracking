using FoodDeliveryTracking.Data.Contracts;
using FoodDeliveryTracking.Data.Models;
using FoodDeliveryTracking.Models.Request;
using FoodDeliveryTracking.Models.Response;
using FoodDeliveryTracking.Services.Auth;
using FoodDeliveryTracking.Services.Encrypt;
using FoodDeliveryTracking.Services.Logger;
using FoodDeliveryTracking.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryTracking.Controllers
{
    /// <summary>
    /// Controller for handling authentication-related API endpoints.
    /// </summary>
    [ApiController]
    [Route("api/auth")]
    public class AuthenticationController : Controller
    {
        private readonly ILoggerManager _loggerManager;
        private readonly IUsersRepository _usersRepository;
        private readonly ITokenManager _tokenManager;
        private readonly IEncryptService _encryptService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationController"/> class with the provided logger manager and users repository.
        /// </summary>
        /// <param name="loggerManager">The logger manager used for logging.</param>
        /// <param name="usersRepository">The repository for interacting with user data.</param>
        /// <param name="tokenManager">The interface for interacting with token manager.</param>
        /// <param name="encryptService">The interface for interacting with encrypt service.</param>
        public AuthenticationController(ILoggerManager loggerManager, IUsersRepository usersRepository,
                                        ITokenManager tokenManager, IEncryptService encryptService)
        {
            _loggerManager = loggerManager;
            _usersRepository = usersRepository;
            _tokenManager = tokenManager;
            _encryptService = encryptService;
        }

        /// <summary>
        /// HTTP POST action that authenticates a user using the provided credentials.
        /// </summary>
        /// <param name="user">User object that contains the username and password.</param>
        /// <returns>ActionResult object that indicates whether the authentication was successful or not.</returns>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync([FromBody] AuthUserRequest user)
        {
            try
            {
                AuthUserResponse userVerify = new AuthUserResponse(await _usersRepository.LoginUserAsync(user));
                if (userVerify != null)
                {
                    userVerify.Token = _tokenManager.GetToken(userVerify, Program.AppSettings.Secret);

                    await _usersRepository.RegisterTokenUserAsync(userVerify);
                    return Ok(MessageResponse<AuthUserResponse>.Success(userVerify));
                }

                _loggerManager.LogWarn($"Unauthorized user {user.Name} name.");
                return Unauthorized($"Unauthorized user.");
            }
            catch (Exception ex)
            {
                _loggerManager.LogWarn($"Unhandled error when trying to login user {user.Name} name. Message: {ex.Message}");
                return BadRequest(MessageResponse<String>.Fail($"Unhandled error when trying to login user {user.Name} name."));
            }
        }

        // ToDo: Terminar el crud de users.
        // [HttpPost]
        // [Route("insert")]
        // [Authorize]
        // public async Task<IActionResult> AddUserAsync([FromBody] AddAuthUserRequest addAuthUser)
        // {
        //     try
        //     {
        //         if (await _usersRepository.InsertUserAsync(addAuthUser))
        //         {
        // 
        //         }
        //         _loggerManager.LogWarn($"Could not add user. {addAuthUser.Name} name.");
        //         return Conflict(MessageResponse<string>.Fail($"Unauthorized user."));
        //     }
        //     catch (Exception ex)
        //     {
        //         _loggerManager.LogWarn($"Unhandled error when trying to add user {addAuthUser.Name} name. Message: {ex.Message}");
        //         return BadRequest(MessageResponse<String>.Fail($"Unhandled error when trying to add user {addAuthUser.Name} name."));
        //     }
        // }
    }
}
