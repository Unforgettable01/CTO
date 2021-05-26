using CTOBusinessLogic.BindingModels;
using CTOBusinessLogic.ViewModels;
using CTOWebApplicationWorker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CTOWebApplicationWorker.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            if (Program.Worker == null)
            {
                return Redirect("~/Home/Enter");
            }

            return View(APIWorker.GetRequest<List<WorkViewModel>>($"api/main/getworks?workerId={Program.Worker.Id}"));
        }
        public IActionResult Cost()
        {
            if (Program.Worker == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIWorker.GetRequest<List<CostViewModel>>($"api/main/getcosts?workerId={Program.Worker.Id}"));
        }
        [HttpGet]
        public IActionResult Privacy()
        {
            if (Program.Worker == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(Program.Worker);
        }
        [HttpPost]
        public void Privacy(string email, string password)
        {
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                APIWorker.PostRequest("api/worker/updatedata", new WorkerBindingModel
                {
                    Id = Program.Worker.Id,
                    Email = email,
                    Password = password,
                });
                Program.Worker.Email = email;
                Program.Worker.Password = password;
                Response.Redirect("Index");
                return;
            }
            throw new Exception("Введите логин и пароль");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
        [HttpGet]
        public IActionResult Enter()
        {
            return View();
        }
        [HttpPost]
        public void Enter(string email, string password)
        {
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                Program.Worker = APIWorker.GetRequest<WorkerViewModel>($"api/worker/login?email={email}&password={password}");
                if (Program.Worker == null)
                {
                    throw new Exception("Неверный email/пароль");
                }

                Response.Redirect("Index");
                return;
            }
            throw new Exception("Введите email, пароль");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public void Register(string fio, string email, string password, string numberPhone)
        {
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                APIWorker.PostRequest("api/worker/register", new WorkerBindingModel
                {
                    FIO = fio,
                    Email = email,
                    Password = password,
                    NumberPhone=numberPhone,
                });
                Response.Redirect("Enter");
                return;
            }
            throw new Exception("Введите email и пароль");
        }
        [HttpGet]
        public IActionResult CreateWork()
        {
            return View();
        }
        [HttpPost]
        public void CreateWork(string workName, decimal workPrice)
        {
            APIWorker.PostRequest("api/main/creatework", new WorkBindingModel
            {
                WorkerId = Program.Worker.Id,
                WorkName = workName,
                WorkPrice = workPrice
            });

            Response.Redirect("Index");
        }
        [HttpGet]
        public IActionResult UpdateWork(int id)
        {
            ViewBag.Works = APIWorker.GetRequest<WorkerViewModel>($"api/main/getworks?workId={id}");
            return View();
        }
        [HttpPost]
        public void UpdateWork(int id, string workName, decimal workPrice)
        {
            var Work = APIWorker.GetRequest<WorkerViewModel>($"api/main/getworks?workId={id}");
            APIWorker.PostRequest("api/main/updateworks", new WorkBindingModel
            {
                Id = id,
                WorkerId = Work.WorkerId,
                WorkName = workName,
                WorkPrice = workPrice
            });

            Response.Redirect("../Index");
        }
        public void DeleteWork(int id)
        {
            APIWorker.PostRequest("api/main/deleteworks", new WorkBindingModel { Id = id });
            Response.Redirect("../Index");
        }

        [HttpGet]
        public IActionResult CreateCost()
        {
            return View();
        }
        [HttpPost]
        public void CreateCost(string costName, decimal costPrice)
        {
            APIWorker.PostRequest("api/main/createorupdatecosts", new CostBindingModel
            {
                WorkerId = Program.Worker.Id,
                CostName = costName,
                CostPrice = costPrice
            });

            Response.Redirect("Cost");
        }

        [HttpGet]
        public IActionResult UpdateCost(int id)
        {
            ViewBag.Cost = APIWorker.GetRequest<CostViewModel>($"api/main/getcosts?costId={id}");
            return View();
        }

        [HttpPost]
        public void UpdateCost(int id, string costName, decimal costPrice)
        {
            var Cost = APIWorker.GetRequest<WorkerViewModel>($"api/main/getcosts?costId={id}");
            APIWorker.PostRequest("api/main/createorupdatecost", new CostBindingModel
            {
                Id = id,
                WorkerId = Cost.WorkerId,
                CostName = costName,
                CostPrice = costPrice
            });

            Response.Redirect("../Cost");
        }
        [HttpGet]
        public IActionResult BindCost(int id)
        {
            ViewBag.Cost = APIWorker.GetRequest<WorkerViewModel>($"api/main/getcosts?costId={id}");
            ViewBag.Request = APIWorker.GetRequest<List<RequestViewModel>>($"api/main/GetRequestList");
            return View();
        }
        [HttpPost]
        public void BindCost(int id, string costName, decimal costPrice, int request)
        {
            RequestViewModel Request = APIWorker.GetRequest<RequestViewModel>($"api/main/getrequestnl?Id={request}");
            Dictionary<int, (string, decimal)> requestCost = Request.RequestCosts;
            if (requestCost.ContainsKey(Convert.ToInt32(id)))
            {
                requestCost[id] = (costName, costPrice);
            }
            else
            {
                requestCost.Add(id, (costName, costPrice));
            }

            APIWorker.PostRequest("api/main/updaterequests", new RequestBindingModel
            {
                Id = Request.Id,
                ClientId = Request.ClientId,
                RequestName = Request.RequestName,
                RequestWorks = Request.RequestWorks,
                RequestCosts = Request.RequestCosts,
            });

            Response.Redirect("../Cost");
        }
        public void DeleteCost(int id)
        {
            APIWorker.PostRequest("api/main/deletecosts", new CostBindingModel { Id = id });
            Response.Redirect("../Cost");
        }

        [HttpPost]
        public decimal Bind(int request)
        {
            RequestViewModel Request = APIWorker.GetRequest<RequestViewModel>($"api/main/getrequestnl?Id={request}");
            return Request.RequestCost;
        }
    }
}
