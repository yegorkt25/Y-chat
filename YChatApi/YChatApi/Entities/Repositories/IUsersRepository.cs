namespace YChatApi.Entities.Repositories
{
    public interface IUsersRepository
    {
        Task<User> GetUserByUsernameAsync(string username);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task AddUserAsync(User user);
        Task DeleteUserAsync(int userId);
    }
}
