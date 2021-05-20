using CTOBusinessLogic.BindingModels;
using CTOBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTOBusinessLogic.Interfaces
{
    public interface IClientStorage
    {
        List<ClientViewModel> GetFullList();

        List<ClientViewModel> GetFilteredList(ClientBindingModel model);

        ClientViewModel GetElement(ClientBindingModel model);

        void Insert(ClientBindingModel model);

        void Update(ClientBindingModel model);

        void Delete(ClientBindingModel model);
    }
}
