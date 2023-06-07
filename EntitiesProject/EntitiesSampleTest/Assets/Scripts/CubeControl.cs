using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class CubeControl : MonoBehaviour
{
    public float characterMoveSpeed = 1f;
    public GameObject characterView;
    class CubeControlBaker : Baker<CubeControl>
    {

        public override void Bake(CubeControl authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new PlayerController
            {
                moveSpeed = authoring.characterMoveSpeed
            });
        }
    }

}

public struct PlayerController : IComponentData
{
    public float moveSpeed;
}
