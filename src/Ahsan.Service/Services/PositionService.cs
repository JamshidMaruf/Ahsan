//using Ahsan.Data.IRepositories;
//using Ahsan.Domain.Entities;
//using Ahsan.Service.DTOs.Positions;
//using Ahsan.Service.Exceptions;
//using Ahsan.Service.Interfaces;
//using AutoMapper;
//using Microsoft.EntityFrameworkCore;
//using System.Linq.Expressions;

//namespace Ahsan.Service.Services
//{
//    public class PositionService : IPositionService
//    {
//        private readonly IRepository<Position> _positionRepository;
//        private readonly IMapper mapper;
//        public PositionService(IRepository<Position> repository, IMapper mapper)
//        {
//            this._positionRepository = repository;
//            this.mapper = mapper;
//        }
//        public async ValueTask<PositionForResultDto> CreateAsync(PositionForCreationDto dto)
//        {
//            Position position = await this._positionRepository.GetAsync(u => u.Name.ToLower() == dto.Name.ToLower());

//            if (position is not null)
//            {
//                throw new CustomException(403, "Position already exist with this name");
//            }

//            var mappedPosition = mapper.Map<Position>(dto);

//            try
//            {
//                var result = await this._positionRepository.InsertAsync(mappedPosition);
//                await this._positionRepository.SaveChangesAsync();

//                return this.mapper.Map<PositionForResultDto>(result);
//            }

//            catch (Exception)
//            {
//                throw new CustomException(500, "Something went wrong");
//            }
//        }

//        public async ValueTask<bool> DeleteAsync(Expression<Func<Position, bool>> expression)
//        {
//            var position = await this._positionRepository.GetAsync(expression);

//            if (position is null)
//            {
//                throw new CustomException(404, "Position not found");
//            }

//            await this._positionRepository.DeleteAsync(position);

//            await this._positionRepository.SaveChangesAsync();

//            return true;
//        }

//        public async ValueTask<IEnumerable<PositionForResultDto>> GetAllAsync(Expression<Func<Position, bool>> expression = null, string search = null)
//        {
//            var positions = _positionRepository.GetAll(expression, isTracking: false);

//            var matchingPositions = await positions.Where(u => u.Name.ToLower() == search.ToLower()).ToListAsync();

//            try
//            {
//                var result = mapper.Map<IEnumerable<PositionForResultDto>>(matchingPositions);
//                return result;
//            }

//            catch
//            {
//                throw new CustomException(500, "Something went wromg");
//            }
//        }

//        public async ValueTask<PositionForResultDto> GetAsync(Expression<Func<Position, bool>> expression)
//        {
//            var position = await this._positionRepository.GetAsync(expression);

//            if (position is null)
//                throw new CustomException(404, "Position not found");

//            try
//            {
//                var result = mapper.Map<PositionForResultDto>(position);
//                return result;
//            }

//            catch
//            {
//                throw new CustomException(500, "Something went wrong");
//            }
//        }

//        public async ValueTask<PositionForResultDto> UpdateAsync(long id, PositionForCreationDto dto)
//        {
//            var updatingPosition = await this._positionRepository.GetAsync(u => u.Id == id);

//            if (updatingPosition is null)
//            {
//                throw new CustomException(404, "Position not found");
//            }

//            var position = mapper.Map<Position>(dto);

//            position.UpdatedAt = DateTime.UtcNow;

//            await this._positionRepository.UpdateAsync(position);

//            await this._positionRepository.SaveChangesAsync();

//            return mapper.Map<PositionForResultDto>(position);
//        }
//    }
//}
