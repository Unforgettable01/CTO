using CTOWebApplicationClient.Models;
using CTOBusinessLogic.BindingModels;
using CTOBusinessLogic.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CTOWebApplicationClient.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }
        public IActionResult Index()
        {
            if (Program.Client == null)
            {
                return Redirect("~/Home/Enter");
            }

            return View(APIClient.GetRequest<List<RequestViewModel>>($"api/main/getrequests?clientId={Program.Client.Id}"));
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            if (Program.Client == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(Program.Client);
        }
        [HttpPost]
        public void Privacy(string email, string password)
        {
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                APIClient.PostRequest("api/client/updatedata", new ClientBindingModel
                {
                    Id = Program.Client.Id,
                    Email = email,
                    Password = password,
                });
                Program.Client.Email = email;
                Program.Client.Password = password;
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
                RequestId = Activity.Current?.Id ??
            HttpContext.TraceIdentifier
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
                Program.Client = APIClient.GetRequest<ClientViewModel>($"api/client/login?email={email}&password={password}");
                if (Program.Client == null)
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
        public void Register(string fio, string email, string password, string nphone)
        {
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                APIClient.PostRequest("api/client/register", new
                ClientBindingModel
                {
                    FIO = fio,
                    Email = email,
                    Password = password,
                });
                Response.Redirect("Enter");
                return;
            }
            throw new Exception("Введите email и пароль");
        }
        [HttpGet]
        public IActionResult CreateRequest()
        {
            ViewBag.Work = new MultiSelectList(APIClient.GetRequest<List<WorkViewModel>>($"api/main/GetWorksList"),
                "Id", "WorkName", "WorkPrice");
            return View(new RequestViewModel());
        }

        [HttpPost]
        public void CreateRequest([Bind("WorkId", "RequestName")] RequestViewModel model,  DateTime datepickerFrom, DateTime datepickerTo)
        {
            List<WorkViewModel> work = model.WorkId.
                   Select(x => APIClient.GetRequest<WorkViewModel>($"api/main/GetWorks?id={x}")).ToList();
            if (string.IsNullOrEmpty(model.RequestName) || model.WorkId.Count == 0)
            {
                return;
            }
            APIClient.PostRequest("api/main/CreateRequests", new RequestBindingModel
            {
                ClientId = Program.Client.Id,
                RequestName = model.RequestName,
                RequestCost = (work.Sum(x => x.WorkPrice)) * Convert.ToDecimal((datepickerTo - datepickerFrom).TotalDays),
                DateFrom = datepickerFrom,
                DateTo = datepickerTo,
                RequestWorks = work.ToDictionary(x => x.Id, x => (x.WorkName, x.WorkPrice))
            });

            Response.Redirect("Index");
        }
        [HttpPost]
        public decimal Calc(int works, decimal price, int date)
        {
            WorkViewModel sum = APIClient.GetRequest<WorkViewModel>($"api/main/getworks?Id={works}");
            price = (price + sum.WorkPrice) * date;
            return price;
        }
        public void DeleteRequest(int id)
        {
            APIClient.PostRequest("api/main/DeleteRequests", new RequestBindingModel { Id = id });
            Response.Redirect("../Index");
        }
        [HttpGet]
        public IActionResult UpdateRequest(string requestname)
        {
            ViewBag.RequestName = requestname;
            ViewBag.Work = APIClient.GetRequest<List<RequestViewModel>>("api/main/GetWorksList");
            return View();
        }
        [HttpPost]
        public void UpdateRequest(int id, [Bind("WorkId", "RequestName")] RequestViewModel model, DateTime datepickerFrom, DateTime datepickerTo)
        {
            List<WorkViewModel> works = model.WorkId.
                      Select(x => APIClient.GetRequest<WorkViewModel>($"api/main/GetWorks?id={x}")).ToList();

            Response.Redirect("../Index");
        }
        public IActionResult Payment()
        {
            if (Program.Client == null)
            {
                return Redirect("~/Home/Enter");
            }

            return View(APIClient.GetRequest<List<PaymentViewModel>>($"api/main/GetPaymentsList?ClientId={Program.Client.Id}"));
        }
        [HttpGet]
        public IActionResult CreatePayment()
        {
            ViewBag.Request = APIClient.GetRequest<List<RequestViewModel>>($"api/main/GetRequestsList?ClienId={Program.Client.Id}");
            ViewBag.Work = APIClient.GetRequest<List<WorkViewModel>>($"api/main/getworklist?workerId={Program.Client.Id}");
            return View();
        }
        [HttpPost]
        public void CreatePayment(int? request, int? work, string Sum)
        {
            if (request != null && work != null && !string.IsNullOrEmpty(Sum))
            {
                APIClient.PostRequest("api/main/CreatePayments", new PaymentBindingModel
                {
                    ClientId = Program.Client.Id,
                    WorkId = work.Value,
                    RequestId = request.Value,
                    Sum = Convert.ToDecimal(Sum),
                    DateOfPayment = DateTime.Now
                });
            }
            Response.Redirect("Payment");
        }
    }
}
