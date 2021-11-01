/*using API.Models;
using API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesControllerOld : ControllerBase
    {
        private readonly EmployeeRepositoryOld employeeRepository;
        public EmployeesControllerOld(EmployeeRepositoryOld employeeRepository)
        {
            this.employeeRepository = employeeRepository;

        }

        [HttpPost]
        public ActionResult Post(Employee employee)
        {
          {
                var validasi = employeeRepository.Insert(employee);
                if (validasi == 1)
                {
                    return Ok(new { status = HttpStatusCode.InternalServerError, result = "Internal Server Error", message = "NiK Tidak Boleh Kosong " });
                }
                else if (validasi == 2)
                {
                    return Ok(new { status = HttpStatusCode.InternalServerError, result = "Internal Server Error", message = "NiK Tidak Boleh Null " });
                }
                else if (validasi == 3)
                {
                    return Ok(new { status = HttpStatusCode.InternalServerError, result = "Internal Server Error", message = "NIK Sudah Terdafar" });
                }
                else if (validasi == 4)
                {
                    return Ok(new { status = HttpStatusCode.InternalServerError, result = "Internal Server Error", message = "Email Sudah Terdafar" });
                }
                else if (validasi == 5)
                {
                    return Ok(new { status = HttpStatusCode.InternalServerError, result = "Internal Server Error", message = "Phone Number Sudah Terdafar" });
                }
                else
                {
                    return Ok(new { status = HttpStatusCode.OK, result = "", message = "Berhasil Memasukkan Data Baru " });
                }
            }
        }


        [HttpGet]
        public ActionResult Get()
        {          
            var check = employeeRepository.GetCheck();
            if ( check == 1){
                var datas = employeeRepository.Get();
                return Ok(new { status = HttpStatusCode.OK,datas, message = "Data Employee" });              
            }
            else
            {
                return NotFound(new { status = HttpStatusCode.NotFound, result = "", message = "Data Not Found " });
            }       
        }

        [HttpGet("{NIK}")]
        public ActionResult Get(string NIK)
        {
            var check = employeeRepository.GetNikCheck(NIK);
            if (check == 0)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, result="", message = "Data Not Found " });
            }
            else
            {
                var result = employeeRepository.Get(NIK);
                return Ok(new { status = HttpStatusCode.OK, result, message = "Data found " });
            }

        }
     
        [HttpDelete("{NIK}")]
        public ActionResult Delete(string NIK)
        {
            try { 
            employeeRepository.Delete(NIK);
            return Ok(new { message = $"Berhasil Menghapus NIK : {NIK} " });
            }
            catch (Exception)
            {
                return Ok(new { status = HttpStatusCode.InternalServerError, result = "", message = "Data Not Found" });
            }
        }

        [HttpPut]
        public ActionResult Update(Employee employee)
        {
            try { 
                employeeRepository.Update(employee);
                return Ok(new {status = HttpStatusCode.OK,result ="",  message = $" Update NIK : {employee.NIK} " });
            }
            catch (Exception)
            {
                return Ok(new { status = HttpStatusCode.InternalServerError, result = "", message = "Update Failed NIK Not Found " });
            }
        }

        [HttpPatch]
        public ActionResult UpdatePatch(Employee employee)
        {
            try
            {
                employeeRepository.Update(employee);
                return Ok(new { status = HttpStatusCode.OK, result = "", message = $" Update NIK : {employee.NIK} " });
            }
            catch (Exception)
            {
                return Ok(new { status = HttpStatusCode.InternalServerError, result = "", message = "Update Failed NIK Not Found " });
            }
        }
    }
}
*/