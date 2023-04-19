//using Ahsan.Data.IRepositories;
//using Ahsan.Domain.Entities;
//using Ahsan.Service.DTOs.Positions;
//using Ahsan.Service.Exceptions;
//using Ahsan.Service.Interfaces;
//using AutoMapper;
//using System.Linq.Expressions;

//#pragma warning disable
//namespace Ahsan.Service.Services;

//public class PositionService : IPositionService
//{
//    private readonly IMapper mapper;
//    private readonly IRepository<Position> _positionRepository;
//    public PositionService(IRepository<Position> repository, IMapper mapper)
//    {
//        this.mapper = mapper;
//        this._positionRepository = repository;
//    }

//    public async ValueTask<PositionForResultDto> CreateAsync(PositionForCreationDto dto)
//    {
//        Position position = await this._positionRepository
//            .GetAsync(u => u.Name.ToLower() == dto.Name.ToLower());
//        if (position is not null)
//            throw new CustomException(403, "Position already exist with this name");

//        var mappedPosition = mapper.Map<Position>(dto);
//        var result = await this._positionRepository.InsertAsync(mappedPosition);
//        await this._positionRepository.SaveChangesAsync();
//        return this.mapper.Map<PositionForResultDto>(result);
//    }

//    public async ValueTask<bool> DeleteAsync(long id)
//    {
//        var position = await this._positionRepository.GetAsync(position => position.Id.Equals(id));
//        if (position is null)
//            throw new CustomException(404, "Position not found");

//        await this._positionRepository.DeleteAsync(position);
//        await this._positionRepository.SaveChangesAsync();
//        return true;
//    }

//    public async ValueTask<IEnumerable<PositionForResultDto>> GetAllAsync(
//        Expression<Func<Position, bool>> expression = null, string search = null)
//    {
//        var positions = _positionRepository.GetAll(expression, isTracking: false);
//        var result = mapper.Map<IEnumerable<PositionForResultDto>>(positions);
//        if (string.IsNullOrEmpty(search))
//            return result
//                .Where(u => u.Name.ToLower().Contains(search.ToLower())).ToList();
//        return result;
//    }

//    public async ValueTask<PositionForResultDto> GetByIdAsync(long id)
//    {
//        var position = await this._positionRepository.GetAsync(position => position.Id.Equals(id));
//        if (position is null)
//            throw new CustomException(404, "Position not found");
//        return mapper.Map<PositionForResultDto>(position);
//    }

//    public async ValueTask<PositionForResultDto> UpdateAsync(PositionForUpdateDto dto)
//    {
//        var updatingPosition = await this._positionRepository.GetAsync(p => p.Id.Equals(dto.Id));
//        if (updatingPosition is null)
//            throw new CustomException(404, "Position not found");

//        this.mapper.Map(dto, updatingPosition);
//        updatingPosition.UpdatedAt = DateTime.UtcNow;
//        await this._positionRepository.SaveChangesAsync();
//        return mapper.Map<PositionForResultDto>(updatingPosition);
//    }
//}
