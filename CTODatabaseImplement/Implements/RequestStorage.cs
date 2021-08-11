using CTOBusinessLogic.BindingModels;
using CTOBusinessLogic.Interfaces;
using CTOBusinessLogic.ViewModels;
using CTODatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTODatabaseImplement.Implements
{
    public class RequestStorage : IRequestStorage
    {
        public List<RequestViewModel> GetFullList()
        {
            using (var context = new CTODatabase())
            {
                return context.Requests
                .Include(rec => rec.Client)
                .Select(CreateViewModel)
                .ToList();
            }
        }

        public List<RequestViewModel> GetFilteredList(RequestBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new CTODatabase())
            {
                return context.Requests
                                 .Include(rec => rec.RequestWorks)
                               .ThenInclude(rec => rec.Work)
                               .Include(rec => rec.Client)
                               .ThenInclude(rec => rec.Requests)
                               .Where(rec => model.DateFrom.HasValue && model.DateTo.HasValue && rec.ClientId == model.ClientId ||
                                !model.DateFrom.HasValue && !model.DateTo.HasValue && rec.ClientId == model.ClientId)
                               .Select(CreateViewModel)
                               .ToList();
            }
        }

        public RequestViewModel GetElement(RequestBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new CTODatabase())
            {
                var request = context.Requests
                .Include(rec => rec.RequestWorks)
                .ThenInclude(rec => rec.Request)
                .FirstOrDefault(rec => rec.RequestName == model.RequestName || rec.Id == model.Id);
                return request != null ?
                new RequestViewModel
                {
                    Id = request.Id,
                    ClientId = request.ClientId,
                    RequestName = request.RequestName,
                 } :
                null;
            }
        }

        public void Insert(RequestBindingModel model)
        {
            using (var context = new CTODatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Request request= CreateModel(model, new Request());
                        context.Requests.Add(request);
                        context.SaveChanges();
                        CreateModel(model, request, context);
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void Update(RequestBindingModel model)
        {
            using (var context = new CTODatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var element = context.Requests.Include(rec => rec.RequestWorks)
                            .ThenInclude(rec => rec.Work).FirstOrDefault(rec => rec.Id == model.Id);
                        if (element == null)
                        {
                            throw new Exception("Заявка не найдена");
                        }
                        CreateModel(model, element, context);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void Delete(RequestBindingModel model)
        {
            using (var context = new CTODatabase())
            {
                Request element = context.Requests.Include(rec => rec.RequestWorks)
                .ThenInclude(rec => rec.Work).FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Requests.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Заявка не найдена");
                }
            }
        }
        public RequestViewModel CreateViewModel(Request request)
        {
            return new RequestViewModel
            {
                Id = request.Id,
                ClientId = request.ClientId,
                RequestName = request.RequestName,

            };
        }
        private Request CreateModel(RequestBindingModel model, Request request)
        {
            request.ClientId = model.ClientId;
            request.RequestName = model.RequestName;

            return request;
        }

        private Request CreateModel(RequestBindingModel model, Request request, CTODatabase context)
        {
            request.ClientId = model.ClientId;
            request.RequestName = model.RequestName;


            if (model.Id.HasValue)
            {
                var requestWorks = context.RequestWorks.Where(rec => rec.RequestId == model.Id.Value).ToList();
                context.RequestWorks.RemoveRange(requestWorks.Where(rec => !model.RequestWorks.ContainsKey(rec.WorkId)).ToList());
                context.SaveChanges();
                foreach (var updateComponent in requestWorks)
                { 
                    model.RequestWorks.Remove(updateComponent.WorkId);
                }
                context.SaveChanges();
            }
            foreach (var pc in model.RequestWorks)
            {
                context.RequestWorks.Add(new RequestWork
                {
                    RequestId = request.Id,
                    WorkId = pc.Key,
                });
                context.SaveChanges();
            }

            return request;
        }
    }
}
