//using Ahsan.Data.IRepositories;
//using Ahsan.Domain.Entities;
//using Ahsan.Service.DTOs.Users;
//using Ahsan.Service.Exceptions;
//using Ahsan.Service.Interfaces;
//using AutoMapper;
//using Microsoft.EntityFrameworkCore;
//using System.Linq.Expressions;

//namespace Ahsan.Service.Services
//{
//    public class UserService : IUserService
//    {
//        private readonly IRepository<User> userRepository;
//        private readonly IMapper mapper;
//        public UserService(IRepository<User> repository, IMapper mapper)
//        {
//            this.userRepository = repository;
//            this.mapper = mapper;
//        }
//        public async ValueTask<UserForResultDto> CreateAsync(UserForCreationDto dto)
//        {
//            User user = await this.userRepository.GetAsync(u => u.Username.ToLower() == dto.Username.ToLower());

//            if (user is not null)
//            {
//                throw new CustomException(403, "User already exist with this username");
//            }

//            User mappedUser = mapper.Map<User>(dto);

//            try
//            {
//                var result = await this.userRepository.InsertAsync(mappedUser);
//                await this.userRepository.SaveChangesAsync();

//                return this.mapper.Map<UserForResultDto>(result);
//            }

//            catch (Exception)
//            {
//                throw new CustomException(500, "Something went wrong");
//            }
//        }

//        public async ValueTask<bool> DeleteAsync(Expression<Func<User, bool>> expression)
//        {
//            var user = await this.userRepository.GetAsync(expression);

//            if (user is null)
//            {
//                throw new CustomException(404, "User not found");
//            }

//            await userRepository.DeleteAsync(user);

//            await this.userRepository.SaveChangesAsync();

//            return true;
//        }

//        public async ValueTask<IEnumerable<UserForResultDto>> GetAllAsync(Expression<Func<User, bool>> expression = null, string search = null)
//        {
//            var users = userRepository.GetAll(expression, isTracking: false);

//            var matchingUsers = await users.Where(
//                u => u.Firstname.ToLower() == search ||
//                u.Lastname.ToLower() == search ||
//                u.Username.ToLower() == search).ToListAsync();

//            try
//            {
//                var result = mapper.Map<IEnumerable<UserForResultDto>>(matchingUsers);
//                return result;
//            }

//            catch
//            {
//                throw new CustomException(500, "Something went wromg");
//            }
//        }

//        public async ValueTask<UserForResultDto> GetAsync(Expression<Func<User, bool>> expression)
//        {
//            var user = await userRepository.GetAsync(expression);

//            if (user is null)
//                throw new CustomException(404, "User not found");

//            try
//            {
//                var result = mapper.Map<UserForResultDto>(user);
//                return result;
//            }

//            catch
//            {
//                throw new CustomException(500, "Something went wrong");
//            }
//        }

//        public async ValueTask<UserForResultDto> UpdateAsync(long id, UserForUpdateDto dto)
//        {
//            var updatingUser = await userRepository.GetAsync(u => u.Id == id);

//            if (updatingUser is null)
//            {
//                throw new CustomException(404, "User not found");
//            }

//            var user = mapper.Map<User>(dto);

//            user.UpdatedAt = DateTime.UtcNow;

//            await userRepository.UpdateAsync(user);

//            await userRepository.SaveChangesAsync();

//            return mapper.Map<UserForResultDto>(user);
//        }

//        public async ValueTask<UserForResultDto> ChangePasswordAsync(UserForChangePassword dto)
//        {
//            User existUser = await userRepository.GetAsync(u => u.Username == dto.Username);

//            if (existUser is null)
//            {
//                throw new Exception("This username is not exist");
//            }
//            else if (dto.NewPassword != dto.ComfirmPassword)
//            {
//                throw new Exception("New password and confirm password are not equal");
//            }
//            else if (existUser.Password != dto.OldPassword)
//            {
//                throw new Exception("Password is incorrect");
//            }

//            existUser.Password = dto.ComfirmPassword;

//            await userRepository.SaveChangesAsync();

//            return mapper.Map<UserForResultDto>(existUser);
//        }
//    }
//}
