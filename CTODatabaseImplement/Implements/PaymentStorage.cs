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
    public class PaymentStorage : IPaymentStorage
    {
        public List<PaymentViewModel> GetFullList()
        {
            using (var context = new CTODatabase())
            {
                return context.Payments.Include(rec => rec.Client)
                .Include(rec => rec.Request)
                .Select(CreateViewModel)
               .ToList();
            }
        }

        public List<PaymentViewModel> GetFilteredList(PaymentBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new CTODatabase())
            {
                return context.Payments.Where(rec => rec.Id == model.Id).Include(rec => rec.Client)
                .Include(rec => rec.Request)
                .Select(CreateViewModel)
                 .ToList();
            }
        }

        public PaymentViewModel GetElement(PaymentBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new CTODatabase())
            {
                var payment = context.Payments.Include(rec => rec.Client)
                   .Include(rec => rec.Request).FirstOrDefault(rec => rec.ClientId == model.ClientId);
                return payment != null ?
                new PaymentViewModel
                {
                    Id = payment.Id,
                    ClientId = payment.ClientId,
                    RwqusetId = payment.RequestId,
                    RequestName = payment.Request.RequestName,
                    Sum = (decimal)payment.Sum,
                    DateOfPayment = payment.DateOfPayment
                } :
                null;
            }
        }

        public void Insert(PaymentBindingModel model)
        {
            using (var context = new CTODatabase())
            {
                context.Payments.Add(CreateModel(model, new Payment()));
                context.SaveChanges();
            }
        }

        public void Update(PaymentBindingModel model)
        {
            using (var context = new CTODatabase())
            {
                var element = context.Payments.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Оплата не найдена");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }

        public void Delete(PaymentBindingModel model)
        {
            using (var context = new CTODatabase())
            {
                Payment element = context.Payments.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Payments.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Оплата не найдена");
                }
            }
        }
        private PaymentViewModel CreateViewModel(Payment payment)
        {
            return new PaymentViewModel
            {
                Id = payment.Id,
                ClientId = payment.ClientId,
                ReuestId = payment.RequestId,
                WorkName = payment.Request.RequestName,
                Sum = (decimal)payment.Sum,
                DateOfPayment = payment.DateOfPayment
            };
        }
        private Payment CreateModel(PaymentBindingModel model, Payment payment)
        {
            payment.ClientId = model.ClientId;
            payment.RequestId = (int)model.RequestId;
            payment.Sum = model.Sum;
            payment.DateOfPayment = model.DateOfPayment;
            return payment;
        }
    }
}
