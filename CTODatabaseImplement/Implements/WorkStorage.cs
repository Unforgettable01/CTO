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
                    WorkerId = rec.WorkerId,
                    WorkName = rec.WorkName,
                    WorkPrice = rec.WorkPrice
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
                Id = work.Id,
                WorkerId = work.WorkerId,
                WorkName = work.WorkName,
                WorkPrice = work.WorkPrice
            };
        }
        private Work CreateModel(WorkBindingModel model, Work work)
        {
            work.WorkerId = model.WorkerId;
            work.WorkName = model.WorkName;
            work.WorkPrice = model.WorkPrice;
            return work;
        }
    }
}
