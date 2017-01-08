namespace BusinessLogic.Interfaces.Credentials
{
    public interface ITokenValidator
    {
        bool IsValid(string token);
        int GetTokenTimeToLive();
    }
}