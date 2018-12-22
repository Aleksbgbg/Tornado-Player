namespace Tornado.Player.Helpers
{
    using System;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Automation.Provider;
    using System.Windows.Controls;
    using System.Windows.Input;

    internal static class InvokeOnEnterHelper
    {
        private static readonly DependencyProperty ButtonProperty = DependencyProperty.RegisterAttached("Button",
                                                                                                        typeof(Button),
                                                                                                        typeof(InvokeOnEnterHelper),
                                                                                                        new FrameworkPropertyMetadata(ButtonPropertyChanged));

        private static readonly DependencyProperty InvokeProviderProperty = DependencyProperty.RegisterAttached("InvokeProvider",
                                                                                                                typeof(IInvokeProvider),
                                                                                                                typeof(InvokeOnEnterHelper));

        internal static Button GetButton(DependencyObject dependencyObject)
        {
            return (Button)dependencyObject.GetValue(ButtonProperty);
        }

        internal static void SetButton(DependencyObject dependencyObject, Button button)
        {
            dependencyObject.SetValue(ButtonProperty, button);
        }

        internal static IInvokeProvider GetInvokeProvider(DependencyObject dependencyObject)
        {
            return (IInvokeProvider)dependencyObject.GetValue(InvokeProviderProperty);
        }

        internal static void SetInvokeProvider(DependencyObject dependencyObject, IInvokeProvider value)
        {
            dependencyObject.SetValue(InvokeProviderProperty, value);
        }

        private static void ButtonPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (!(dependencyObject is TextBox textBox))
            {
                throw new ArgumentOutOfRangeException(nameof(dependencyObject), dependencyObject, $"{nameof(InvokeOnEnterHelper)} must be applied to {nameof(TextBox)} objects only");
            }

            bool oldValueIsButton = e.OldValue is Button;

            if (e.NewValue is Button button)
            {
                IInvokeProvider invokeProvider = (IInvokeProvider)new ButtonAutomationPeer(button).GetPattern(PatternInterface.Invoke);
                Debug.Assert(invokeProvider != null);

                SetInvokeProvider(textBox, invokeProvider);

                if (!oldValueIsButton)
                {
                    textBox.KeyDown += TextBox_KeyDown;
                }
            }
            else if (oldValueIsButton)
            {
                SetInvokeProvider(textBox, null);
                textBox.KeyDown -= TextBox_KeyDown;
            }
        }

        private static void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox textBox = (TextBox)sender;

                if (GetButton(textBox).IsEnabled)
                {
                    GetInvokeProvider(textBox).Invoke();
                }
            }
        }
    }
}