﻿using IMDBApi;

namespace IMDBApi
{
    public class TitleRepo : ITitleRepo
    {
        private int _nextTconst = 6;
        private List<Title> _titles = new List<Title>
        {
              new Title { Tconst = "tt0000001", TitleType = "movie", PrimaryTitle = "The Kid", OriginalTitle = "The Kid", IsAdult = false, StartYear = 1921, EndYear = null, RuntimeMinutes = 68},
              new Title { Tconst = "tt0000002", TitleType = "movie", PrimaryTitle = "The Godfather", OriginalTitle = "The Godfather", IsAdult = false, StartYear = 1972, EndYear = null, RuntimeMinutes = 175},
              new Title {Tconst = "tt0000003", TitleType = "movie", PrimaryTitle = "The Dark Knight", OriginalTitle = "The Dark Knight", IsAdult = false, StartYear = 2008, EndYear = null, RuntimeMinutes = 152},
              new Title {Tconst = "tt0000004", TitleType = "movie", PrimaryTitle = "Forrest Gump", OriginalTitle = "Forrest Gump", IsAdult = false, StartYear = 1994, EndYear = null, RuntimeMinutes = 142},
              new Title {Tconst = "tt0000005", TitleType = "movie", PrimaryTitle = "Titanic", OriginalTitle = "Titanic", IsAdult = false, StartYear = 1997, EndYear = null, RuntimeMinutes = 195}
        };

        public IEnumerable<Title> GetTitleList()
        {
            IEnumerable<Title> result = new List<Title>(_titles);
            //if (orderby != null)
            //{
            //    switch (orderby)
            //    {
            //        case "TitleType":
            //            result = result.OrderBy(t => t.TitleType);
            //            break;
            //        case "PrimaryTitle":
            //            result = result.OrderBy(t => t.PrimaryTitle);
            //            break;
            //        case "OriginalTitle":
            //            result = result.OrderBy(t => t.OriginalTitle);
            //            break;
            //        case "StartYear":
            //            result = result.OrderBy(t => t.StartYear);
            //            break;
            //        case "EndYear":
            //            result = result.OrderBy(t => t.EndYear);
            //            break;
            //        case "RuntimeMinutes":
            //            result = result.OrderBy(t => t.RuntimeMinutes);
            //            break;
            //        default:
            //            break;
            //    }

            //}
            return result;
        }

        public Title? GetTitle(string tconst)
        {
            return _titles.FirstOrDefault(t => t.Tconst == tconst);
        }

        public Title AddTitle(Title title)
        {
            title.Tconst = $"tt{_nextTconst++}";
            _titles.Add(title);
            return title;
        }

        public Title? Delete(string tconst)
        {
            Title title = _titles.FirstOrDefault(t => t.Tconst == tconst);
            if (title != null)
            {
                _titles.Remove(title);
            }
            return title;
        }

        public Title? UpdateTitle(string tconst, Title title)
        {
            Title? existingTitle = _titles.FirstOrDefault(t => t.Tconst == tconst);
            if (existingTitle != null)
            {
                existingTitle.TitleType = title.TitleType;
                existingTitle.PrimaryTitle = title.PrimaryTitle;
                existingTitle.OriginalTitle = title.OriginalTitle;
                existingTitle.IsAdult = title.IsAdult;
                existingTitle.StartYear = title.StartYear;
                existingTitle.EndYear = title.EndYear;
                existingTitle.RuntimeMinutes = title.RuntimeMinutes;
                
            }
            return existingTitle;
        }

        public IEnumerable<Title> SearchTitle(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
