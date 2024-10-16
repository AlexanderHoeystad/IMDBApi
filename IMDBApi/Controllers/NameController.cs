using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMDBApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NameController : Controller
    {
        private readonly NameRepoDB _nameRepo;

        public NameController(NameRepoDB nameRepo)
        {
            _nameRepo = nameRepo;
        }

        // GET: api/names
        [HttpGet]
        public ActionResult<IEnumerable<Name>> GetNames(string? orderby = null)
        {
            var names = _nameRepo.GetNameList(orderby);
            return Ok(names);
        }

        // GET: api/names/{nconst}
        [HttpGet("{nconst}")]
        public ActionResult<Name> GetName(string nconst)
        {
            var name = _nameRepo.GetName(nconst);
            if (name == null)
            {
                return NotFound(); // Return 404 if not found
            }
            return Ok(name);
        }

        // POST: api/names
        [HttpPost]
        public ActionResult<Name> CreateName(Name name)
        {
            var createdName = _nameRepo.AddName(name);
            return CreatedAtAction(nameof(GetName), new { nconst = createdName.Nconst }, createdName);
        }

        // PUT: api/names/{nconst}
        [HttpPut("{nconst}")]
        public ActionResult<Name> UpdateName(string nconst, Name name)
        {
            var updatedName = _nameRepo.UpdateName(nconst, name);
            if (updatedName == null)
            {
                return BadRequest(); // Return 400 if Nconst doesn't match
            }
            return NoContent(); // Return 204 No Content for a successful update
        }

        // DELETE: api/names/{nconst}
        [HttpDelete("{nconst}")]
        public ActionResult<Name> DeleteName(string nconst)
        {
            var deletedName = _nameRepo.Delete(nconst);
            if (deletedName == null)
            {
                return NotFound(); // Return 404 if not found
            }
            return Ok(deletedName); // Return the deleted name
        }

        [HttpGet("search")]
        public ActionResult<IEnumerable<Name>> SearchPersons(string searchTerm)
        {
            var results = _nameRepo.SearchPersons(searchTerm);
            return Ok(results);
        }

    }
}
