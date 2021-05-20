using CTOBusinessLogic.BindingModels;
using CTOBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTOBusinessLogic.Interfaces
{
    public interface IPaymentStorage
    {
        List<PaymentViewModel> GetFullList();

        List<PaymentViewModel> GetFilteredList(PaymentBindingModel model);

        PaymentViewModel GetElement(PaymentBindingModel model);

        void Insert(PaymentBindingModel model);

        void Update(PaymentBindingModel model);

        void Delete(PaymentBindingModel model);
    }
}
