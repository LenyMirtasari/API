using API.Context;
using API.Models;
using API.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
 
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        private readonly MyContext myContext;
        public AccountRepository(MyContext myContext) : base(myContext)
        {
            

        }
       /* public int Insert(RegisterVM registerVM)
        {
            myContext.Add(registerVM);
            var result = myContext.SaveChanges();
            return result;
        }*/


    }
}
