using System;
namespace Web_API_Entity_Framework_Sample.Models
{
    public interface IUnitOfWork
    {
        IGenericRepository<ToDo> ToDoRepository { get; }

        void Dispose();
        void Save();
    }
}
