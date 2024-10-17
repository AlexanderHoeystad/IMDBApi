using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace IMDBApi
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
            // Create parameters for the stored procedure
            var nconstParam = new SqlParameter("@nconst", title.Tconst);
            var titleTypeParam = new SqlParameter("@titleType", title.TitleType);
            var primaryTitleParam = new SqlParameter("@primaryTitle", title.PrimaryTitle);
            var originalTitleParam = new SqlParameter("@originalTitle", title.OriginalTitle);
            var isAdultParam = new SqlParameter("@isAdult", title.IsAdult);
            var startYearParam = new SqlParameter("@startYear", title.StartYear);
            var endYearParam = new SqlParameter("@endYear", title.EndYear);
            var runtimeMinutesParam = new SqlParameter("@runTimeMinutes", title.RuntimeMinutes);

            // Execute the stored procedure
            _context.Database.ExecuteSqlRaw(
                "EXEC dbo.AddMovie @nconst, @titleType, @primaryTitle, @originalTitle, @isAdult, @startYear, @endYear, @runTimeMinutes",
                nconstParam,
                titleTypeParam,
                primaryTitleParam,
                originalTitleParam,
                isAdultParam,
                startYearParam,
                endYearParam,
                runtimeMinutesParam
            );

            return title; // Return the title object or any other result as needed
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

        public IEnumerable<Title> GetTitleList()
        {
            return _context.Titles.FromSqlRaw("EXEC GetTitlesSP").ToList();
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
                _context.SaveChanges();
            }
            return existingTitle;
        }

        public IEnumerable<Title> SearchTitle(string searchTerm)
        {
            return _context.Titles.FromSqlRaw("EXEC SearchTitle @searchTerm = {0}", searchTerm).ToList();
        }

    }
}
