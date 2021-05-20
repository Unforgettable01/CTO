using CTOBusinessLogic.BindingModels;
using CTOBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTOBusinessLogic.Interfaces
{
    public interface IRequestStorage
    {
        List<RequestViewModel> GetFullList();

        List<RequestViewModel> GetFilteredList(RequestBindingModel model);

        RequestViewModel GetElement(RequestBindingModel model);

        void Insert(RequestBindingModel model);

        void Update(RequestBindingModel model);

        void Delete(RequestBindingModel model);
    }
}
