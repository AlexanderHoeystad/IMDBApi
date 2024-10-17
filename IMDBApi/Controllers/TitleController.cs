using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMDBApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TitleController : Controller
    {
        private TitleRepoDB _titleRepo;

        public TitleController(TitleRepoDB titleRepo)
        {
            _titleRepo = titleRepo;
        }

        // GET: api/<TempController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<Title>> GetAll()
        {
            var titles = _titleRepo.GetTitleList();
            if (titles == null)
            {
                return NoContent();
            }
            return Ok(titles);


        }

        // GET api/<TempController>/5
        [HttpGet("{tconst}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Title> Get(string tconst)
        {
            var title = _titleRepo.GetTitle(tconst);
            if (title == null)
            {
                return NotFound();
            }
            return Ok(title);

        }

        // POST api/<TempController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Title> Post([FromBody] Title title)
        {
            var newTitle = _titleRepo.AddTitle(title);
            return CreatedAtAction(nameof(Get), new { tconst = newTitle.Tconst }, newTitle);


        }

        // PUT api/<TempController>/5
        [HttpPut("{tconst}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<Title> Put(string tconst, [FromBody] Title title)
        {
            var existingTitle = _titleRepo.GetTitle(tconst);
            if (existingTitle == null)
            {
                return NotFound();
            }
            var updatedTitle = _titleRepo.UpdateTitle(tconst, title);
            return Ok(updatedTitle);


        }

        // DELETE api/<TempController>/5
        [HttpDelete("{tconst}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<Title?> Delete(string tconst)
        {
            var title = _titleRepo.Delete(tconst);
            if (title == null)
            {
                return NoContent();
            }
            return Ok(title);


        }

        [HttpGet("search")]
        public ActionResult<IEnumerable<Title>> SearchTitle(string searchTerm)
        {
            var results = _titleRepo.SearchTitle(searchTerm);
            return Ok(results);
        }


    }
}
