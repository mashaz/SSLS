using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSLS.Domain.Concrete;


namespace SSLS.Domain.Abstract
{
    public interface IBooksReopository
    {
        IQueryable<Book> Books { get; }
        IQueryable<Category> Categories { get; }
        IQueryable<Reader> Readers { get; }
        IQueryable<Borrow> Borrows { get; }
        //IQueryable<Admin> Admins { get; }
        void saveBook(Book book);
        Book DeleteBook(int id);
    }
}
