namespace IMDBApi
{
    public class CrewRepo
    {
        private int _nextTconst = 6;
        private List<Crew> _crews = new List<Crew>
        {
              new Crew { Tconst = "tt0000001", Directors = "nm0000001", Writers = "nm0000002" },
              new Crew { Tconst = "tt0000002", Directors = "nm0000003", Writers = "nm0000004" },
              new Crew { Tconst = "tt0000003", Directors = "nm0000005", Writers = "nm0000006" },
              new Crew { Tconst = "tt0000004", Directors = "nm0000007", Writers = "nm0000008" },
              new Crew { Tconst = "tt0000005", Directors = "nm0000009", Writers = "nm0000010" }
        };

        public IEnumerable<Crew> GetCrewList(string? orderby = null)
        {
            IEnumerable<Crew> result = new List<Crew>(_crews);
            if (orderby != null)
            {
                switch (orderby)
                {
                    case "Directors":
                        result = result.OrderBy(c => c.Directors);
                        break;
                    case "Writers":
                        result = result.OrderBy(c => c.Writers);
                        break;
                    default:
                        break;
                }
            }
            return result;
        }

        public Crew? GetCrew(string tconst)
        {
            return _crews.FirstOrDefault(c => c.Tconst == tconst);
        }

        public Crew AddCrew(Crew crew)
        {
            crew.Tconst = $"tt{_nextTconst++}";
            _crews.Add(crew);
            return crew;
        }

        public Crew? Delete(string tconst)
        {
            Crew crew = _crews.FirstOrDefault(c => c.Tconst == tconst);
            if (crew != null)
            {
                _crews.Remove(crew);
            }
            return crew;
        }

        public Crew? UpdateCrew(string tconst, Crew crew)
        {
            Crew existingCrew = _crews.FirstOrDefault(c => c.Tconst == tconst);
            if (existingCrew != null)
            {
                existingCrew.Directors = crew.Directors;
                existingCrew.Writers = crew.Writers;
            }
            return existingCrew;
        }
    }
}
