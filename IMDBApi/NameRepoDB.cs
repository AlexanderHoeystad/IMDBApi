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

        // Get list of names with optional ordering
        public IEnumerable<Name> GetNameList(string? orderby = null)
        {
            IQueryable<Name> query = _context.Names;

            // Apply ordering if specified
            if (!string.IsNullOrEmpty(orderby))
            {
                switch (orderby.ToLower())
                {
                    case "primaryname":
                        query = query.OrderBy(n => n.PrimaryName);
                        break;
                    case "birthyear":
                        query = query.OrderBy(n => n.BirthYear);
                        break;
                    case "deathyear":
                        query = query.OrderBy(n => n.DeathYear);
                        break;
                    default:
                        break;
                }
            }

            return query.ToList(); // Return the list of names
        }

        // Add a new name
        public Name AddName(Name name)
        {
            _context.Names.Add(name);
            _context.SaveChanges(); // Save changes to the database
            return name; // Return the added name
        }

        // Delete a name by Nconst
        public Name? Delete(string nconst)
        {
            var name = _context.Names.Find(nconst);
            if (name == null)
            {
                return null; // Name not found
            }

            _context.Names.Remove(name);
            _context.SaveChanges(); // Save changes to the database
            return name; // Return the deleted name
        }

        // Get a name by Nconst
        public Name? GetName(string nconst)
        {
            return _context.Names.Find(nconst); // Fetch the name by Nconst
        }

        // Update an existing name
        public Name? UpdateName(string nconst, Name name)
        {
            if (nconst != name.Nconst)
            {
                return null; // Nconst mismatch
            }

            _context.Names.Update(name);
            _context.SaveChanges(); // Save changes to the database
            return name; // Return the updated name
        }

        public IEnumerable<Name> SearchPerson(string searchTerm)
        {
            return _context.Names.FromSqlRaw("EXEC SearchPerson @searchTerm = {0}", searchTerm).ToList();
        }
    }
}
