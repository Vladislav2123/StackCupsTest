using System;
using Models;
using Views;
using Controllers;
using Zenject;

namespace Architecture.Base
{
    public static class Bases
    {
        private static ControllersBase _controllersBase;
        private static ModelsBase _modelsBase;
        private static ViewsBase _viewsBase;

        public static event Action OnInitializedEvent;
        public static event Action OnResetBaseEvent;

        public static bool IsInitialized { get; private set; }

        public static void Initialize(DiContainer diContainer)
        {
            _modelsBase = new ModelsBase(diContainer);
            _viewsBase = new ViewsBase(diContainer);
            _controllersBase = new ControllersBase(diContainer);

            _modelsBase.InitializeAllModels();
            _viewsBase.InitializeAllViews();
            _controllersBase.InitializeAllControllers();

            IsInitialized = true;
            OnInitializedEvent?.Invoke();
        }

        public static void OnStart() => _controllersBase.SendOnStartToAllControllers();

        public static void ResetBase()
        {
            OnResetBaseEvent?.Invoke();
            IsInitialized = false;

            OnInitializedEvent = null;
        }

        public static T GetModel<T>() where T : IModel
        {
            return _modelsBase.GetModel<T>();
        }

        public static T GetView<T>() where T : IView
        {
            return _viewsBase.GetView<T>();
        }

        public static T GetController<T>() where T : IController
        {
            return _controllersBase.GetController<T>();
        }



    }
}