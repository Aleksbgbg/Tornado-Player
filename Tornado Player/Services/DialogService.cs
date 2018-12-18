namespace Tornado.Player.Services
{
    using Caliburn.Micro;
    using Caliburn.Micro.Wrapper;

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

        public TModel ShowDialog<TModel, TViewModel>()
                where TModel : new()
                where TViewModel : IDialogViewModel<TModel>
        {
            return ShowDialog<TModel, TViewModel>(new TModel());
        }

        public TModel ShowDialog<TModel, TViewModel>(TModel model)
                where TViewModel : IDialogViewModel<TModel>
        {
            TViewModel viewModel = _viewModelFactory.MakeViewModel<TViewModel>(model);

            _windowManager.ShowDialog(viewModel);

            return viewModel.Data;
        }
    }
}