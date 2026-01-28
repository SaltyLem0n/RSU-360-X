using Microsoft.AspNetCore.Mvc;
using RSU_360_X.Models;
using RSU_360_X.Services;
using System.Linq;
using System.Threading.Tasks;

namespace RSU_360_X.Controllers
{
    [Route("staff/visa")] // Maps to https://localhost:7256/staff/visa
    public class VisaStaffController : Controller
    {
        private readonly IVisaRepository _visaRepo;

        public VisaStaffController(IVisaRepository visaRepo)
        {
            _visaRepo = visaRepo;
        }


    }
}
