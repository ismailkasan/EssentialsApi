namespace Essentials.Domain
{
    public record UserOutputModel(string UserName, string Name, string Surname, string Password)
    { }

    public record ProductOutputModel(long Id, string Name, bool IsDeleted)
    { }
}
