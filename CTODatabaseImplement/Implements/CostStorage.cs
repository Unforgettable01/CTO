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
                return context.Costs.Include(rec => rec.Worker).Select(rec => new CostViewModel
                {
                    Id = rec.Id,
                    UserId = rec.UserId,
                    ExpensesName = rec.ExpensesName,
                    ExpensesPrice = rec.ExpensesPrice
                })
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
                return context.Costs.Include(rec => rec.Worker).Where(rec => rec.Id == model.Id 
                || rec.WorkerId == model.WorkerId)
                .Select(rec => new CostViewModel
                {
                    Id = rec.Id,
                    UserId = rec.UserId,
                    ExpensesName = rec.ExpensesName,
                    ExpensesPrice = rec.ExpensesPrice

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
                     Id = expenses.Id,
                     UserId = expenses.UserId,
                     ExpensesName = expenses.ExpensesName,
                     ExpensesPrice = expenses.ExpensesPrice
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
                UserId = cost.UserId,
                ExpensesName = cost.ExpensesName,
                ExpensesPrice = cost.ExpensesPrice
            };
        }
        private Cost CreateModel(CostBindingModel model, Cost cost)
        {
            cost.UserId = model.UserId;
            cost.ExpensesName = model.ExpensesName;
            cost.ExpensesPrice = model.ExpensesPrice;
            return cost;
        }
    }
}
