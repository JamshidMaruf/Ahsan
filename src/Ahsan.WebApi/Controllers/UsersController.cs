using Ahsan.Service.DTOs.Users;
using Ahsan.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ahsan.WebApi.Controllers;

#pragma warning disable
public class UsersController : BaseController
{
    private readonly IUserService userService;
    public UsersController(IUserService userService)
    {
        this.userService = userService;
    }

    /// <summary>
    /// Create user
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>

    [Authorize(Roles = "Admin")]
    [HttpPost("create")]
    public async Task<IActionResult> PostCompanyAsync(UserForCreationDto dto)
       => Ok(new
       {
           Code = 200,
           Error = "Success",
           Data = await this.userService.CreateAsync(dto)
       });

    /// <summary>
    /// Update user
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPut("update")]
    public async Task<IActionResult> PutCompanyAsync(UserForUpdateDto dto)
        => Ok(new
        {
            Code = 200,
            Error = "Success",
            Data = await this.userService.UpdateAsync(dto)
        });

    /// <summary>
    /// Delete user via given id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    
    [Authorize(Roles = "Admin")]
    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteCompany(long id)
        => Ok(new
        {
            Code = 200,
            Error = "Success",
            Data = await this.userService.DeleteAsync(id)
        });

    /// <summary>
    /// Get user for given id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("get-by-id/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(new
        {
            Code = 200,
            Error = "Success",
            Data = await this.userService.GetByIdAsync(id)
        });

    /// <summary>
    /// Get user list
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "User")]
    [HttpGet("get-list")]
    public async Task<IActionResult> GetAllUsers()
        => Ok(new
        {
            Code = 200,
            Error = "Success",
            Data = await this.userService.GetAllAsync()
        });

    /// <summary>
    /// Upload user profile image
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost("image-upload")]
    public async ValueTask<IActionResult> UploadImage([FromForm] UserImageForCreationDto dto)
        => Ok(new
        {
            Code = 200,
            Error = "Success",
            Data = await this.userService.ImageUploadAsync(dto)
        });

    /// <summary>
    /// Delete user profile image
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpDelete("image-delete/{userId:long}")]
    public async Task<IActionResult> DeleteUserImage(long userId)
        => Ok(new
        {
            Code = 200,
            Error = "Success",
            Data = await this.userService.DeleteUserImageAsync(userId)
        });

    /// <summary>
    /// Get user profile image
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpGet("image-get/{userId:long}")]
    public async Task<IActionResult> GetUserImage(long userId)
        => Ok(new
        {
            Code = 200,
            Error = "Success",
            Data = await this.userService.GetUserImageAsync(userId)
        });
}
