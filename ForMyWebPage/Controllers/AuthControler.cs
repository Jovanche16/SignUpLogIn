using ForMyWebPage.Models.Requests;
using ForMyWebPage.Models.Responses;
using ForMyWebPage.Repositories.IRepositories;
using ForMyWebPage.Services;
using ForMyWebPage.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Cors;

namespace AlbumApp.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("corsPolicy")]
    [ApiController]
    public class AuthControler : ControllerBase
    {
        private readonly UserManager<User> _userRepository;
        private readonly AccessTokenGenerator _accesTokenGenerator;
        private readonly RefreshTokenGenerator _refreshTokenGenerator;
        private readonly RefreshTokenValidator _refreshTokenValidator;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        public AuthControler(UserManager<User> userRepository, AccessTokenGenerator accessTokenGenerator, RefreshTokenGenerator refreshTokenGenerator,
            RefreshTokenValidator refreshTokenValidator, IRefreshTokenRepository refreshTokenRepository)
        {
            _userRepository = userRepository;
            _accesTokenGenerator = accessTokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
            _refreshTokenValidator = refreshTokenValidator;
            _refreshTokenRepository = refreshTokenRepository;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(request.Password !=request.ConfirmPassword)
            {
                return BadRequest("Passwords doesn't match!");
            }
            Random r = new Random();
            User registrationUser = new User()
            {
               
                Id = r.Next(123),
                Email = request.EmailAddress,
                UserName = request.Username
            };
             IdentityResult result = await _userRepository.CreateAsync(registrationUser, request.Password);
            if (!result.Succeeded)
            {
                IdentityErrorDescriber errorDescriber = new IdentityErrorDescriber();
                IdentityError primaryEror = result.Errors.FirstOrDefault();
                if(primaryEror.Code == nameof(errorDescriber.DuplicateEmail))
                {
                    return Conflict();
                }
                if (primaryEror.Code == nameof(errorDescriber.DuplicateUserName))
                {
                    return Conflict();
                }
            }

            return Ok();
        }

        [HttpPost("logIn")]
        public async Task<IActionResult> LogIn (LoginRequest request)
        {
            User user = await _userRepository.FindByNameAsync(request.Username);
            if(user ==null)
            {
                return Unauthorized();
            }
            bool isCorectPassword = await _userRepository.CheckPasswordAsync(user, request.Password);

            if(!isCorectPassword)
            {
                return Unauthorized();
            }
            string accessToken = _accesTokenGenerator.GenerateToken(user);
            string refreshToken = _refreshTokenGenerator.RefreshToken(user);
            RefreshTokenDto refreshTokenDTO = new RefreshTokenDto()
            {
                Token = refreshToken,
                UserId = user.Id
            };
            await _refreshTokenRepository.Create(refreshTokenDTO);

            return Ok(new AuthenticatedUserResponse()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(RefreshRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bool isValidrefreshToken = _refreshTokenValidator.Validate(request.RefreshToken);
            if(!isValidrefreshToken)
            {
                BadRequest("This is not refresh token!");
            }
            RefreshTokenDto refreshTokenDTO = await _refreshTokenRepository.GetByToken(request.RefreshToken);
            if(refreshTokenDTO == null)
            {
                return NotFound();
            }
            User user = await _userRepository.FindByIdAsync(refreshTokenDTO.UserId.ToString());
            if(user == null)
            {
                return NotFound();
            }
            await _refreshTokenRepository.Delete(refreshTokenDTO.Id);

            string accessToken = _accesTokenGenerator.GenerateToken(user);
            string refreshToken = _refreshTokenGenerator.RefreshToken(user);
            RefreshTokenDto refreshTokenDTO1 = new RefreshTokenDto()
            {
                Token = refreshToken,
                UserId = user.Id
            };
            await _refreshTokenRepository.Create(refreshTokenDTO1);

            return Ok(new AuthenticatedUserResponse()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }

    }
}
