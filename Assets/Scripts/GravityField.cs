using System.Collections.Generic;
using UnityEngine;

namespace GravityField
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class GravityField : MonoBehaviour
    {
        static HashSet<GravityField> _fields = new HashSet<GravityField>();
        static public HashSet<GravityField> Fields => _fields;

        Rigidbody2D _rig = null;

        public float Range = 100.0f;
        public float G = 6.67f;

        private void Awake()
        {
            _rig = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            _fields.Add(this);
        }

        private void OnDisable()
        {
            _fields.Remove(this);
        }

        private void Update()
        {
            float mass = _rig.mass;

            foreach (GravityField otherField in _fields)
            {
                if (otherField == this)
                {
                    continue;
                }

                Rigidbody2D otherRig = otherField.GetComponent<Rigidbody2D>();

                float otherMass = otherRig.mass;

                Vector2 difference = _rig.position - otherRig.position;
                float distance = difference.magnitude;
                float gravity = this.Gravity(in mass, in otherMass, in distance);

                if (distance <= otherField.Range)
                {
                    otherRig.AddForce(difference.normalized * gravity);
                }
            }
        }

        private float Gravity(in float mA, in float mB, in float distance)
        {
            return ((mA * mB) / Mathf.Pow(distance, 2.0f)) * this.G;
        }
    }
}
