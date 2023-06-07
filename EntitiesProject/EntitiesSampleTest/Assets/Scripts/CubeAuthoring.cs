using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Cube
{
    public class CubeAuthoring : MonoBehaviour
    {
        public float DecreePerSecond = 90f;
        public float movementSpeed = 0.2f;
        class CubeBaker : Baker<CubeAuthoring>
        {
            public override void Bake(CubeAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new Cube
                {
                    radPerSecond = math.radians(authoring.DecreePerSecond),
                    moveSpeed = authoring.movementSpeed,
                    moveDirection = 1
                });
            }
        }

    }

    public struct Cube : IComponentData
    {
        public float radPerSecond;
        public float moveSpeed;
        public int moveDirection;
    }


}
