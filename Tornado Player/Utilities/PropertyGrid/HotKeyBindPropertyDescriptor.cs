namespace Tornado.Player.Utilities.PropertyGrid
{
    using System;
    using System.ComponentModel;
    using System.Text.RegularExpressions;

    using Tornado.Player.Controls;
    using Tornado.Player.Models.Settings;

    internal sealed class HotKeyBindPropertyDescriptor : PropertyDescriptor
    {
        private readonly HotKeyBind _hotKeyBind;

        public HotKeyBindPropertyDescriptor(HotKeyBind hotKeyBind) : base(Regex.Replace(hotKeyBind.Shortcut.ToString(), "([a-z])([A-Z])", "$1 $2"),
                                                                          new Attribute[]
                                                                          {
                                                                              new EditorAttribute(typeof(HotKeyBindEditor), typeof(HotKeyBindEditor))
                                                                          })
        {
            Description = $"Select a global shortcut combination to invoke the '{Name}' action.";
            _hotKeyBind = hotKeyBind;
        }

        public override string Description { get; }

        public override Type ComponentType => null;

        public override bool IsReadOnly => false;

        public override Type PropertyType => typeof(HotKeyBind);

        public override bool CanResetValue(object component)
        {
            return false;
        }

        public override object GetValue(object component)
        {
            return _hotKeyBind;
        }

        public override void ResetValue(object component)
        {
            throw new NotSupportedException($"{nameof(ResetValue)} is not supported for HotKeyBind");
        }

        public override void SetValue(object component, object value)
        {
            throw new NotSupportedException($"{nameof(SetValue)} is not supported for HotKeyBind");
        }

        public override bool ShouldSerializeValue(object component)
        {
            return true;
        }
    }
}