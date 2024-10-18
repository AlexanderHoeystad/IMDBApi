
namespace IMDBApi
{
    public interface INameRepo
    {
        IEnumerable<Name> GetPersonList(string? orderby = null);
        Name AddPerson(Name name);
        Name? DeletePerson(string nconst);
        Name? GetPerson(string nconst);
        Name? UpdatePerson(string nconst, Name name);
        IEnumerable<Name> SearchPerson(string searchTerm);
    }
}