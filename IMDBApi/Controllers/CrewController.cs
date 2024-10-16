using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMDBApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrewController : Controller
    {
        //private CrewRepo _crewRepo;
        private CrewRepoDB _crewRepo;

        public CrewController(CrewRepoDB crewRepo)
        {
            _crewRepo = crewRepo;
        }


        // GET: api/<TempController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<Crew>> GetAll([FromQuery] string? orderBy = null)
        {
            var crews = _crewRepo.GetCrewList(orderBy);
            if (crews == null)
            {
                return NoContent();
            }
            return Ok(crews);


        }

        // GET api/<TempController>/5
        [HttpGet("{tconst}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Crew> Get(string tconst)
        {
            var crew = _crewRepo.GetCrew(tconst);
            if (crew == null)
            {
                return NotFound();
            }
            return Ok(crew);

        }

        // POST api/<NameController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Crew> Post([FromBody] Crew crew)
        {
            var newCrew = _crewRepo.AddCrew(crew);
            return CreatedAtAction(nameof(Get), new { tconst = newCrew.Tconst }, newCrew);


        }

        // PUT api/<TempController>/5
        [HttpPut("{tconst}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<Crew> Put(string tconst, [FromBody] Crew crew)
        {
            var existingCrew = _crewRepo.GetCrew(tconst);
            if (existingCrew == null)
            {
                return NotFound();
            }
            var updatedCrew = _crewRepo.UpdateCrew(tconst, crew);
            return Ok(updatedCrew);


        }

        // DELETE api/<TempController>/5
        [HttpDelete("{tconst}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<Crew?> Delete(string tconst)
        {
            var crew = _crewRepo.Delete(tconst);
            if (crew == null)
            {
                return NoContent();
            }
            return Ok(crew);


        }




    }
}
