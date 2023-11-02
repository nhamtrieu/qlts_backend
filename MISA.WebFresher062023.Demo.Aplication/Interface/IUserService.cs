using MISA.WebFresher062023.Demo.Domain;

namespace MISA.WebFresher062023.Demo.Application
{
    public interface IUserService : IReadOnlyService<Guid, UserDto>
    {
        /// <summary>
        /// hàm check username và password
        /// </summary>
        /// <param name="UserDto"></param>
        /// <returns></returns>
        /// Createdby: ttnham (02/10/2023)
        Task<UserReturnDto> CheckUserLoginAsync(UserDto UserDto);

        /// <summary>
        /// hàm tạo token mới
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        /// Createdby: ttnham (02/10/2023)
        string GenerateJwtToken(string userName);

        /// <summary>
        /// hàm map dữ liệu
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// Createdby: ttnham (02/10/2023)
        UserDto MapUserToUserDto(User user);

        /// <summary>
        /// hàm map dữ liệu
        /// </summary>
        /// <param name="UserDto"></param>
        /// <returns></returns>
        /// Createdby: ttnham (02/10/2023)
        User MapUserDtoToUser(UserDto UserDto);
    }
}
