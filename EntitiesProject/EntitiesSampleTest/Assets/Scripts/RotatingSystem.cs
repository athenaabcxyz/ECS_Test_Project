using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEditor.SceneManagement;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace Cube.MainThread
{
    public partial struct RotatingSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {

        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            Random randomValue=new(1);
            foreach (var (transform, speed, tag, color) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<Cube>, RefRO<CubeTag>, RefRW<URPMaterialPropertyBaseColor>>())
            {
                float3 moveDirection;
                
                float4 redColor = new float4(255, 0, 0, 255);
                float4 blueColor = new float4(0, 200, 255, 255);
                moveDirection.x = randomValue.NextFloat(0f, 1f);
                moveDirection.y = randomValue.NextFloat(0f, 1f);
                moveDirection.z = randomValue.NextFloat(0f, 1f);

                if (tag.ValueRO.tag == 0)
                {
                    transform.ValueRW = transform.ValueRO.RotateY(speed.ValueRO.radPerSecond * deltaTime);
                    color.ValueRW.Value = blueColor;
                }
                else
                    color.ValueRW.Value = redColor;
                if (transform.ValueRO.Position.x <= 0)
                {
                    speed.ValueRW.moveDirection = 1;
                }
                else
                    if(transform.ValueRO.Position.x>=50)
                {
                    speed.ValueRW.moveDirection = -1;
                }
                   
                speed.ValueRW.moveSpeed = randomValue.NextFloat(0.05f, 0.5f);
                transform.ValueRW.Position += moveDirection * speed.ValueRO.moveSpeed*speed.ValueRO.moveDirection;
            }
        }
    }
}