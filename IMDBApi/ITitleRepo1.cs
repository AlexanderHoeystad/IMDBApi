
namespace IMDBApi
{
    public interface ITitleRepo1
    {
        Title AddTitle(Title title);
        Title? Delete(string tconst);
        Title? GetTitle(string tconst);
        IEnumerable<Title> GetTitleList();
        IEnumerable<Title> SearchTitle(string searchTerm);
        Title? UpdateTitle(string tconst, Title title);
    }
}