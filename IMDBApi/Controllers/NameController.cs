using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMDBApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NameController : Controller
    {
        private NameRepo _nameRepo;

        public NameController(NameRepo nameRepo)
        {
            _nameRepo = nameRepo;
        }

        // GET: api/<TempController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<Name>> GetAll([FromQuery] string? orderBy = null)
        {
            var names = _nameRepo.GetNameList(orderBy);
            if (names == null)
            {
                return NoContent();
            }
            return Ok(names);

        }

        // GET api/<TempController>/5
        [HttpGet("{nconst}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Name> Get(string nconst)
        {
            var name = _nameRepo.GetName(nconst);
            if (name == null)
            {
                return NotFound();
            }
            return Ok(name);
        }

        // POST api/<NameController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Name> Post([FromBody] Name name)
        {
            if (name == null)
            {
                return BadRequest();
            }

            var newName = _nameRepo.AddName(name);
            if (newName == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating the new name.");
            }

            return CreatedAtAction(nameof(Get), new { id = newName.Nconst }, newName);
        }

        // PUT api/<TempController>/5
        [HttpPut("{nconst}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<Name> Put(string nconst, [FromBody] Name name)
        {
            var existingName = _nameRepo.GetName(nconst);
            if (existingName == null)
            {
                return NotFound();
            }
            var updatedName = _nameRepo.UpdateName(nconst, name);
            return Ok(updatedName);

        }

        // DELETE api/<TempController>/5
        [HttpDelete("{nconst}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<Name?> Delete(string nconst)
        {
            var name = _nameRepo.Delete(nconst);
            if (name == null)
            {
                return NoContent();
            }
            return Ok(name);

        }
    }
}
