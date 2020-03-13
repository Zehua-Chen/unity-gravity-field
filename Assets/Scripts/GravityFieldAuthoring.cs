using UnityEngine;
using Unity.Entities;

[RequireComponent(typeof(Rigidbody2D))]
public class GravityFieldAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    [SerializeField]
    public float _range = 100.0f;

    public void Convert(
        Entity entity,
        EntityManager dstManager,
        GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new GravityField
        {
            Range = _range
        });
    }
}
