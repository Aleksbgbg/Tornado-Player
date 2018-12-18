namespace Tornado.Player.ViewModels.Dialogs
{
    using Tornado.Player.ViewModels.Interfaces.Dialogs;

    internal abstract class DialogViewModel<TModel> : ViewModelBase, IDialogViewModel<TModel>
    {
        private protected DialogViewModel(TModel data)
        {
            Data = data;
        }

        public TModel Data { get; }
    }
}