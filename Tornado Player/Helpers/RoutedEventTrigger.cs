namespace Tornado.Player.Helpers
{
    using System;
    using System.Windows;
    using System.Windows.Interactivity;

    internal class RoutedEventTrigger : EventTriggerBase<DependencyObject>
    {
        internal RoutedEvent RoutedEvent { get; set; }

        protected override void OnAttached()
        {
            FrameworkElement associatedElement;

            if (AssociatedObject is Behavior associatedBehavior)
            {
                associatedElement = ((IAttachedObject)associatedBehavior).AssociatedObject as FrameworkElement;
            }
            else
            {
                associatedElement = AssociatedObject as FrameworkElement;
            }

            if (associatedElement == null)
            {
                throw new InvalidOperationException($"Cannot attach a {nameof(RoutedEventTrigger)} to a non-{nameof(FrameworkElement)} object.");
            }

            if (RoutedEvent != null)
            {
                associatedElement.AddHandler(RoutedEvent, new RoutedEventHandler(OnRoutedEvent));
            }
        }

        protected override string GetEventName()
        {
            return RoutedEvent.Name;
        }

        private void OnRoutedEvent(object sender, RoutedEventArgs args)
        {
            OnEvent(args);
        }
    }
}