using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSLS.Domain.Abstract;
using SSLS.Domain.Concrete;

namespace SSLS.Domain.Abstract
{
   public interface IReaderProcessor
    {
       void ChangePwdOrder(string pwd, Reader reader);
       void ChangeBalanceOrder(string money,Reader reader);
      
    }
}
