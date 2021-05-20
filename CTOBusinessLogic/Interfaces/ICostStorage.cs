using CTOBusinessLogic.BindingModels;
using CTOBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTOBusinessLogic.Interfaces
{
    public interface ICostStorage
    {
        List<CostViewModel> GetFullList();

        List<CostViewModel> GetFilteredList(CostBindingModel model);

        CostViewModel GetElement(CostBindingModel model);

        void Insert(CostBindingModel model);

        void Update(CostBindingModel model);

        void Delete(CostBindingModel model);
    }
}
