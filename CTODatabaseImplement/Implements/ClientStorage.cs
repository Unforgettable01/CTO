using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTODatabaseImplement.Implements
{
    public class ClientStorage : IClientStorage
    {
        public List<ClientViewModel> GetFullList()
        {
            using (var context = new CTODatabase())
            {
                return context.Clients.Select(rec => new ClientViewModel
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
        public List<ClientViewModel> GetFilteredList(ClientBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new CTODatabase())
            {
                return context.Clients.Where(rec => rec.Email == model.Email && rec.Password == rec.Password)
                .Select(rec => new ClientViewModel
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

        public ClientViewModel GetElement(ClientBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new CTODatabase())
            {
                var client = context.Clients.FirstOrDefault(rec => rec.Id == model.Id || rec.Email == model.Email);
                return client != null ?
                new ClientViewModel
                {
                    Id = client.Id,
                    UserName = client.Name,
                    UserRole = client.Role,
                    Email = client.Email,
                    Password = client.Password
                } :
                null;
            }
        }

        public void Insert(ClientBindingModel model)
        {
            using (var context = new CTODatabase())
            {
                context.Clients.Add(CreateModel(model, new Client()));
                context.SaveChanges();
            }
        }

        public void Update(ClientBindingModel model)
        {
            using (var context = new CTODatabase())
            {
                var element = context.Clients.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Клиент не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }

        public void Delete(ClientBindingModel model)
        {
            using (var context = new CTODatabase())
            {
                Client element = context.Clients.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Clients.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Клиент не найден");
                }
            }
        }

        private Client CreateModel(ClientBindingModel model, Client client)
        {
            client.Name = model.UserName;
            client.Role = model.UserRole;
            client.Email = model.Email;
            client.Password = model.Password;
            return client;
        }
    }
}
