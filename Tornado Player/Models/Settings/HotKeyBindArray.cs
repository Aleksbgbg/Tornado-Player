namespace Tornado.Player.Models.Settings
{
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    using Tornado.Player.Utilities.PropertyGrid;

    internal class HotKeyBindArray : CustomTypeDescriptor, IEnumerable<HotKeyBind>
    {
        private readonly HotKeyBind[] _hotKeyBinds;

        public HotKeyBindArray(HotKeyBind[] hotKeyBinds)
        {
            _hotKeyBinds = hotKeyBinds;
        }

        public static implicit operator HotKeyBind[] (HotKeyBindArray hotKeyBindArray)
        {
            return hotKeyBindArray._hotKeyBinds;
        }

        public override object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }

        public override PropertyDescriptorCollection GetProperties()
        {
            PropertyDescriptor[] properties = this.Select(hotKeyBind => new HotKeyBindPropertyDescriptor(hotKeyBind))
                                                  .Cast<PropertyDescriptor>()
                                                  .ToArray();

            return new PropertyDescriptorCollection(properties);
        }

        public IEnumerator<HotKeyBind> GetEnumerator()
        {
            return ((IEnumerable<HotKeyBind>)_hotKeyBinds).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _hotKeyBinds.GetEnumerator();
        }
    }
}