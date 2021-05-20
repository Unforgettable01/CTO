using CTOBusinessLogic.BindingModels;
using CTOBusinessLogic.Interfaces;
using CTOBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;

namespace CTOBusinessLogic.BusinessLogic
{
    public class WorkLogic
    {
        private readonly IWorkStorage _workStorage;

        public WorkLogic(IWorkStorage workStorage)
        {
            _workStorage = workStorage;
        }
        public List<WorkViewModel> Read(WorkBindingModel model)
        {
            if (model == null)
            {
                return _workStorage.GetFullList();
            }
            if (model.Id.HasValue || model.WorkName != null)
            {
                return new List<WorkViewModel> { _workStorage.GetElement(model) };
            }
            return _workStorage.GetFilteredList(model);
        }

        public void CreateRooms(WorkBindingModel model)
        {
            var element = _workStorage.GetElement(new WorkBindingModel
            {
                WorkName = model.WorkName
            });
            _workStorage.Insert(model);
        }

        public void UpdateRooms(WorkBindingModel model)
        {

            _workStorage.Update(model);

        }
        public void Delete(WorkBindingModel model)
        {
            var element = _workStorage.GetElement(new WorkBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Номер не найден");
            }
            _workStorage.Delete(model);
        }
    }
}
