using UnityEngine;
using UnityEngine.PlayerLoop;
using Unity.Entities;
using Unity.Collections;

[UpdateInGroup(typeof(PhysicsSystemGroup))]
public class GravityFieldSystem : ComponentSystem
{
    EntityQuery _query;

    public float G { get; set; } = 6.67f;

    protected override void OnCreate()
    {
        _query = this.EntityManager.CreateEntityQuery(
            new ComponentType(typeof(GravityField)),
            new ComponentType(typeof(Rigidbody2D)));
    }

    protected override void OnUpdate()
    {
        using (NativeArray<Entity> entities = _query.ToEntityArray(Allocator.TempJob))
        {
            //for (int a = 0; a < entities.Length; a++)
            //{
            //    Entity entityA = entities[a];
            //    Rigidbody2D rigA = this.EntityManager.GetComponentObject<Rigidbody2D>(entityA);
            //    GravityField fieldA = this.EntityManager.GetComponentData<GravityField>(entityA);
            //    float massA = rigA.mass;

            //    for (int j = 0; j < entities.Length; j++)
            //    {
            //        Entity entityB = entities[a];
            //        Rigidbody2D rigB = this.EntityManager.GetComponentObject<Rigidbody2D>(entityB);
            //        GravityField fieldB = this.EntityManager.GetComponentData<GravityField>(entityB);
            //        float massB = rigB.mass;

            //        Vector2 difference = rigA.position - rigB.position;
            //        float magnitude = difference.magnitude;

            //        float gravity = this.Gravity(in massA, in massB, in magnitude);

            //        rigB.AddForce(difference.normalized * gravity);
            //        rigA.AddForce(difference.normalized * gravity * -1.0f);
            //    }
            //}
        }
    }

    public float Gravity(in float mA, in float mB, in float distance)
    {
        return ((mA * mB) / Mathf.Pow(distance, 2.0f)) * this.G;
    }
}
