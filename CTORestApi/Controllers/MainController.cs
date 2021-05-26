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
    public class MainController : Controller
    {
        private readonly RequestLogic _request;
        private readonly WorkLogic _work;
        private readonly RequestLogic _main;
        private readonly PaymentLogic _payment;
        private readonly CostLogic _cost;
        public MainController(RequestLogic request, WorkLogic work, RequestLogic main, PaymentLogic payment, CostLogic cost)
        {
            _request = request;
            _work = work;
            _main = main;
            _payment = payment;
            _cost = cost;
        }
        [HttpGet]
        public List<WorkViewModel> GetWorkList() => _work.Read(null)?.ToList();

        [HttpGet]
        public WorkViewModel GetWork(int Id) => _work.Read(new WorkBindingModel { Id = Id })?[0];

        [HttpGet]
        public CostViewModel GetCost(int costId) => _cost.Read(new CostBindingModel { Id = costId })?[0];

        [HttpGet]
        public List<WorkViewModel> GetWorks(int workerId) => _work.Read(new WorkBindingModel { WorkerId = workerId });

        [HttpGet]
        public List<CostViewModel> GetCosts(int workerId) => _cost.Read(new CostBindingModel { WorkerId = workerId });

        [HttpGet]
        public List<RequestViewModel> GetRequestList() => _request.Read(null)?.ToList();

        [HttpGet]
        public RequestViewModel GetRequestNL(int Id) => _request.Read(new RequestBindingModel { Id = Id })?[0];

        [HttpGet]
        public List<RequestViewModel> GetRequest(int clientId) => _request.Read(new RequestBindingModel { ClientId = clientId });

        [HttpGet]
        public List<RequestViewModel> GetRequests(int Id) => _request.Read(new RequestBindingModel { Id = Id });

        [HttpGet]
        public List<PaymentViewModel> GetPaymentList(int ClientId) => _payment.Read(new PaymentBindingModel {ClientId = ClientId });

        [HttpPost]
        public void CreateRequest(RequestBindingModel model) => _main.CreateRequest(model);

        [HttpPost]
        public void UpdateRequest(RequestBindingModel model) => _main.UpdateRequest(model);

        [HttpPost]
        public void DeleteRequest(RequestBindingModel model) => _main.Delete(model);

        [HttpPost]
        public void CreateorUpdateWork(WorkBindingModel model) => _work.CreateorUpdateWork(model);

        [HttpPost]
        public void DeleteWork(WorkBindingModel model) => _work.Delete(model);

        [HttpPost]
        public void CreateOrUpdateCost(CostBindingModel model) => _cost.CreateOrUpdate(model);

        [HttpPost]
        public void DeleteCost(CostBindingModel model) => _cost.Delete(model);

        public void CreatePayment(PaymentBindingModel model) => _payment.CreateOrUpdate(model);
    }
}
