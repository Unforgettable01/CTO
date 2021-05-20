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
                .Include(rec => rec.Worker)
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
                //////??????????????????????????////////////////////////////////////

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
                    Id = conference.Id,
                    UserId = conference.UserId,
                    ConferenceName = conference.ConferenceName,
                    MembersCount = conference.MembersCount,
                    ConferenceCost = conference.ConferenceCost,
                    DateFrom = conference.DateFrom,
                    DateTo = conference.DateTo,
                     //  ConferencesRooms = conference.RoomsConferences
                     //.ToDictionary(recPC => recPC.Id, recPC => (recPC.Rooms?.RoomsType, recPC.Count, recPC.Rooms?RoomsPrice))
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
                Id = conference.Id,
                UserId = conference.UserId,
                ConferenceName = conference.ConferenceName,
                MembersCount = conference.MembersCount,
                ConferenceCost = conference.ConferenceCost,
                DateFrom = conference.DateFrom,
                DateTo = conference.DateTo,
                //// Rooms = conference.RoomsConferences
                // .Select(rec => RoomsStorage.CreateViewModel(rec.Rooms))
                // .ToList(),
                //Expenses = conference.ExpensesConference.Select(rec => ExpensesStorage.CreateViewModel(rec.Expenses)).ToList()
            };
        }
        private Request CreateModel(RequestBindingModel model, Request request)
        {
            conference.UserId = model.UserId;
            conference.ConferenceName = model.ConferenceName;
            conference.MembersCount = model.MembersCount;
            conference.ConferenceCost = model.ConferenceCost;
            conference.DateFrom = model.DateFrom;
            conference.DateTo = model.DateTo;
            return conference;
        }

        private Request CreateModel(RequestBindingModel model, Request request, CTODatabase context)
        {
            conferences.UserId = model.UserId;
            conferences.ConferenceName = model.ConferenceName;
            conferences.MembersCount = model.MembersCount;
            conferences.ConferenceCost = model.ConferenceCost;
            conferences.DateFrom = model.DateFrom;
            conferences.DateTo = model.DateTo;

            if (model.Id.HasValue)
            {
                var conferenceRoom = context.RoomsConferences.Where(rec => rec.ConferenceId == model.Id.Value).ToList();
                // удалили те, которых нет в модели
                context.RoomsConferences.RemoveRange(conferenceRoom.Where(rec => !model.ConferencesRooms.ContainsKey(rec.RoomsId)).ToList());
                context.SaveChanges();
                // обновили количество у существующих записей
                foreach (var updateComponent in conferenceRoom)
                {
                    // updateComponent.Count = model.ConferencesRooms[updateComponent.RoomId].Item2;
                    model.ConferencesRooms.Remove(updateComponent.RoomsId);
                }
                context.SaveChanges();
            }
            //добавили новые
            foreach (var pc in model.ConferencesRooms)
            {
                context.RoomsConferences.Add(new RoomConference
                {
                    ConferenceId = conferences.Id,
                    RoomsId = pc.Key,
                });
                context.SaveChanges();
            }

            return conferences;
        }
    }
}
