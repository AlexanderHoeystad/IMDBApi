namespace IMDBApi
{
    public class NameRepo
    {
        private int _NconstId = 6;
        private List<Name> _names = new List<Name>
        {
            new Name { Nconst = "nm0000001", PrimaryName = "Fred Astaire", BirthYear = 1899, DeathYear = 1987, PrimaryProfession = "soundtrack,actor,miscellaneous", KnownForTitles = "tt0050419,tt0053137,tt0072308,tt0043044" },
            new Name { Nconst = "nm0000002", PrimaryName = "Lauren Bacall", BirthYear = 1924, DeathYear = 2014, PrimaryProfession = "actress,soundtrack", KnownForTitles = "tt0038355,tt0037382,tt0117057,tt0071877" },
            new Name { Nconst = "nm0000003", PrimaryName = "Brigitte Bardot", BirthYear = 1934, DeathYear = null, PrimaryProfession = "actress,soundtrack,music_department", KnownForTitles = "tt0059956,tt0057345,tt0054452,tt0049189" },
            new Name { Nconst = "nm0000004", PrimaryName = "John Belushi", BirthYear = 1949, DeathYear = 1982, PrimaryProfession = "actor,writer,soundtrack", KnownForTitles = "tt0077975,tt0078723,tt0080455,tt0072562" },
            new Name { Nconst = "nm0000005", PrimaryName = "Ingmar Bergman", BirthYear = 1918, DeathYear = 2007, PrimaryProfession = "writer,director,actor", KnownForTitles = "tt0050976,tt0083922,tt0060827,tt0050986" }
        };

        public IEnumerable<Name> GetNameList(string? orderby = null)
        {
            IEnumerable<Name> result = new List<Name>(_names);
            if (orderby != null)
            {
                switch (orderby)
                {
                    case "PrimaryName":
                        result = result.OrderBy(n => n.PrimaryName);
                        break;
                    case "BirthYear":
                        result = result.OrderBy(n => n.BirthYear);
                        break;
                    case "DeathYear":
                        result = result.OrderBy(n => n.DeathYear);
                        break;
                    case "PrimaryProfession":
                        result = result.OrderBy(n => n.PrimaryProfession);
                        break;
                    case "KnownForTitles":
                        result = result.OrderBy(n => n.KnownForTitles);
                        break;
                    default:
                        break;
                }
            }
            return result;
        }

        public Name? GetName(string nconst)
        {
            return _names.FirstOrDefault(n => n.Nconst == nconst);
        }

        public Name AddName(Name name)
        {
            name.Nconst = $"nm{_NconstId++}";
            _names.Add(name);
            return name;
        }

        public Name? Delete(string nconst)
        {
            Name name = _names.FirstOrDefault(n => n.Nconst == nconst);
            if (name != null)
            {
                _names.Remove(name);
            }
            return name;
        }

        public Name? UpdateName(string nconst, Name name)
        {
            Name existingName = _names.FirstOrDefault(n => n.Nconst == nconst);
            if (existingName != null)
            {
                existingName.PrimaryName = name.PrimaryName;
                existingName.BirthYear = name.BirthYear;
                existingName.DeathYear = name.DeathYear;
                existingName.PrimaryProfession = name.PrimaryProfession;
                existingName.KnownForTitles = name.KnownForTitles;
            }
            return existingName;
        }



    }
}
