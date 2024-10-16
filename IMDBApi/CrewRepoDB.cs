
namespace IMDBApi
{
    public class CrewRepoDB : ICrewRepo
    {
        private readonly IMDBDbContext _context;

        public CrewRepoDB(IMDBDbContext context)
        {
            _context = context;
        }



        public Crew AddCrew(Crew crew)
        {
            _context.Crews.Add(crew);
            _context.SaveChanges();
            return crew;
        }

        public Crew? Delete(string tconst)
        {
            Crew? crewToDelete = GetCrew(tconst);
            if (crewToDelete != null)
            {
                _context.Crews.Remove(crewToDelete);
                _context.SaveChanges();
                return crewToDelete;
            }
            return null;
        }

        public Crew? GetCrew(string tconst)
        {
            return _context.Crews.FirstOrDefault(c => c.Tconst == tconst);
        }

        public IEnumerable<Crew> GetCrewList(string? orderby = null)
        {
            return _context.Crews.ToList();
        }

        public Crew? UpdateCrew(string tconst, Crew crew)
        {
            Crew? crewToUpdate = GetCrew(tconst);
            if (crewToUpdate == null)
            {
                return null;
            }
            crewToUpdate.Directors = crew.Directors;
            crewToUpdate.Writers = crew.Writers;
            _context.SaveChanges();
            return crewToUpdate;
        }
    }
}
