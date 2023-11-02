using AutoMapper;
using MISA.WebFresher062023.Demo.Domain;

namespace MISA.WebFresher062023.Demo.Application
{
    public class DepartmentService :BaseReadOnlyService<Department, Guid, DepartmentDto> , IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        public DepartmentService(IDepartmentRepository repository, IMapper mapper) : base(repository)
        {
            _departmentRepository = repository;
            _mapper = mapper;
        }

        public override string IncrementNumberString(string numberString)
        {
            throw new NotImplementedException();
        }

        public override DepartmentDto MapEntityToEntityDto(Department entity)
        {
            var departmentDto = _mapper.Map<DepartmentDto>(entity);
            return departmentDto;
        }
    }
}
