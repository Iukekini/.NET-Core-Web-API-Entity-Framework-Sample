using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_API_Entity_Framework_Sample.Models;

namespace Web_API_Entity_Framework_Sample.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [ProducesResponseType(401)]
    public class ToDoController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ToDoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        /// <summary>
        /// Get a list of all the To Do's
        /// </summary>
        /// <returns>The get.</returns>
        // GET: api/values
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ToDo>))]
        public IActionResult Get()
        {
            return Ok(_unitOfWork.ToDoRepository.Get());
        }


        /// <summary>
        /// Get a To Do by id
        /// </summary>
        /// <returns>The get.</returns>
        /// <param name="id">Identifier.</param>
        // GET api/values/5
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ToDo))]
        [ProducesResponseType(404)]
        public IActionResult Get(int id)
        {

            var serviceType = _unitOfWork.ToDoRepository.GetByID(id);

            //Check to see if we got a result set status code
            if (serviceType == null)
            {
                return NotFound();
            }
            return Ok(serviceType);
        }

        /// <summary>
        /// Create a To Do. 
        /// </summary>
        /// <returns>The post.</returns>
        /// <param name="todo">Service type.</param>
        // POST api/values
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ToDo))]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody]ToDo todo)
        {

            _unitOfWork.ToDoRepository.Insert(todo);
            _unitOfWork.Save();

            return CreatedAtAction(nameof(Get), new { Id = todo.ToDoId }, todo);
        }


        /// <summary>
        /// Update a To Do
        /// </summary>
        /// <returns>The put.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="todo">To Do Object.</param>
        // PUT api/values/5
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(ToDo))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Put(int id, [FromBody] ToDo todo)
        {
            if (id != todo.ToDoId)
                return BadRequest();

            _unitOfWork.ToDoRepository.Update(todo);
            _unitOfWork.Save();
            return Ok(todo);
        }

        /// <summary>
        /// Delete To Do by ID
        /// </summary>
        /// <returns>The delete.</returns>
        /// <param name="id">Identifier.</param>
        // DELETE api/values/5
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult Delete(int id)
        {

            _unitOfWork.ToDoRepository.Delete(id);
            _unitOfWork.Save();
            return Ok();
        }
    }
}
