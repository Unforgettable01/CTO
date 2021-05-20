using CTOBusinessLogic.BindingModels;
using CTOBusinessLogic.Interfaces;
using CTOBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
namespace CTOBusinessLogic.BusinessLogic
{
    public class RequestLogic
    {
        private readonly IRequestStorage _requestStorage;

        public RequestLogic(IRequestStorage requestStorage)
        {
            _requestStorage = requestStorage;
        }
        public List<RequestViewModel> Read(RequestBindingModel model)
        {
            if (model == null)
            {
                return _requestStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<RequestViewModel> { _requestStorage.GetElement(model) };
            }
            return _requestStorage.GetFilteredList(model);
        }

        public void CreateConference(RequestBindingModel model)
        {
            _requestStorage.Insert(model);
        }
        public void UpdateConference(RequestBindingModel model)
        {
            _requestStorage.Update(model);
        }
        public void Delete(RequestBindingModel model)
        {
            var element = _requestStorage.GetElement(new RequestBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Номер не найден");
            }
            _requestStorage.Delete(model);
        }
    }
}
