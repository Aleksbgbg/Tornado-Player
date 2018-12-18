namespace Tornado.Player.Services.Interfaces
{
    using Tornado.Player.ViewModels.Interfaces.Dialogs;

    internal interface IDialogService
    {
        TModel ShowDialog<TModel, TViewModel>() where TModel : new()
                                                where TViewModel : IDialogViewModel<TModel>;

        TModel ShowDialog<TModel, TViewModel>(TModel model) where TViewModel : IDialogViewModel<TModel>;
    }
}