using FRT.Portal.Common.Collections;
using System.Collections.Generic;

namespace GM.Utility
{
    /// <summary>
    /// 表示一个枚举项的元数据
    /// </summary>
    public class EnumItem : IKeyedObject
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public string Description { get; set; }
        public object Object { get; set; }
        public int Sort { get; set; }
        public bool IsDefault { get; set; }
    }

    public class KeyedEnumItemCollection : KeyedObjectCollection<EnumItem>
    {
        private List<EnumItem> _defaults = new List<EnumItem>();
        private EnumItem _default = null;

        public List<EnumItem> Defaults
        {
            get
            {
                if (_defaults.Count == 0)
                {
                    _defaults.Add(this[0]);
                }

                return _defaults;
            }
        }

        public EnumItem Default
        {
            get
            {
                if (_default == null)
                {
                    _default = this[0];
                }

                return _default;
            }
        }

        protected override void InsertItem(int index, EnumItem item)
        {
            base.InsertItem(index, item);

            if (item.IsDefault)
            {
                if (_default == null)
                {
                    _default = item;
                }
                _defaults.Add(item);
            }
        }
    }

    public class ValuedEnumItemCollection : KeyedObjectCollection<EnumItem>
    {
        private List<EnumItem> _defaults = new List<EnumItem>();
        private EnumItem _default = null;

        protected override string GetKeyForItem(EnumItem item)
        {
            return item.Value.ToString();
        }

        public List<EnumItem> Defaults
        {
            get
            {
                if (_defaults.Count == 0)
                {
                    _defaults.Add(this[0]);
                }

                return _defaults;
            }
        }

        public EnumItem Default
        {
            get
            {
                if (_default == null)
                {
                    _default = this[0];
                }

                return _default;
            }
        }

        protected override void InsertItem(int index, EnumItem item)
        {
            base.InsertItem(index, item);

            if (item.IsDefault)
            {
                if (_default == null)
                {
                    _default = item;
                }
                _defaults.Add(item);
            }
        }
    }
}