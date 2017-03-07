using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSLS.Domain.Abstract;
using SSLS.Domain.Concrete;

namespace SSLS.Domain.Concrete
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();
        public void AddItem(Book book, int quantity)
        {
            CartLine line = lineCollection.Where(e => e.Book.Id == book.Id).FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new CartLine { Book = book });
            }
            else
            {
               
            }
        }
        public IEnumerable<CartLine> Lines { get { return lineCollection; } }
        public void RemoveLine(Book book)
        { lineCollection.RemoveAll(e => e.Book.Id == book.Id); }
        public void Clear()
        { lineCollection.Clear(); }
     
    }
}
