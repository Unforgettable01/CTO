using CTODatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTODatabaseImplement.Implements
{
    public class WorkerStorage : IWorkerStorage
    {
        public List<WorkerViewModel> GetFullList()
        {
            using (var context = new CTODatabase())
            {
                return context.Workers.Select(rec => new WorkerViewModel
                {
                    Id = rec.Id,
                    FIO = rec.Name,
                    Email = rec.Email,
                    Password = rec.Password,
                    UserRole = rec.Role,
                })
                .ToList();
            }
        }
        public List<WorkerViewModel> GetFilteredList(WorkerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new CTODatabase())
            {
                return context.Workers.Where(rec => rec.Email == model.Email && rec.Password == rec.Password)
                .Select(rec => new WorkerViewModel
                {
                    Id = rec.Id,
                    UserName = rec.Name,
                    UserRole = rec.Role,
                    Email = rec.Email,
                    Password = rec.Password
                })
                .ToList();
            }
        }

        public WorkerViewModel GetElement(WorkerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new CTODatabase())
            {
                var worker = context.Workers.FirstOrDefault(rec => rec.Id == model.Id || rec.Email == model.Email);
                return worker != null ?
                new WorkerViewModel
                {
                    Id = worker.Id,
                    UserName = worker.Name,
                    UserRole = worker.Role,
                    Email = worker.Email,
                    Password = worker.Password
                } :
                null;
            }
        }

        public void Insert(WorkerBindingModel model)
        {
            using (var context = new CTODatabase())
            {
                context.Workers.Add(CreateModel(model, new Worker()));
                context.SaveChanges();
            }
        }

        public void Update(WorkerBindingModel model)
        {
            using (var context = new CTODatabase())
            {
                var element = context.Workers.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Сотрудник не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }

        public void Delete(WorkerBindingModel model)
        {
            using (var context = new CTODatabase())
            {
                Worker element = context.Workers.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Workers.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Сотрудник не найден");
                }
            }
        }

        private Client CreateModel(WorkerBindingModel model, Worker worker)
        {
            worker.Name = model.UserName;
            worker.Role = model.UserRole;
            worker.Email = model.Email;
            worker.Password = model.Password;
            return worker;
        }
    }
}
