namespace Tornado.Player.Controls
{
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Data;

    using Tornado.Player.Models.Settings;
    using Tornado.Player.Utilities;

    using Xceed.Wpf.Toolkit.PropertyGrid;
    using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

    public partial class HotKeyBindEditor : ITypeEditor
    {
        private static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof(Value),
                                                                                               typeof(HotKeyBind),
                                                                                               typeof(HotKeyBindEditor),
                                                                                               new FrameworkPropertyMetadata(ValuePropertyChanged));

        private static readonly DependencyProperty EnableControlProperty = DependencyProperty.Register(nameof(EnableControl),
                                                                                                       typeof(bool),
                                                                                                       typeof(HotKeyBindEditor),
                                                                                                       new FrameworkPropertyMetadata(EnableControlPropertyChanged));

        private static readonly DependencyProperty EnableShiftProperty = DependencyProperty.Register(nameof(EnableShift),
                                                                                                     typeof(bool),
                                                                                                     typeof(HotKeyBindEditor),
                                                                                                     new FrameworkPropertyMetadata(EnableShiftPropertyChanged));

        private static readonly DependencyProperty EnableAltProperty = DependencyProperty.Register(nameof(EnableAlt),
                                                                                                   typeof(bool),
                                                                                                   typeof(HotKeyBindEditor),
                                                                                                   new FrameworkPropertyMetadata(EnableAltPropertyChanged));

        private static readonly DependencyProperty EnableWindowsKeyProperty = DependencyProperty.Register(nameof(EnableWindowsKey),
                                                                                                          typeof(bool),
                                                                                                          typeof(HotKeyBindEditor),
                                                                                                          new FrameworkPropertyMetadata(EnableWindowsKeyPropertyChanged));

        public HotKeyBindEditor()
        {
            InitializeComponent();
        }

        internal HotKeyBind Value
        {
            get => (HotKeyBind)GetValue(ValueProperty);

            set => SetValue(ValueProperty, value);
        }

        internal bool EnableControl
        {
            get => (bool)GetValue(EnableControlProperty);

            set => SetValue(EnableControlProperty, value);
        }

        internal bool EnableShift
        {
            get => (bool)GetValue(EnableShiftProperty);

            set => SetValue(EnableShiftProperty, value);
        }

        internal bool EnableAlt
        {
            get => (bool)GetValue(EnableAltProperty);

            set => SetValue(EnableAltProperty, value);
        }

        internal bool EnableWindowsKey
        {
            get => (bool)GetValue(EnableWindowsKeyProperty);

            set => SetValue(EnableWindowsKeyProperty, value);
        }

        public FrameworkElement ResolveEditor(PropertyItem propertyItem)
        {
            Binding binding = new Binding(nameof(Value))
            {
                Source = propertyItem,
                Mode = propertyItem.IsReadOnly ? BindingMode.OneWay : BindingMode.TwoWay
            };

            BindingOperations.SetBinding(this, ValueProperty, binding);

            return this;
        }

        private static void ValuePropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            HotKeyBindEditor hotKeyBindEditor = (HotKeyBindEditor)dependencyObject;

            HotKeyModifiers modifiers = ((HotKeyBind)e.NewValue).Modifiers;

            hotKeyBindEditor.EnableControl = modifiers.HasFlag(HotKeyModifiers.ControlKey);
            hotKeyBindEditor.EnableShift = modifiers.HasFlag(HotKeyModifiers.ShiftKey);
            hotKeyBindEditor.EnableAlt = modifiers.HasFlag(HotKeyModifiers.AltKey);
            hotKeyBindEditor.EnableWindowsKey = modifiers.HasFlag(HotKeyModifiers.WinKey);
        }

        private static void EnableControlPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            SetModifiersFlag(dependencyObject, e.NewValue, HotKeyModifiers.ControlKey);
        }

        private static void EnableShiftPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            SetModifiersFlag(dependencyObject, e.NewValue, HotKeyModifiers.ShiftKey);
        }

        private static void EnableAltPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            SetModifiersFlag(dependencyObject, e.NewValue, HotKeyModifiers.AltKey);
        }

        private static void EnableWindowsKeyPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            SetModifiersFlag(dependencyObject, e.NewValue, HotKeyModifiers.WinKey);
        }

        private static void SetModifiersFlag(DependencyObject dependencyObject, object newValue, HotKeyModifiers modifier)
        {
            Debug.Assert(dependencyObject is HotKeyBindEditor);
            Debug.Assert(newValue is bool);

            HotKeyBindEditor hotKeyBindEditor = (HotKeyBindEditor)dependencyObject;

            if ((bool)newValue)
            {
                hotKeyBindEditor.Value.Modifiers |= modifier;
            }
            else
            {
                hotKeyBindEditor.Value.Modifiers &= ~modifier;
            }
        }
    }
}