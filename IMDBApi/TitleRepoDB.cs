﻿namespace IMDBApi
{
    public class TitleRepoDB : ITitleRepo
    {
        private readonly IMDBDbContext _context;

        public TitleRepoDB(IMDBDbContext context)
        {
            _context = context;
        }

        public Title AddTitle(Title title)
        {
            _context.Titles.Add(title);
            _context.SaveChanges();
            return title;
        }

        public Title? Delete(string tconst)
        {
            var title = GetTitle(tconst);
            if (title != null)
            {
                _context.Titles.Remove(title);
                _context.SaveChanges();
            }
            return title;
        }

        public Title? GetTitle(string tconst)
        {
            return _context.Titles.Find(tconst);
        }

        public IEnumerable<Title> GetTitleList(string? orderby = null)
        {
            if (orderby == null)
            {
                return _context.Titles;
            }
            else
            {
                return orderby switch
                {
                    "TitleType" => _context.Titles.OrderBy(t => t.TitleType),
                    "PrimaryTitle" => _context.Titles.OrderBy(t => t.PrimaryTitle),
                    "OriginalTitle" => _context.Titles.OrderBy(t => t.OriginalTitle),
                    "IsAdult" => _context.Titles.OrderBy(t => t.IsAdult),
                    "StartYear" => _context.Titles.OrderBy(t => t.StartYear),
                    "EndYear" => _context.Titles.OrderBy(t => t.EndYear),
                    "RuntimeMinutes" => _context.Titles.OrderBy(t => t.RuntimeMinutes),
                    "GenreID" => _context.Titles.OrderBy(t => t.GenreID),
                    "Genre" => _context.Titles.OrderBy(t => t.Genre),
                    _ => _context.Titles
                };
            }
        }

        public Title? UpdateTitle(string tconst, Title title)
        {
            var existingTitle = GetTitle(tconst);
            if (existingTitle != null)
            {
                existingTitle.TitleType = title.TitleType;
                existingTitle.PrimaryTitle = title.PrimaryTitle;
                existingTitle.OriginalTitle = title.OriginalTitle;
                existingTitle.IsAdult = title.IsAdult;
                existingTitle.StartYear = title.StartYear;
                existingTitle.EndYear = title.EndYear;
                existingTitle.RuntimeMinutes = title.RuntimeMinutes;
                existingTitle.GenreID = title.GenreID;
                existingTitle.Genre = title.Genre;
                _context.SaveChanges();
            }
            return existingTitle;
        }   

    }
}
