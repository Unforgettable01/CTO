using CTODatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTODatabaseImplement.Implements
{
    public class WorkStorage : IWorkStorage
    {
        public List<WorkViewModel> GetFullList()
        {
            using (var context = new CTODatabase())
            {
                return context.Works.Select(CreateViewModel).ToList();
            }
        }

        public List<WorkViewModel> GetFilteredList(WorkBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new CTODatabase())
            {
                return context.Works.Include(rec => rec.Worker)
                .Select(rec => new WorkViewModel
                {
                    Id = rec.Id,
                    UserId = rec.UserId,
                    RoomsType = rec.RoomsType,
                    RoomsPrice = rec.RoomsPrice
                })
                .ToList();
            }
        }

        public WorkViewModel GetElement(WorkBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new CTODatabase())
            {
                var work = context.Works.Include(rec => rec.Worker)
                .FirstOrDefault(rec => rec.Id == model.Id || rec.WorkName == model.WorkName);
                return work != null ? CreateViewModel(work) : null;
            }
        }

        public void Insert(WorkBindingModel model)
        {
            using (var context = new CTODatabase())
            {
                context.Works.Add(CreateModel(model, new Work()));
                context.SaveChanges();
            }
        }

        public void Update(WorkBindingModel model)
        {
            using (var context = new CTODatabase())
            {
                var element = context.Works.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Такого ремонта нет");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }

        public void Delete(WorkBindingModel model)
        {
            using (var context = new CTODatabase())
            {
                Work element = context.Works.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Works.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Такого ремонта нет");
                }
            }
        }
        public static WorkViewModel CreateViewModel(Work work)
        {
            return new WorkViewModel
            {
                UserId = work.UserId,
                Id = work.Id,
                RoomsType = work.RoomsType,
                RoomsPrice = work.RoomsPrice
            };
        }
        private Work CreateModel(WorkBindingModel model, Work work)
        {
            work.UserId = model.UserId;
            work.RoomsType = model.RoomsType;
            work.RoomsPrice = model.RoomsPrice;
            return work;
        }
    }
}
