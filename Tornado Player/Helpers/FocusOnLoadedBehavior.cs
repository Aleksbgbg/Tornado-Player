namespace Tornado.Player.Helpers
{
    using System.Windows.Controls;
    using System.Windows.Interactivity;

    internal class FocusOnLoadedBehavior : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            AssociatedObject.Loaded += (sender, e) => AssociatedObject.Focus();
        }
    }
}