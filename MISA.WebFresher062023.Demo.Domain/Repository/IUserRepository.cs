namespace MISA.WebFresher062023.Demo.Domain
{
    public interface IUserRepository : IReadOnlyRepository<User, Guid>
    {
        /// <summary>
        /// Kiểm tra thông tin user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<User> CheckUserInfo(User user);

        /// <summary>
        /// Tìm kiếm thông tin user theo username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Task<User> FindByUserName(string username);
    }
}
