namespace Tornado.Player.ViewModels
{
    using Tornado.Player.ViewModels.Interfaces;

    internal class EditTrackViewModel : ViewModelBase, IEditTrackViewModel
    {
        public EditTrackViewModel(ITrackViewModel target)
        {
            Target = target;
        }

        public ITrackViewModel Target { get; }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;

            set
            {
                if (_isSelected == value) return;

                _isSelected = value;
                NotifyOfPropertyChange(() => IsSelected);
            }
        }
    }
}