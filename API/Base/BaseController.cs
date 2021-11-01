using API.Repository.Data;
using API.Repository.Interface;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[EnableCors("AllowOrigin")] 
    public class BaseController<Entity, Repository, Key> : ControllerBase
        where Entity : class
        where Repository : IRepository<Entity, Key>
    {
        private readonly Repository repository;
        public BaseController(Repository repository)
        {
            this.repository = repository;
        }

        public BaseController(AccountRepository accountRoleRepository)
        {
        }

        [HttpGet]
        public ActionResult<Entity> Get()
        {
            try
            {
                var result = repository.Get();
                return Ok(new { status = HttpStatusCode.OK, result, message = "Data Result" });
            }
            catch (Exception)
            {
                return Ok(new { status = HttpStatusCode.InternalServerError, result = "", message = "Data Not Found" });
            }
        }

        [HttpGet("{Key}")]
        public ActionResult Get(Key key)
        {
            try
            {
                var result = repository.Get(key);
                return Ok(new { status = HttpStatusCode.OK, result, message = "Data Result" });
            }
            catch (Exception)
            {
                return Ok(new { status = HttpStatusCode.InternalServerError, result = "", message = "Data Not Found" });
            }

        }

        [HttpPost]
        public ActionResult Insert(Entity entity)
        {
            try
            {
                var result = repository.Insert(entity);
                return Ok(new { status = HttpStatusCode.OK, result, message = "Data Inserted" });
            }
            catch (Exception)
            {
                return Ok(new { status = HttpStatusCode.InternalServerError, result = "", message = "Insert Data Failed" });
            }
        }



        [HttpPut]
        public ActionResult Update(Entity entity, Key key)
        {
            try {
            var result = repository.Update(entity, key);
            return Ok(new { status = HttpStatusCode.OK, result, message = "Data Updated" });
            }
            catch (Exception)
            {
                return Ok(new { status = HttpStatusCode.InternalServerError, result = "", message = "Data Update Failed" });
            }
        }

        [HttpDelete("{Key}")]
        public ActionResult Delete(Key key)
        {
            try { 
            var result=  repository.Delete(key);
            return Ok(new { status = HttpStatusCode.OK, result, message = "Data Deleted" });
            }
            catch (Exception)
            {
                return Ok(new { status = HttpStatusCode.InternalServerError, result = "", message = "Data Not Found" });
            }


        }


    }
}