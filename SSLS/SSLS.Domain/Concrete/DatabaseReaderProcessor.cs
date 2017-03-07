using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSLS.Domain.Abstract;
using SSLS.Domain.Concrete;

namespace SSLS.Domain.Concrete
{
    public class DatabaseReaderProcessor :IReaderProcessor
    {
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
        public void ChangeBalanceOrder(string money,Reader reader)
        {
            using (var db = new SSLSEntities())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        decimal x =  decimal.Parse(money);
                        Reader readers = db.Reader.FirstOrDefault(c => c.Id == reader.Id);
                        readers.Balance = readers.Balance + x;
                        db.SaveChanges();
                        dbContextTransaction.Commit();


                    }
                    catch (Exception) { dbContextTransaction.Rollback(); }
                }
            }
        }
    }
}
