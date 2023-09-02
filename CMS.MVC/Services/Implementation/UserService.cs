using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CMS.Data.Context;
using CMS.Data.Dto;
using CMS.Data.Entities;
using CMS.Data.Enum;
using CMS.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


namespace CMS.MVC.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signinManager;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public UserService(AppDbContext context, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signinManager, IMapper mapper,
            IConfiguration config)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signinManager = signinManager;
            _config = config;
            _mapper = mapper;
        }


        public async Task<ResponseDTO<bool>> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new ResponseDTO<bool>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    DisplayMessage = "User not found",
                };
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                var errorMessages = result.Errors.Select(e => e.Description).ToList();
                return new ResponseDTO<bool>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    DisplayMessage = "Failed to delete user",
                };
            }

            return new ResponseDTO<bool>
            {
                StatusCode = StatusCodes.Status200OK,
                DisplayMessage = "User deleted successfully",
                Result = true
            };
        }

        public async Task<ResponseDTO<bool>> SetActiveStatus(string userId, bool status)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new ResponseDTO<bool>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    DisplayMessage = "User not found",
                };
            }

            user.ActiveStatus = status;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                var errorMessages = result.Errors.Select(e => e.Description).ToList();
                return new ResponseDTO<bool>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    DisplayMessage = "Failed to update active status",
                };
            }

            return new ResponseDTO<bool>
            {
                StatusCode = StatusCodes.Status200OK,
                DisplayMessage = "Active status updated successfully",
                Result = true
            };
        }

        public async Task<bool> GrantPermission(string userId, Permissions claims)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var newClaim = new Claim(claims.ToString(), claims.ToString());

            var result = await _userManager.AddClaimAsync(user, newClaim);

            if (!result.Succeeded)
            {
                throw new Exception("Failed to request permission.");
            }

            return true;
        }

        #region DeleteFileAsync

        public async Task<ResponseDTO<bool>> DeleteFileAsync(string publicId, string email)
        {
            var response = new ResponseDTO<bool>();

            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    throw new ArgumentNullException(nameof(email));
                }

                var findContact = await _userManager.FindByEmailAsync(email);
                if (findContact == null)
                {
                    throw new ArgumentNullException($"User with the email {email} does not exist");
                }

                var account = new Account
                {
                    ApiKey = _config.GetSection("Cloudinary:ApiKey").Value,
                    ApiSecret = _config.GetSection("Cloudinary:ApiSecret").Value,
                    Cloud = _config.GetSection("Cloudinary:CloudName").Value
                };

                var cloudinary = new Cloudinary(account);

                var deletionParams = new DeletionParams(publicId);

                var result = await cloudinary.DestroyAsync(deletionParams);

                if (result != null)
                {
                    response.StatusCode = StatusCodes.Status200OK;
                    response.DisplayMessage = "Image was successfully deleted";
                    response.Result = true;
                    return response;
                }

                response.StatusCode = StatusCodes.Status400BadRequest;
                response.DisplayMessage = "Image failed to delete";
                response.Result = false;
                return response;


            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCodes.Status401Unauthorized;
                return response;
            }
        }

        #endregion


        #region UploadFileAsync
        public async Task<ResponseDTO<Dictionary<string, string>>> UploadFileAsync(IFormFile file, string email)
        {
            var response = new ResponseDTO<Dictionary<string, string>>();
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    throw new ArgumentNullException(nameof(email));
                }

                var findContact = await _userManager.FindByEmailAsync(email);
                if (findContact == null)
                {
                    throw new ArgumentNullException($"User with the email {email} does not exist");
                }

                var account = new Account
                {
                    ApiKey = _config.GetSection("Cloudinary:ApiKey").Value,
                    ApiSecret = _config.GetSection("Cloudinary:ApiSecret").Value,
                    Cloud = _config.GetSection("Cloudinary:CloudName").Value

                };

                var cloudinary = new Cloudinary(account);

                if (file.Length is > 0 and <= 1024 * 1024 * 2)
                {
                    if (file.ContentType.Equals("image/jpeg") || file.ContentType.Equals("image/png") ||
                        file.ContentType.Equals("image/jpg"))
                    {
                        UploadResult uploadResult;
                        await using (var stream = file.OpenReadStream())
                        {
                            var uploadParameters = new ImageUploadParams
                            {
                                File = new FileDescription(file.FileName, stream),
                                Transformation = new Transformation().Width(300).Height(300).Crop("fill")
                                    .Gravity("face")
                            };

                            uploadResult = await cloudinary.UploadAsync(uploadParameters);
                        }

                        var result = new Dictionary<string, string>
                        {
                            { "PublicId", uploadResult.PublicId },
                            { "Url", uploadResult.Url.ToString() }
                        };
                        response.Result = result;
                        response.DisplayMessage = "Image was uploaded successfully!";
                        response.StatusCode = StatusCodes.Status200OK;
                        return response;
                    }
                    else
                    {
                        response.StatusCode = StatusCodes.Status401Unauthorized;
                        return response;
                    }
                }
                else
                {
                    response.StatusCode = StatusCodes.Status401Unauthorized;
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCodes.Status401Unauthorized;
                return response;

            }
        }
        #endregion


        public async Task<bool> RequestPermission(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var existingClaims = await _userManager.GetClaimsAsync(user);
            if (existingClaims != null)
            {
                foreach (var usersClaim in existingClaims)
                {
                    await _userManager.RemoveClaimAsync(user, usersClaim);
                }

            }

            var newClaim = new Claim(Permissions.can_update.ToString(), Permissions.can_update.ToString());
            var result = await _userManager.AddClaimAsync(user, newClaim);

            if (!result.Succeeded)
            {
                throw new Exception("Failed to request permission.");
            }

            return true;

        }

        public async Task<ResponseDTO<string>> GetUserRoles(string userId)
        {
            var response = new ResponseDTO<string>();
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    response.StatusCode = StatusCodes.Status404NotFound;
                    response.DisplayMessage = $"Not successful";
                    return response;
                }

                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Any())
                {
                    string rolesString = string.Join(", ", roles);
                    response.StatusCode = StatusCodes.Status200OK;
                    response.DisplayMessage = "User roles retrieved successfully";
                    response.Result = rolesString;
                    return response;
                }

                response.StatusCode = StatusCodes.Status204NoContent;
                response.DisplayMessage = "This user has no roles ";
                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCodes.Status400BadRequest;
                response.DisplayMessage = "Bad Request";
                return response;
            }
        }

        public async Task<ResponseDTO<IEnumerable<GetAllUsersDto>>> GetAllUsers()
        {
            var response = new ResponseDTO<IEnumerable<GetAllUsersDto>>();
            try
            {
                var users = await _userManager.Users.ToListAsync();
                if (users == null)
                {
                    response.StatusCode = StatusCodes.Status404NotFound;
                    response.DisplayMessage = "No User Found";
                    return response;
                }

                var usersDtos = _mapper.Map<IEnumerable<GetAllUsersDto>>(users);
                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Operation Successful";
                response.Result = usersDtos;
                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCodes.Status400BadRequest;
                response.DisplayMessage = "Error Occured while getting user";
                return response;

            }

        }

        public async Task<ResponseDTO<GetuserByIdDto>> GetByIDAsync(string Id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(Id);
                if (user == null)
                {
                    return new ResponseDTO<GetuserByIdDto>
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        DisplayMessage = $"User with ID {Id} was not found"
                    };
                }

                var userResponse = _mapper.Map<GetuserByIdDto>(user);
                return new ResponseDTO<GetuserByIdDto>
                {
                    StatusCode = StatusCodes.Status200OK,
                    DisplayMessage = "Successful Operation",
                    Result = userResponse
                };
            }
            catch (Exception ex)
            {

                return new ResponseDTO<GetuserByIdDto>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    DisplayMessage = "Error Occured while getting user",
                };
            }
        }
    }
}
