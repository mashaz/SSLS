using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSLS.Domain.Abstract;


namespace SSLS.Domain.Concrete
{
    public class EFBookRepository : IBooksReopository
    {
        private SSLSEntities db = new SSLSEntities();
        public IQueryable<Book> Books
        { 
            get{return db.Book;}
        }
   
        public IQueryable<Category> Categories
        {
            get { return db.Category; }
        }
        public IQueryable<Reader> Readers
        {
            get { return db.Reader; }

        }
        public IQueryable<Borrow> Borrows
        {
            get { return db.Borrow; }
        }
        public void saveBook(Book book)
        {
            if (book.Id == 0)
            {
                db.Book.Add(book);
            }
            else
            {
                Book dbEntity = db.Book.Find(book.Id);
                if (dbEntity != null)
                {
                    dbEntity.Name = book.Name;
                    dbEntity.CategoryId = book.CategoryId;
                    dbEntity.Price = book.Price;
                 
                }
            }
            db.SaveChanges();
        }
        public Book DeleteBook(int id)
        {
            Book dbEntity = db.Book.Find(id);
            if (dbEntity != null)
            {
                db.Book.Remove(dbEntity);
                db.SaveChanges();
            }
            return dbEntity;
        }
    }
}
