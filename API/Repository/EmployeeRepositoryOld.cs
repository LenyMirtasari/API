/*using API.Context;
using API.Models;
using API.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Repository
{
    public class EmployeeRepositoryOld : IEmployeeRepository
    {
        private readonly MyContext context;
        public EmployeeRepositoryOld(MyContext context)
        {
            this.context = context;

        }
        public int Delete(string NIK)
        {
            var entity = context.Employees.Find(NIK);
            context.Remove(entity);
            var result = context.SaveChanges();
            return result;
        }

        public IEnumerable<Employee> Get()
        {   
            return context.Employees.ToList();
        }

        public int GetCheck()
        {
            var datas = Get();      
            if (datas.Count() > 0)
            {
                return 1;
            }
            return 0;
        }

        public Employee Get(string NIK)
        {
            return context.Employees.Find(NIK);
        }

        public int GetNikCheck(string NIK)
        {
            var datas = Get(NIK);
            if (datas == null)
            {
                return 0;
            }
            return 1;
        }


        public int Insert(Employee employee)
        {
            var nikInput = employee.NIK;
            var emailInput = employee.email;
            var phoneInput = employee.Phone;
            var data = Get();

            if (nikInput == "")
            {
                return 1;
            }
            else if (nikInput == null)
            {
                return 2;
            }
            else
            {

                foreach (var data1 in data)
                {
                    if (data1.NIK == nikInput)
                    {
                        return 3;
                    }
                    else if (data1.email == emailInput)
                    {
                        return 4;
                    }
                    else if (data1.Phone == phoneInput)
                    {
                        return 5;
                    }

                }
                context.Employees.Add(employee);
                var result = context.SaveChanges();
                return 0;
            }
        }


        public int Update(Employee employee)
        {
            context.Entry(employee).State = EntityState.Modified;
            var result = context.SaveChanges();
            return result;
        }
    }
}*/