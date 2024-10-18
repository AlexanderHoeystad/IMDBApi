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
        public ActionResult<Name> GetPerson(string nconst)
        {
            var name = _nameRepo.GetPerson(nconst);
            if (name == null)
            {
                return NotFound(); // Return 404 if not found
            }
            return Ok(name);
        }

        // POST: api/names
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Name> AddPerson(Name name)
        {
            // Validate the model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return 400 Bad Request if the model is invalid
            }

            try
            {
                var createdName = _nameRepo.AddPerson(name); // Call to repository to add the name
                return CreatedAtAction(nameof(GetPerson), new { nconst = createdName.Nconst }, createdName); // Return 201 Created
            }
            catch (Exception ex)
            {
                // Log the exception (optional, depending on your logging setup)
                // _logger.LogError(ex, "An error occurred while creating a name.");

                return StatusCode(500, "Internal server error"); // Return 500 Internal Server Error if something goes wrong
            }
        }


        // PUT: api/names/{nconst}
        [HttpPut("{nconst}")]
        public ActionResult<Name> UpdateName(string nconst, Name name)
        {
            var updatedName = _nameRepo.UpdatePerson(nconst, name);
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
            var deletedName = _nameRepo.DeletePerson(nconst);
            if (deletedName == null)
            {
                return NotFound(); // Return 404 if not found
            }
            return Ok(deletedName); // Return the deleted name
        }

        [HttpGet("search")]
        public ActionResult<IEnumerable<Name>> SearchPerson(string searchTerm)
        {
            var results = _nameRepo.SearchPerson(searchTerm);
            return Ok(results);
        }

    }
}
