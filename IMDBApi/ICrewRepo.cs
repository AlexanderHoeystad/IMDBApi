namespace IMDBApi
{
    public interface ICrewRepo
    {
        Crew AddCrew(Crew crew);
        Crew? Delete(string tconst);
        Crew? GetCrew(string tconst);
        IEnumerable<Crew> GetCrewList(string? orderby = null);
        Crew? UpdateCrew(string tconst, Crew crew);
    }
}
