namespace ECommerce.Application.Interfaces
{
    public interface IUserRepository
    {
        public string GetUserNameFromUserId(string userId);
    }
}
