using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace ConfigSpawing
{
    public class ConfigAuthoring : MonoBehaviour
    {
        public GameObject CubePrefab;
        public int quantity;
        public float safeZoneRadius;

        class ConfigBaker : Baker<ConfigAuthoring>
        {
            public override void Bake(ConfigAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new Config
                {
                    CubePrefab = GetEntity(authoring.CubePrefab, TransformUsageFlags.Dynamic),
                    quantity = authoring.quantity,
                    SafeZoneRadius = authoring.safeZoneRadius,
                });
            }
        }

      
    }
    public struct Config : IComponentData
    {
        public Entity CubePrefab;
        public int quantity;
        public float SafeZoneRadius;
    }
}
