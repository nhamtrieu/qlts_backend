namespace MISA.WebFresher062023.Demo.Domain
{
    public class Department : IEntity<Guid>
    {
        /// <summary>
        /// ID phòng ban
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Mã phòng ban
        /// </summary>
        public string? DepartmentCode { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string? DepartmentName { get; set; }

        /// <summary>
        /// Ngày chỉnh sửa
        /// </summary>
        public DateTimeOffset ModifyDate { get; set; }

        public string GetCode()
        {
            throw new NotImplementedException();
        }

        public Guid GetId()
        {
            return DepartmentId;
        }

        public void SetCode(string code)
        {
            throw new NotImplementedException();
        }

        public void SetId(Guid id)
        {
            DepartmentId = id;
        }
    }
}
