using API.Context;
using API.Models;
using API.PasswordHashing;
using API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{

    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        private readonly MyContext myContext;
     
        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;

        }
    /*    private static string GetRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);        
        }*/
     
        public int Register(RegisterVM registeVM)
        {   var nikInput = registeVM.NIK;
            var emailInput = registeVM.Email;
            var phoneInput = registeVM.PhoneNumber;
            var datas = myContext.Employees.ToList();
            foreach (var data1 in datas)
            {
                if (data1.NIK == nikInput)
                {
                    return 1;
                }
                else if (data1.email == emailInput)
                {
                    return 2;
                }
                else if (data1.Phone == phoneInput)
                {
                    return 3;
                }

            }
            Employee e = new Employee();
            e.NIK = registeVM.NIK;
            e.FirstName = registeVM.FirstName;
            e.LastName = registeVM.LastName;
            e.Phone = registeVM.PhoneNumber;
            e.BirthDate = registeVM.BirthDate;
            e.salary = registeVM.Salary;
            e.email = registeVM.Email;
            e.Gender = registeVM.Gender;
            myContext.Add(e);
            myContext.SaveChanges();

            Account a = new Account();
            a.NIK = registeVM.NIK;
            a.Password = Hashing.HashPassword(registeVM.Password);
           // a.Password = registeVM.Password;
            myContext.Add(a);
            myContext.SaveChanges();

            Education edu = new Education();
            edu.Degree = registeVM.Degree;
            edu.GPA = registeVM.Gpa;
            edu.UniversityId = registeVM.UniversityId;
            myContext.Add(edu);
            myContext.SaveChanges();

            Profiling p = new Profiling();
            p.NIK = registeVM.NIK;
            p.EducationId = edu.Id;
            myContext.Add(p);
            myContext.SaveChanges();

            AccountRole ar = new AccountRole();
            ar.NIK = registeVM.NIK;
            ar.RoleId = 1 ;
            myContext.Add(ar);
            myContext.SaveChanges();
            return 0;                
        }

        
        public IEnumerable GetProfile()
        {
            var result = from emp in myContext.Employees
                         join acc in myContext.Accounts on emp.NIK equals acc.NIK
                         join prof in myContext.Profilings on acc.NIK equals prof.NIK
                         join edu in myContext.Educations on prof.EducationId equals edu.Id
                         join univ in myContext.Universities on edu.UniversityId equals univ.Id
                         select new
                         {
                             NIK = emp.NIK,
                             FullName = emp.FirstName + " " + emp.LastName,
                             PhoneNumber = emp.Phone,                        
                             BirthDate = emp.BirthDate,
                             Salary = emp.salary,
                             Email = emp.email,
                             Degree = edu.Degree,
                             Gpa = edu.GPA,
                             UniversityName = univ.Name
                         };
            return result;
            
        }
        public string GetUserData(String emailInput)
        {
            var nik = myContext.Employees.Where(p => p.email == emailInput).FirstOrDefault();
            var roleId = myContext.AccountRoles.Where(p => p.NIK == nik.NIK).FirstOrDefault();
            string roleName = myContext.Roles.Find(roleId.RoleId).RoleName;

            return roleName;
        }
        /*var innerJoin = myContext.Employees.Join(
                       myContext.Accounts,
                       emp => emp.NIK,
                       acc => acc.NIK,
                      (emp, acc) => new
                      {
                          NIK = emp.NIK,
                          FullName = emp.FirstName + " " + emp.LastName,
                          PhoneNumber = emp.Phone,
                          BirtDate = emp.BirthDate,

                      }).Join(myContext.Profilings,
                      acc => acc.NIK,
                      prof => prof.NIK,
                      (acc, prof) => new
                      { prof.EducationId }).Join(myContext.Educations,
                      prof => prof.EducationId,
                      edu => edu.Id,
                      (prof, edu) => new
                      {
                          edu.UniversityId,
                          Degree = edu.Degree,
                      }).Join(myContext.Universities,
                      edu => edu.UniversityId,
                      univ => univ.Id,
                      (edu, univ) => new
                      {
                          NIk = univ.Name
                      }
                      )
                      ;*/
        public IEnumerable GetProfile(String key)
        {
            var result = from emp in myContext.Employees
                         join acc in myContext.Accounts on emp.NIK equals acc.NIK
                         join prof in myContext.Profilings on acc.NIK equals prof.NIK
                         join edu in myContext.Educations on prof.EducationId equals edu.Id
                         join univ in myContext.Universities on edu.UniversityId equals univ.Id
                         where emp.NIK == key
                         select new
                         {
                             NIK = emp.NIK,
                             FullName = emp.FirstName + " " + emp.LastName,
                             PhoneNumber = emp.Phone,
                             BirthDate = emp.BirthDate,
                             Salary = emp.salary,
                           //  Password = acc.Password,
                             Email = emp.email,
                             Degree = edu.Degree,
                             Gpa = edu.GPA,
                             UniversityName = univ.Name
                         };

            return result;
        }
      
        public int GetLogin(string emailInput, string passwordInput)
        {             
            try
            {
                var checkEmail = myContext.Employees.Where(p => p.email == emailInput).FirstOrDefault();
                var NIK = (from emp in myContext.Employees where emp.email == emailInput select emp.NIK).Single();
                var password = (from emp in myContext.Employees
                                join acc in myContext.Accounts on emp.NIK equals acc.NIK
                                where emp.email == emailInput
                                select acc.Password).Single();
                var validPw =  Hashing.ValidatePassword(passwordInput, password);
               if (checkEmail != null)
                    {
                        if (validPw == true)
                        {
                        return 0;

                        }
                        else if(validPw == false)
                        {
                            return 2;
                        }                  

                    }
            }
            catch (InvalidOperationException)
            {
                return 1;
            }
            return 3;
        }

        public int GetNikCheck(string NIK)
        {
            var datas =myContext.Employees.Find(NIK) ;
            if (datas != null )
            {
                return 1;
            }
            return 0;
        }
        public int GetNikProfile()
        {
            var datas = myContext.Employees.Count();
            if (datas >0)
            {
                return 1;
            }
            return 0;
        }
        public string[] GetUserRole(string emailInput)
        {
            var userNIK = (from emp in myContext.Employees where emp.email == emailInput select emp.NIK).FirstOrDefault();
            var roles = myContext.AccountRoles.Where(a=>a.NIK== userNIK).ToList();
            List<string> result = new List<string>();
            foreach(var item in roles)
            {
                result.Add(myContext.Roles.Where(a => a.RoleId == item.RoleId).First().RoleName);
            }
            return result.ToArray();
        }
    /*    public int signManager(string key)
        {
            AccountRole f = myContext.AccountRoles.FirstOrDefault(x => x.NIK == key);
            f.RoleId = 2;
            var result = myContext.SaveChanges();
            return result;
        }*/
        public int SignManager(string key)
        {
            AccountRole ar = new AccountRole();
            ar.NIK = key;
            ar.RoleId = 2;
            myContext.Add(ar);
            var result= myContext.SaveChanges();
            return result;
        }
    }
}