
namespace IMDBApi
{
    public interface INameRepo
    {
        IEnumerable<Name> GetNameList(string? orderby = null);
        Name AddName(Name name);
        Name? Delete(string nconst);
        Name? GetName(string nconst);
        Name? UpdateName(string nconst, Name name);
        IEnumerable<Name> SearchPerson(string searchTerm);
    }
}