using CTOBusinessLogic.BindingModels;
using CTOBusinessLogic.Interfaces;
using CTOBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;

namespace CTOBusinessLogic.BusinessLogic
{
    public class CostLogic
    {
        private readonly ICostStorage _costStorage;

        public CostLogic(ICostStorage costStorage)
        {
            _costStorage = costStorage;
        }
        public List<CostViewModel> Read(CostBindingModel model)
        {

            if (model == null)
            {
                return _costStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<CostViewModel> { _costStorage.GetElement(model) };
            }
            return _costStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(CostBindingModel model)
        {
            var element = _costStorage.GetElement(new CostBindingModel
            {
                CostName = model.CostName
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть затраты с таким названием");
            }
            if (model.Id.HasValue)
            {
                _costStorage.Update(model);
            }
            else
            {
                _costStorage.Insert(model);
            }
        }
        public void Delete(CostBindingModel model)
        {
            var element = _costStorage.GetElement(new CostBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Затраты не найдены");
            }
            _costStorage.Delete(model);
        }
    }
}
