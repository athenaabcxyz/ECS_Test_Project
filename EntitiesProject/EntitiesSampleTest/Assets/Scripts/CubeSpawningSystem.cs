using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.UIElements;
using Random = Unity.Mathematics.Random;

namespace ConfigSpawing
{
    public partial struct CubeSpawningSystem : ISystem
    {

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            
            state.RequireForUpdate<Config>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            Random randomValue = new(1);
            state.Enabled = false;
            var config = SystemAPI.GetSingleton<Config>();

            LocalTransform transformRed = LocalTransform.FromPosition(new float3(0, 0, 0));
            LocalTransform transformBlue = LocalTransform.FromPosition(new float3(100, 0, 0));


            var ecb = new EntityCommandBuffer(Allocator.Temp);
            var redCubes = new NativeArray<Entity>(config.quantity, Allocator.Temp);
            var blueCubes = new NativeArray<Entity>(config.quantity, Allocator.Temp);

            ecb.Instantiate(config.CubePrefab, redCubes);
            ecb.Instantiate(config.CubePrefab, blueCubes);


            foreach (var redCube in redCubes) 
            {
 
                transformRed.Position.z += randomValue.NextFloat(1, 4);
                transformRed.Position.y += randomValue.NextFloat(1, 4);
                ecb.SetComponent(redCube, transformRed);
                ecb.AddComponent(redCube, new CubeTag { tag = 1});
            }
            foreach (var blueCube in blueCubes) 
            {
                transformBlue.Position.z += randomValue.NextFloat(1, 4);
                transformBlue.Position.y += randomValue.NextFloat(1, 4);
                ecb.SetComponent(blueCube, transformBlue);
                ecb.AddComponent(blueCube, new CubeTag { tag = 0 }) ;
            }
            ecb.Playback(state.EntityManager);
        }
    }
}