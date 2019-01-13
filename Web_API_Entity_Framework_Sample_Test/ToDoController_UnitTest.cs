using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Web_API_Entity_Framework_Sample.Controllers;
using Web_API_Entity_Framework_Sample.Models;

namespace Web_API_Entity_Framework_Sample_Test
{
    /// <summary>
    /// To do controller unit test.
    /// </summary>
    [TestClass]
    public class ToDoController_UnitTest
    {

        IQueryable<ToDo> _todos;
        Mock<IUnitOfWork> _mockContext;
        Mock<IGenericRepository<ToDo>> _toDoRepository;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Web_API_Entity_Framework_Sample_Test.ToDoController_UnitTest"/> class.
        /// </summary>
        public ToDoController_UnitTest()
        {
            _todos = new List<ToDo>()
            {
                new ToDo() { ToDoId = 1, Completed = false, CompletedBy = "Goe", CompletedOn = DateTime.Parse("1/1/2019"), CreatedBy = "Joe " , CreatedOn = DateTime.Parse("1/1/2019"), Name = "Sample Task", Notes = "Notes"},
                new ToDo() { ToDoId = 2, Completed = false, CompletedBy = "Goe", CompletedOn = DateTime.Parse("1/1/2019"), CreatedBy = "Joe " , CreatedOn = DateTime.Parse("1/1/2019"), Name = "Sample Task", Notes = "Notes"},
                new ToDo() { ToDoId = 3, Completed = false, CompletedBy = "Goe", CompletedOn = DateTime.Parse("1/1/2019"), CreatedBy = "Joe " , CreatedOn = DateTime.Parse("1/1/2019"), Name = "Sample Task", Notes = "Notes"},
                new ToDo() { ToDoId = 4, Completed = false, CompletedBy = "Goe", CompletedOn = DateTime.Parse("1/1/2019"), CreatedBy = "Joe " , CreatedOn = DateTime.Parse("1/1/2019"), Name = "Sample Task", Notes = "Notes"},
            }.AsQueryable();

            _toDoRepository = new Mock<IGenericRepository<ToDo>>();
            _mockContext = new Mock<IUnitOfWork>();
            _mockContext.Setup(d => d.ToDoRepository).Returns(_toDoRepository.Object);

        }

        /// <summary>
        /// Retreives to dos unit test.
        /// </summary>
        [TestMethod]
        public void RetreiveToDos_UnitTest()
        {

            _toDoRepository.Setup(d => d.Get(null, null, "")).Returns(_todos);


            var toDoController = new ToDoController(_mockContext.Object);
            var actionResult = toDoController.Get();

            var okResult = actionResult as OkObjectResult;


            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var content = okResult.Value as IEnumerable<ToDo>;

            Assert.AreEqual(4, content.Count());
            Assert.AreEqual("Sample Task", content.First().Name);

        }


        /// <summary>
        /// Retrieves to do unit test invalid identifier.
        /// </summary>
        [TestMethod]
        public void RetrieveToDo_UnitTest_InvalidID()
        {


            _toDoRepository.Setup(d => d.GetByID(It.IsAny<int>())).Returns(() => null);


            var toDoController = new ToDoController(_mockContext.Object);
            var results = toDoController.Get(5) as NotFoundResult;


            Assert.IsNotNull(results);
            Assert.AreEqual(404, results.StatusCode);

        }

        /// <summary>
        /// Retrieves to do unit test.
        /// </summary>
        [TestMethod]
        public void RetrieveToDo_UnitTest()
        {

            _toDoRepository.Setup(d => d.GetByID(It.IsAny<int>())).Returns(_todos.First());


            //Run Get Method
            var toDoController = new ToDoController(_mockContext.Object);
            var results = toDoController.Get(1);
            var okResult = results as OkObjectResult;

            //Assert we got back a good result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            //Check the data return to make sure we got the correct item. 
            var content = okResult.Value as ToDo;
            //////assert
            Assert.AreEqual(1, content.ToDoId);
            Assert.AreSame("Sample Task", content.Name);
        }


        /// <summary>
        /// Adds to do unit test.
        /// </summary>
        [TestMethod]
        public void AddToDo_UnitTest()
        {
            var todoToAdd = new ToDo()
            {
                Name = "Sample Task"
            };

            var toDoController = new ToDoController(_mockContext.Object);
            var result = toDoController.Post(todoToAdd);
            var createdResult = result as CreatedAtActionResult;

            Assert.IsNotNull(createdResult);
            Assert.AreEqual(201, createdResult.StatusCode);
            _toDoRepository.Verify(m => m.Insert(It.IsAny<ToDo>()), Times.Once);
            _mockContext.Verify(m => m.Save(), Times.Once);

            //Check the data return to make sure we got the correct item. 
            var content = createdResult.Value as ToDo;
            //////assert
            Assert.AreSame("Sample Task", content.Name);
        }

        /// <summary>
        /// Updates to do unit test.
        /// </summary>
        [TestMethod]
        public void UpdateToDo_UnitTest()
        {
            var todoToAdd = new ToDo()
            {
                ToDoId = 1,
                Name = "Sample Task 2"
            };


            var toDoController = new ToDoController(_mockContext.Object);
            var result = toDoController.Put(1, todoToAdd);
            var createdResult = result as OkObjectResult;


            Assert.IsNotNull(createdResult);
            Assert.AreEqual(200, createdResult.StatusCode);


            _toDoRepository.Verify(m => m.Update(It.IsAny<ToDo>()), Times.Once);
            _mockContext.Verify(m => m.Save(), Times.Once);

            //Check the data return to make sure we got the correct item. 
            var content = createdResult.Value as ToDo;

            Assert.AreEqual(1, content.ToDoId);
            Assert.AreSame("Sample Task 2", content.Name);
        }


        /// <summary>
        /// Updates to do mismatched identifier unit test.
        /// </summary>
        [TestMethod]
        public void UpdateToDo_MismatchedID_UnitTest()
        {
            var todoUpdate = new ToDo()
            {
                ToDoId = 1,
                Name = "Sample Task Updated Name"
            };


            var toDoController = new ToDoController(_mockContext.Object);
            var result = toDoController.Put(3, todoUpdate);
            var createdResult = result as BadRequestResult;


            Assert.IsNotNull(createdResult);
            Assert.AreEqual(400, createdResult.StatusCode);


            _toDoRepository.Verify(m => m.Update(It.IsAny<ToDo>()), Times.Never);
            _mockContext.Verify(m => m.Save(), Times.Never);

        }


        /// <summary>
        /// Deletes to do unit test.
        /// </summary>
        [TestMethod]
        public void DeleteToDo_UnitTest()
        {

            var toDoController = new ToDoController(_mockContext.Object);
            var result = toDoController.Delete(1);
            var deleteResult = result as OkResult;

            Assert.IsNotNull(deleteResult);
            Assert.AreEqual(200, deleteResult.StatusCode);


            _toDoRepository.Verify(m => m.Delete(It.IsAny<int>()), Times.Once);
            _mockContext.Verify(m => m.Save(), Times.Once);
        }

    }
}
