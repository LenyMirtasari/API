    using API.Models;
    using API.Repository.Data;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    namespace API.Controllers
    {
        public class ProfilingsController : BaseController<Profiling, ProfilingRepository, string>
        {
            public ProfilingsController(ProfilingRepository profilingRepository) : base(profilingRepository)
            {

            }
        }
    }
