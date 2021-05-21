using CTOBusinessLogic.BindingModels;
using CTOBusinessLogic.BusinessLogic;
using CTOBusinessLogic.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTORestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WorkerController : Controller
    {
        private readonly WorkerLogic _logic;
        public WorkerController(WorkerLogic logic)
        {
            _logic = logic;
        }
        [HttpGet]
        public WorkerViewModel Login(string email, string password) => _logic.Read(new WorkerBindingModel { Email = email, Password = password })?[0];

        [HttpGet]
        public WorkerViewModel UserList() => _logic.Read(null)?[0];

        [HttpPost]
        public void Register(WorkerBindingModel model) => _logic.CreateOrUpdate(model);
        [HttpPost]
        public void UpdateData(WorkerBindingModel model) => _logic.CreateOrUpdate(model);
    }
}
