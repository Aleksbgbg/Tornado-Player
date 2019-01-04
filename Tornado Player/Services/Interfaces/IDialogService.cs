namespace Tornado.Player.Services.Interfaces
{
    using Tornado.Player.Models.Dialogs;
    using Tornado.Player.ViewModels.Interfaces.Dialogs;

    internal interface IDialogService
    {
        TModel ShowDialog<TModel, TViewModel>(WindowSettings windowSettings = default)
                where TModel : new()
                where TViewModel : IDialogViewModel<TModel>;

        TModel ShowDialog<TModel, TViewModel>(TModel model, WindowSettings windowSettings = default)
                where TViewModel : IDialogViewModel<TModel>;
    }
}