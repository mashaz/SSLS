using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSLS.Domain.Abstract;
using SSLS.Domain.Concrete;

namespace SSLS.Domain.Abstract
{
    public interface IOrderProcessor 
    {
        void ProcessOrder(Cart cart, Reader reader);
        void ProcessReturn(int id,Reader reader);
        void ProcessContinue(int id, Reader reader);
        void ChangeBalanceOrder(string money, Reader reader);
        void RegisterAccount(string username, string pwd);
        void ChangePwdOrder(string pwd, Reader reader);
    }
}
