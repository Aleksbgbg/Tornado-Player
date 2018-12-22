namespace Tornado.Player.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Caliburn.Micro;
    using Caliburn.Micro.Wrapper;

    using Tornado.Player.Models;
    using Tornado.Player.Services.Interfaces;
    using Tornado.Player.ViewModels.Interfaces.Dialogs;

    internal class DialogService : IDialogService
    {
        private readonly IWindowManager _windowManager;

        private readonly IViewModelFactory _viewModelFactory;

        public DialogService(IWindowManager windowManager, IViewModelFactory viewModelFactory)
        {
            _windowManager = windowManager;
            _viewModelFactory = viewModelFactory;
        }

        public TModel ShowDialog<TModel, TViewModel>(WindowSettings windowSettings = default)
                where TModel : new()
                where TViewModel : IDialogViewModel<TModel>
        {
            return ShowDialog<TModel, TViewModel>(new TModel(), windowSettings);
        }

        public TModel ShowDialog<TModel, TViewModel>(TModel model, WindowSettings windowSettings = default)
                where TViewModel : IDialogViewModel<TModel>
        {
            TViewModel viewModel = _viewModelFactory.MakeViewModel<TViewModel>(model);

            if (windowSettings == null)
            {
                _windowManager.ShowDialog(viewModel);
            }
            else
            {
                Dictionary<string, object> settings = typeof(WindowSettings).GetProperties()
                                                                            .Select(property => new
                                                                            {
                                                                                Name = property.Name,
                                                                                Value = property.GetValue(windowSettings)
                                                                            })
                                                                            .Where(property => property.Value != null)
                                                                            .ToDictionary(property => property.Name,
                                                                                          property => property.Value);

                _windowManager.ShowDialog(viewModel, settings: settings);
            }

            return viewModel.Data;
        }
    }
}