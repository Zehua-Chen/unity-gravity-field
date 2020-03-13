using UnityEngine;
using Unity.Entities;

namespace GravityField
{
    public class PhysicsSystemsGroupManager : MonoBehaviour
    {
        PhysicsSystemGroup _group;

        private void Awake()
        {
            _group = World.All[0].GetExistingSystem<PhysicsSystemGroup>();

            // Unty does not have system groups for FixedUpdate yet, and we don't
            // want to simulations to run in regular Update()
            _group.Enabled = false;
        }

        private void FixedUpdate()
        {
            _group.Enabled = true;
            _group.Update();
            _group.Enabled = false;
        }
    }

}
