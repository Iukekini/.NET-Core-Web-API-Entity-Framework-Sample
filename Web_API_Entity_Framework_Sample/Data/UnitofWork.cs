using System;
namespace Web_API_Entity_Framework_Sample.Models
{

    public class UnitOfWork : IDisposable, IUnitOfWork
    {

        private ToDoContext context;
        private IGenericRepository<ToDo> _toDoRepository;

        public UnitOfWork(ToDoContext _context)
        {
            context = _context;
        }

        public IGenericRepository<ToDo> ToDoRepository
        {
            get
            {

                if (this.ToDoRepository == null)
                {
                    this._toDoRepository = new GenericRepository<ToDo>(context);
                }
                return _toDoRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
