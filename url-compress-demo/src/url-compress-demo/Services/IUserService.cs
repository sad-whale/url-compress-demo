namespace url_compress_demo.Services
{
    /// <summary>
    /// Интерфейс службы, предоставляющей Id текущего пользователя
    /// </summary>
    public interface IUserService
    {
        string GetCurrentUserId();
    }
}
