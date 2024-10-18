
namespace IMDBApi
{
    public interface INameRepo
    {
        IEnumerable<Name> GetPersonsList(string? orderby = null);
        Name AddPerson(Name name);
        Name? DeletePerson(string nconst);
        Name? GetPerson(string nconst);
        Name? UpdatePerson(string nconst, Name name);
        IEnumerable<Name> SearchPerson(string searchTerm);
    }
}