using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GravityField
{
    /// <summary>
    /// Store a list of gravity fields and rigidbodies. We store rigidbodies
    /// as it is commonly accessed the same time as GravityFields
    /// </summary>
    public class GravityFields
    {
        struct Element
        {
            public GravityField Field;
            public Rigidbody2D Rigidbody;
        }

        List<Element> _data = new List<Element>();

        public int Count => _data.Count;

        public void Add(GravityField field, Rigidbody2D rigidbody)
        {
            field.Index = _data.Count;
            _data.Add(new Element
            {
                Field = field,
                Rigidbody = rigidbody
            });
        }

        public void Remove(GravityField field)
        {
            if (_data.Count == 0)
            {
                return;
            }

            Element last = _data.Last();

            if (last.Field == field)
            {
                _data.Clear();
                return;
            }

            last.Field.Index = field.Index;
            field.Index = -1;

            _data[last.Field.Index] = last;
            _data.RemoveAt(_data.Count - 1);
        }

        public void Get(int i, out GravityField field, out Rigidbody2D rigidbody)
        {
            Element element = _data[i];

            field = element.Field;
            rigidbody = element.Rigidbody;
        }
    }
}
