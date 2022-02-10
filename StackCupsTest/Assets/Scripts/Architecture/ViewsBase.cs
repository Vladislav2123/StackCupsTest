using System;
using System.Collections.Generic;
using Zenject;

namespace Views
{
    public class ViewsBase
    {
        private Dictionary<Type, IView> _viewsMap;
        private DiContainer _diContainer;

        public ViewsBase(DiContainer diContainer)
        {
            _viewsMap = new Dictionary<Type, IView>();
            _diContainer = diContainer;
            CreateViews();
        }

        public void CreateViews()
        {
        }

        public void InitializeAllViews()
        {
            var views = _viewsMap.Values;

            foreach (var view in views)
            {
                view.Initialize();
            }
        }

        private void CreateView<T>() where T : IView, new()
        {
            var view = _diContainer.Instantiate<T>();
            var type = typeof(T);
            _viewsMap[type] = view;
        }

        public T GetView<T>() where T : IView
        {
            var type = typeof(T);
            return (T)_viewsMap[type];
        }
    }
}
