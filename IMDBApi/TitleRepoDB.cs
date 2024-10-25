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
            var endYearParam = new SqlParameter("@endYear", (object)title.EndYear ?? DBNull.Value); // handle null
            var runtimeMinutesParam = new SqlParameter("@runTimeMinutes", (object)title.RuntimeMinutes ?? DBNull.Value); // handle null

            // Assume title.Genres is a comma-separated string, like "Action, Drama"
            var genresParam = new SqlParameter("@genres", title.Genres ?? string.Empty); // Pass empty string if null

            // Execute the stored procedure with genres
            _context.Database.ExecuteSqlRaw(
                "EXEC dbo.AddMovie @nconst, @titleType, @primaryTitle, @originalTitle, @isAdult, @startYear, @endYear, @runTimeMinutes, @genres",
                nconstParam,
                titleTypeParam,
                primaryTitleParam,
                originalTitleParam,
                isAdultParam,
                startYearParam,
                endYearParam,
                runtimeMinutesParam,
                genresParam // Add the genres parameter
            );

            return title; // Return the title object or any other result as needed
        }



        public Title? Delete(string tconst)
        {
            var title = GetTitle(tconst); // Retrieve the title before deleting
            if (title != null)
            {
                // Execute stored procedure to delete the title
                _context.Database.ExecuteSqlRaw("EXEC DeleteTitle @Tconst", new SqlParameter("@Tconst", tconst));
            }
            return title; // Return the deleted title or null if not found
        }

        public Title? GetTitle(string tconst)
        {
            return _context.Titles
                .FromSqlRaw("EXEC GetTitleByTconst @Tconst", new SqlParameter("@Tconst", tconst))
                .AsEnumerable()
                .FirstOrDefault();
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
