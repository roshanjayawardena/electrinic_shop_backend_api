namespace Electronic_Application.Contracts.Infastructure.Auth
{
    public interface ICurrentUserService
    {
        Guid UserId { get; }
        string Name { get; }
        bool IsAuthenticated { get; }
        string Email { get; }
        IEnumerable<string> Roles { get; }
        bool HasRole(params string[] roles);
    }
}
