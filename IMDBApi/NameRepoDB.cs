using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace IMDBApi
{

    public class NameRepoDB : INameRepo
    {
        private readonly IMDBDbContext _context;

        public NameRepoDB(IMDBDbContext context)
        {
            _context = context;
        }

        // Get a list of Names with optional ordering
        public IEnumerable<Name> GetNameList(string? orderby = null)
        {
            return _context.Names.FromSqlRaw("EXEC GetPersonsSP").ToList();
        }

        // Add a new Name - note that this assumes you have an appropriate stored procedure
        public Name AddName(Name name)
        {
            // Call stored procedure to add a name
            _context.Database.ExecuteSqlRaw("EXEC AddPerson @Nconst, @PrimaryName, @BirthYear, @DeathYear",
                new SqlParameter("@Nconst", name.Nconst),
                new SqlParameter("@PrimaryName", name.PrimaryName),
                new SqlParameter("@BirthYear", name.BirthYear ?? (object)DBNull.Value),
                new SqlParameter("@DeathYear", name.DeathYear ?? (object)DBNull.Value));

            return name; // Return the created name
        }

        // Get a specific Name by Nconst
        public Name? GetName(string nconst)
        {
            return _context.Names
                .FromSqlRaw("EXEC GetNameByNconst @Nconst", new SqlParameter("@Nconst", nconst))
                .AsEnumerable()
                .FirstOrDefault();
        }


        // Update an existing Name
        public Name? UpdateName(string nconst, Name name)
        {
            // Call stored procedure to update the name
            _context.Database.ExecuteSqlRaw("EXEC UpdatePerson @Nconst, @PrimaryName, @BirthYear, @DeathYear",
                new SqlParameter("@Nconst", nconst),
                new SqlParameter("@PrimaryName", name.PrimaryName),
                new SqlParameter("@BirthYear", name.BirthYear ?? (object)DBNull.Value),
                new SqlParameter("@DeathYear", name.DeathYear ?? (object)DBNull.Value));

            return name; // Return updated name
        }

        // Delete a Name by Nconst
        public Name? Delete(string nconst)
        {
            var name = GetName(nconst); // Get the existing name before deleting
            if (name != null)
            {
                // Call stored procedure to delete the name
                _context.Database.ExecuteSqlRaw("EXEC DeletePerson @Nconst", new SqlParameter("@Nconst", nconst));
            }
            return name; // Return the deleted name or null if it was not found
        }

        // Search for Names using a search term
        public IEnumerable<Name> SearchPerson(string searchTerm)
        {
            return _context.Names.FromSqlRaw("EXEC SearchPerson @searchTerm", new SqlParameter("@searchTerm", searchTerm)).ToList();
        }
    }

}
