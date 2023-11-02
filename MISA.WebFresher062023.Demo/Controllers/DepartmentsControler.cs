using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher062023.Demo.Application;

namespace MISA.WebFresher062023.Demo
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class DepartmentsControler : BaseReadOnlyController<Guid, DepartmentDto>
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentsControler(IDepartmentService readOnlyService) : base(readOnlyService)
        {
            _departmentService = readOnlyService;
        }
    }
}
