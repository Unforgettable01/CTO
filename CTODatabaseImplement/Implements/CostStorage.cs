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
    public class CostStorage : ICostStorage
    {
        public List<CostViewModel> GetFullList()
        {
            using (var context = new CTODatabase())
            {
                return context.Costs
                    .Include(rec => rec.Request)
                    .ThenInclude(rec => rec.RequestName)
                    .Select(CreateViewModel)
                    .ToList();
            }
        }

        public List<CostViewModel> GetFilteredList(CostBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new CTODatabase())
            {
                return context.Costs.Include(rec => rec.Request).Where(rec => rec.Id == model.Id 
                || rec.RequestId == model.Id)
                .Select(rec => new CostViewModel
                {
                    Id = rec.Id,
                    Reques = rec.WorkerId,
                    CostName = rec.CostName,
                    CostPrice = rec.CostPrice
                })
                .ToList();
            }
        }
        public CostViewModel GetElement(CostBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new CTODatabase())
            {
                var cost = context.Costs.Include(rec => rec.Worker).FirstOrDefault(rec => rec.Id == model.Id);
                return cost != null ?
                 new CostViewModel
                 {
                     Id = cost.Id,
                     WorkerId = cost.WorkerId,
                     CostName = cost.CostName,
                     CostPrice = cost.CostPrice
                 } :
                 null;
            }
        }
        public void Insert(CostBindingModel model)
        {
            using (var context = new CTODatabase())
            {
                context.Costs.Add(CreateModel(model, new Cost()));
                context.SaveChanges();
            }
        }

        public void Update(CostBindingModel model)
        {
            using (var context = new CTODatabase())
            {
                var element = context.Costs.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Статья затрат не найдена");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }

        public void Delete(CostBindingModel model)
        {
            using (var context = new CTODatabase())
            {
                Cost element = context.Costs.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Costs.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Статья затрат не найдена");
                }
            }
        }

        public static CostViewModel CreateViewModel(Cost cost)
        {
            return new CostViewModel
            {
                Id = cost.Id,
                WorkId = cost.WorkId,
                CostName = cost.CostName,
                CostPrice = cost.CostPrice
            };
        }
        private Cost CreateModel(CostBindingModel model, Cost cost)
        {
            cost.Id = Convert.ToInt32(model.Id);
            cost.WorkId = model.WorkId;
            cost.CostName = model.CostName;
            cost.CostPrice = model.CostPrice;
            return cost;
        }
    }
}
