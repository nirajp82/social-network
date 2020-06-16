using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialNetwork.DTO;
using SocialNetwork.Nucleus;

namespace SocialNetwork.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        #region Members
        private ILogger<ValuesController> _logger { get; }
        private IValueEngine _valueEngine { get; }
        #endregion


        #region Constructor
        public ValuesController(ILogger<ValuesController> logger, IValueEngine valueEngine)
        {
            _logger = logger;
            _valueEngine = valueEngine;
        }
        #endregion


        #region Action Methods
        /// <summary>
        /// Fetch list of all Values
        /// </summary>
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<ValueDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            var values = await _valueEngine.FindAllAsync();
            if (values?.Any() == true)
                return Ok(values);
            else
                return NoContent();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ValueDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            ValueDTO entity = await _valueEngine.FindFirstAsync(id);
            if (entity != null)
                return Ok(entity);
            else
                return NotFound();
        }

        // POST api/values
        [HttpPost]
        [ProducesResponseType(typeof(ValueDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] string value)
        {
            ValueDTO entity = await _valueEngine.AddAsync(new ValueDTO { Name = value });
            return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromBody] string value)
        {
            await _valueEngine.UpdateAsync(new ValueDTO { Name = value, Id = id });
            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            await _valueEngine.DeleteAsync(id);
            return NoContent();
        }
        #endregion
    }
}
