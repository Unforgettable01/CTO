using CTOBusinessLogic.BindingModels;
using CTOBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTOBusinessLogic.Interfaces
{
    public interface IWorkerStorage
    {
        List<WorkerViewModel> GetFullList();

        List<WorkerViewModel> GetFilteredList(WorkerBindingModel model);

        WorkerViewModel GetElement(WorkerBindingModel model);

        void Insert(WorkerBindingModel model);

        void Update(WorkerBindingModel model);

        void Delete(WorkerBindingModel model);
    }
}
