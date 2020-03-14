using UnityEngine;

namespace GravityField
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class GravityField : MonoBehaviour
    {
        static GravityFields _fields = new GravityFields();
        static public GravityFields Fields => _fields;

        Rigidbody2D _rig = null;

        public float Range = 100.0f;
        public float G = 6.67f;

        /// <summary>
        /// A hash like index that we use to store this gravity field.
        /// We don't use HashSet to avoid using foreach in update loop
        /// </summary>
        [HideInInspector]
        public int Index = -1;

        private void Awake()
        {
            _rig = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            _fields.Add(this, _rig);
        }

        private void OnDisable()
        {
            _fields.Remove(this);
        }

        private void Update()
        {
            float mass = _rig.mass;

            for (int i = 0; i < _fields.Count; i++)
            {
                GravityField otherField;
                Rigidbody2D otherRig;

                _fields.Get(i, out otherField, out otherRig);

                if (otherField == this)
                {
                    continue;
                }

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
