using System;
using System.Collections.Generic;
using Zenject;

namespace Controllers
{
    public class ControllersBase
    {
        private Dictionary<Type, IController> _controllersMap;

        private DiContainer _diContainer;

        public ControllersBase(DiContainer diContainer)
        {
            _diContainer = diContainer;
            _controllersMap = new Dictionary<Type, IController>();
            CreateOntrollers();
        }

        public void CreateOntrollers()
        {
        }


        public void InitializeAllControllers()
        {
            var controllers = _controllersMap.Values;

            foreach (var controller in controllers)
            {
                controller.Initialize();
            }
        }

        public void SendOnStartToAllControllers()
        {
            var controllers = _controllersMap.Values;

            foreach (var controller in controllers)
            {
                controller.OnStart();
            }
        }

        private void CreateController<T>() where T : IController, new()
        {
            var controller = _diContainer.Instantiate<T>();
            var type = typeof(T);
            _controllersMap[type] = controller;
        }

        public T GetController<T>() where T : IController
        {
            var type = typeof(T);
            return (T)_controllersMap[type];
        }
    }
}
