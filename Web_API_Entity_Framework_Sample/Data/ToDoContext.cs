using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Web_API_Entity_Framework_Sample.Models
{
    /// <summary>
    /// To do context.
    /// </summary>
    public class ToDoContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Web_API_Entity_Framework_Sample.Models.ToDoContext"/> class.
        /// </summary>
        /// <param name="options">Options.</param>
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options) { }

        //Paramless required for mocking
        public ToDoContext() { }

        /// <summary>
        /// Gets or sets to dos.
        /// </summary>
        /// <value>To dos.</value>
        public virtual DbSet<ToDo> ToDos { get; set; }

    }

    /// <summary>
    /// To do.
    /// </summary>
    public class ToDo
    {
        /// <summary>
        /// ToDo Identitier
        /// </summary>
        public int ToDoId { get; set; }

        /// <summary>
        /// ToDo Name
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>The notes.</value>
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:Web_API_Entity_Framework_Sample.Models.ToDo"/> is completed.
        /// </summary>
        /// <value><c>true</c> if completed; otherwise, <c>false</c>.</value>
        public bool Completed { get; set; }

        /// <summary>
        /// Gets or sets the created on.
        /// </summary>
        /// <value>The created on.</value>
        public DateTime? CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>The created by.</value>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the completed on.
        /// </summary>
        /// <value>The completed on.</value>
        public DateTime? CompletedOn { get; set; }

        /// <summary>
        /// Gets or sets the completed by.
        /// </summary>
        /// <value>The completed by.</value>
        public string CompletedBy { get; set; }


    }

}
