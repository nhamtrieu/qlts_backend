using MISA.WebFresher062023.Demo.Application;
using MISA.WebFresher062023.Demo.Domain;

namespace MISA.WebFresher062023.Demo.Infrastructure
{
    public class DepartmentRepository : BaseReadOnlyRepository<Department, Guid>, IDepartmentRepository
    {
        public DepartmentRepository(IUnitOfWork uow) : base(uow)
        {

        }

        public new string TableName = "Department";
    }
}
