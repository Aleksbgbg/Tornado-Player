namespace Tornado.Player.Helpers
{
    using System.Windows.Controls;
    using System.Windows.Interactivity;

    internal class PreventItemFocusOnRightClickBehavior : Behavior<ListBox>
    {
        protected override void OnAttached()
        {
            AssociatedObject.PreviewMouseRightButtonDown += (sender, e) => e.Handled = true;
        }
    }
}