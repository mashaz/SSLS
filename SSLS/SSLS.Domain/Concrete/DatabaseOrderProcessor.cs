using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSLS.Domain.Abstract;
using SSLS.Domain.Concrete;


namespace SSLS.Domain.Concrete
{
    
    public class DatabaseOrderProcessor :IOrderProcessor
    {
        public void ProcessOrder(Cart cart, Reader reader)
        {
            using (var db = new SSLSEntities())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var cartLine in cart.Lines)
                        {
                            
                            
                            Borrow borrow = new Borrow();
                            borrow.ReaderId = reader.Id;
                            borrow.BookId = cartLine.Book.Id;
                            borrow.BorrowDate = DateTime.Now;
                            borrow.ShouldReturnDate = DateTime.Now.AddDays(30);
                            borrow.Renew = false;
                            Book book = db.Book.FirstOrDefault(c => c.Id == cartLine.Book.Id);
                            book.Status = "0";
                            db.Borrow.Add(borrow);
                            db.SaveChanges();
                            dbContextTransaction.Commit();
                        }
                     
                    }
                    catch (Exception) { dbContextTransaction.Rollback(); }
                }
            }
          
        }
        public void ProcessReturn(int id,Reader reader)
        {

            using (var db = new SSLSEntities())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {

                        Borrow borrow = db.Borrow.FirstOrDefault(c => c.BookId == id && c.ReaderId == reader.Id);// c.BookId == book.Id &&c.ReaderId == reader.Id
                       
                       borrow.ReturnDate = DateTime.Now;
                       borrow.Renew = true;

                        //DateTime x = (DateTime)borrow.ShouldReturnDate;
                        //if (DateTime.Compare(x, DateTime.Now) < 0)
                        //{
                        //    Fine fine = new Fine();
                        //    fine.BorrowId = borrow.Id;
                        //    db.Fine.Add(fine);
                        //}

                        db.SaveChanges();
                        dbContextTransaction.Commit();

                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                    }
                }
            }
           
        }
        public void ProcessContinue(int id, Reader reader)
        {
            using (var db = new SSLSEntities())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        Borrow borrow = db.Borrow.FirstOrDefault(c => c.BookId == id && c.ReaderId == reader.Id);
                        borrow.ShouldReturnDate = DateTime.Now.AddDays(30);
                        db.SaveChanges();
                        dbContextTransaction.Commit();
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();

                    }

                }
            }
        }
        public void ChangeBalanceOrder(string money, Reader reader)
        {
            using (var db = new SSLSEntities())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        decimal x = decimal.Parse(money);
                        Reader readers = db.Reader.FirstOrDefault(c => c.Id == reader.Id);
                        readers.Balance = readers.Balance + x;
                        db.SaveChanges();
                        dbContextTransaction.Commit();


                    }
                    catch (Exception) { dbContextTransaction.Rollback(); }
                }
            }
        }

        public void RegisterAccount(string username, string password)
        {
            using (var db = new SSLSEntities())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {

                        Reader reader = new Reader();
                        reader.Name = username;
                        reader.Password = password;
                        db.Reader.Add(reader);
                        db.SaveChanges();
                        dbContextTransaction.Commit();


                    }
                    catch (Exception) { dbContextTransaction.Rollback(); }
                }
            }
        }
        public void ChangePwdOrder(string pwd, Reader reader)
        {
            using (var db = new SSLSEntities())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        Reader readers = db.Reader.FirstOrDefault(c => c.Id == reader.Id);
                        readers.Password = pwd;

                        db.SaveChanges();
                        dbContextTransaction.Commit();


                    }
                    catch (Exception) { dbContextTransaction.Rollback(); }
                }
            }
        }

    }
}
