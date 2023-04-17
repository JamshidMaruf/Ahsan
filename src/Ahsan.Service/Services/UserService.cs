using Ahsan.Data.IRepositories;
using Ahsan.Domain.Entities;
using Ahsan.Service.DTOs.Users;
using Ahsan.Service.Exceptions;
using Ahsan.Service.Helpers;
using Ahsan.Service.Interfaces;
using AutoMapper;
using System.Linq.Expressions;

namespace Ahsan.Service.Services;

public class UserService : IUserService
{
    private readonly IRepository<User> userRepository;
    private readonly IMapper mapper;
    public UserService(IRepository<User> repository, IMapper mapper)
    {
        this.userRepository = repository;
        this.mapper = mapper;
    }

    public async ValueTask<UserForResultDto> CreateAsync(UserForCreationDto dto)
    {
        User user = await this.userRepository.GetAsync(u => u.Username.ToLower() == dto.Username.ToLower());
        if (user is not null)
            throw new CustomException(403, "User already exist with this username");

        User mappedUser = mapper.Map<User>(dto);
        var result = await this.userRepository.InsertAsync(mappedUser);
        await this.userRepository.SaveChangesAsync();
        return this.mapper.Map<UserForResultDto>(result);
    }   

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var user = await this.userRepository.GetAsync(u => u.Id.Equals(id));
        if (user is null)
            throw new CustomException(404, "User not found");

        await this.userRepository.DeleteAsync(user);
        await this.userRepository.SaveChangesAsync();
        return true;
    }

    public async ValueTask<IEnumerable<UserForResultDto>> GetAllAsync(
        Expression<Func<User, bool>> expression = null, string search = null)
    {
        var users = userRepository.GetAll(expression, isTracking: false);

        var result = mapper.Map<IEnumerable<UserForResultDto>>(users);
        if (string.IsNullOrEmpty(search))
        {
            return result.Where(
                u => u.Firstname.ToLower().Contains(search.ToLower()) ||
                u.Lastname.ToLower().Contains(search.ToLower()) ||
                u.Username.ToLower().Contains(search.ToLower())).ToList();
        }
        return result;
    }

    public async ValueTask<UserForResultDto> GetByIdAsync(long id)
    {
        var user = await userRepository.GetAsync(u => u.Id.Equals(id));
        if (user is null)
            throw new CustomException(404, "User not found");
        return mapper.Map<UserForResultDto>(user);
    }

    public async ValueTask<UserForResultDto> UpdateAsync(UserForUpdateDto dto)
    {
        var updatingUser = await userRepository.GetAsync(u => u.Id.Equals(dto.Id));
        if (updatingUser is null)
            throw new CustomException(404, "User not found");

        this.mapper.Map(dto, updatingUser);
        updatingUser.UpdatedAt = DateTime.UtcNow;
        await this.userRepository.SaveChangesAsync();
        return mapper.Map<UserForResultDto>(updatingUser);
    }

    public async ValueTask<UserForResultDto> ChangePasswordAsync(UserForChangePassword dto)
    {
        User existUser = await userRepository.GetAsync(u => u.Username == dto.Username);
        if (existUser is null)
            throw new Exception("This username is not exist");
        else if (dto.NewPassword != dto.ComfirmPassword)
            throw new Exception("New password and confirm password are not equal");
        else if (existUser.Password != dto.OldPassword)
            throw new Exception("Password is incorrect");

        existUser.Password = dto.ComfirmPassword;
        await userRepository.SaveChangesAsync();
        return mapper.Map<UserForResultDto>(existUser);
    }

    public async ValueTask ImageUploadAsync(UserForCreationDto dto)
    {
        var fileExtension = Path.GetExtension(dto.Image.FileName);
        var fileName = Guid.NewGuid().ToString("N") + fileExtension;
        var webRootPath = EnvironmentHelper.WebHostPath;
        var folder = Path.Combine(webRootPath, "uploads");
        
        if(!Directory.Exists(folder))
            Directory.CreateDirectory(folder);


    }
}
