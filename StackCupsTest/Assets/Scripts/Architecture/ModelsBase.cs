using System;
using System.Collections.Generic;
using Zenject;

namespace Models
{
    public class ModelsBase
    {
        private Dictionary<Type, IModel> _modelsMap;

        private DiContainer _diContainer;

        public ModelsBase(DiContainer diContainer)
        {
            _modelsMap = new Dictionary<Type, IModel>();
            _diContainer = diContainer;
            CreateModels();
        }

        public void CreateModels()
        {
        }

        public void InitializeAllModels()
        {
            var models = _modelsMap.Values;

            foreach (var model in models)
            {
                model.Initialize();
            }
        }

        private void CreateModel<T>() where T : IModel, new()
        {
            var model = _diContainer.Instantiate<T>();
            var type = typeof(T);
            _modelsMap[type] = model;
        }

        public T GetModel<T>() where T : IModel
        {
            var type = typeof(T);
            return (T)_modelsMap[type];
        }
    }
}
