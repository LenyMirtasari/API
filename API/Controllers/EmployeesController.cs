using API.Models;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{

    //[Authorize(Roles = "Manager")]
    public class EmployeesController : BaseController <Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository repository;
        public IConfiguration _configuration;

        public EmployeesController(EmployeeRepository employeeRepository, IConfiguration configuration) : base(employeeRepository)
        {
            this.repository = employeeRepository;
            this._configuration = configuration; 
        }

        

        [Route("Register")]
        [HttpPost]
        public ActionResult Register(RegisterVM registerVM)
        {
            var validasi = repository.Register(registerVM);
            if (validasi == 1)
            {
                return Ok(new { status = HttpStatusCode.InternalServerError, result = "Internal Server Error", message = "NIK Sudah Terdafar" });
            }
            else if (validasi == 2)
            {
                return Ok(new { status = HttpStatusCode.InternalServerError, result = "Internal Server Error", message = "Email Sudah Terdafar" });
            }
            else if (validasi == 3)
            {
                return Ok(new { status = HttpStatusCode.InternalServerError, result = "Internal Server Error", message = "Phone Number Sudah Terdafar" });
            }
            else
            {
                return Ok(new { status = HttpStatusCode.OK, result = "", message = "Berhasil Memasukkan Data Baru " });
            }
        }
        [Authorize(Roles = "Director, Manager")]
        [Route("Profile")]
        [HttpGet]
        public ActionResult GetProfile()
        {
            var check = repository.GetNikProfile();
            if (check == 0)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, result = "", message = "Data Not Found " });
                
            }
            else
            {
                var result = repository.GetProfile();
                return Ok(result);
            }
        }

        [Route("Login")]
        [HttpPost]
        public ActionResult Login(LoginVM loginVM)
        {
            var check = repository.GetLogin(loginVM.Email, loginVM.Password);
           
            if (check == 1)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, result = "", message = "Wrong Email " });

            }
            else if (check == 2)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, result = "", message = "Wrong Password " });
            }
            else
            {
                var getUserRoles = repository.GetUserRole(loginVM.Email);
                
                var data = new LoginDataVM()
                {
                    Email = loginVM.Email,
                };
                var claims = new List<Claim>
                {
                    new Claim("email", data.Email),
                };
                foreach ( var a in getUserRoles )
                {
                    claims.Add(new Claim("roles", a.ToString()));
                }
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                            _configuration["Jwt:Issuer"],
                            _configuration["Jwt:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddMinutes(10),
                            signingCredentials: signIn
                            );
                var idtoken = new JwtSecurityTokenHandler().WriteToken(token);
                claims.Add(new Claim("TokenSecurity", idtoken.ToString()));
                return Ok(new { status = HttpStatusCode.OK,idtoken, message = "Login Success " });
            }
        }

        [Authorize]
        [Route("Profile/{Key}")]
        [HttpGet]
        public ActionResult GetProfile(String key)
        {
            var check = repository.GetNikCheck(key);
            if (check == 0)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, result = "", message = "Data Not Found " });
            }
            else
            {
                var result = repository.GetProfile(key);
                return Ok(result);
            }
        }

        [Authorize]
        [HttpGet("TestJWT")]
        public ActionResult TestJWT()
        {
            return Ok("Test JWT berhasil");
        }

        [Authorize(Roles = "Director")]
        [Route("SignManager/{Key}")]
        [HttpPost]
        public ActionResult SignManager(string key)
        {
            try
            {
                var result = repository.SignManager(key);
                return Ok(new { status = HttpStatusCode.OK, result, message = "Data Updated" });
            }
            catch (Exception)
            {
                return Ok(new { status = HttpStatusCode.InternalServerError, result = "", message = "Data Update Failed" });
            }
        }
        /*       [HttpPost]
               public void insertDummyData(RegisterVM registerVm)
               {
                   //--- Insert Dummy data into Student
                   employees.Add(new Employee { NIK = "12343", FirstName = "Max", LastName = " Cullen", email="leny@gmail.com", Phone ="987887767" });


                   //---- Insert Dummy data into StudentAdditionalInfo
                   accounts.Add(new Account{ NIK = "12343", Password = "123456"});

               }*/

        /*   private ActionResult View(object studentViewModel)
           {
               throw new NotImplementedException();
           }*/

        /* [HttpPost]
         public void insertDummyData(RegisterVM registerVM)
         {
             //--- Insert Dummy data into Student
             //var validasi = employeeRepository.Insert(employee);
             EmployeeRepository.Insert();


             //---- Insert Dummy data into StudentAdditionalInfo
            *//* studentAdditionalInfo.Add(new StudentAdditionalInfo { Id = 100, StudentId = 1, FavourateFruit = "Apple", Hobby = "Driving" });
             studentAdditionalInfo.Add(new StudentAdditionalInfo { Id = 101, StudentId = 2, FavourateFruit = "Mango", Hobby = "Hunting" });
             studentAdditionalInfo.Add(new StudentAdditionalInfo { Id = 102, StudentId = 3, FavourateFruit = "Bannana", Hobby = "Fishing" });
             studentAdditionalInfo.Add(new StudentAdditionalInfo { Id = 103, StudentId = 4, FavourateFruit = "Pine Apple", Hobby = "Sailing" });
             studentAdditionalInfo.Add(new StudentAdditionalInfo { Id = 104, StudentId = 5, FavourateFruit = "Grapes", Hobby = "Street Racing" });*//*

         }*/

    }
}
