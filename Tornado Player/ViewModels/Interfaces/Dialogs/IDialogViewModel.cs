namespace Tornado.Player.ViewModels.Interfaces.Dialogs
{
    internal interface IDialogViewModel<out TModel>
    {
        TModel Data { get; }
    }
}