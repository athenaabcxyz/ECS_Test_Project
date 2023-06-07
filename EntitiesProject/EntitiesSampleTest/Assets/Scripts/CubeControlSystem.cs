using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Random = Unity.Mathematics.Random;

public partial class CubeControlSystem : SystemBase
{
    PlayerInputController controller;
    [BurstCompile]
    protected override void OnCreate()
    {
        controller=new PlayerInputController();
        controller.Player.Enable();
    }

    [BurstCompile]
    protected override void OnUpdate()
    {
        float deltaTime = SystemAPI.Time.DeltaTime;

        float hAxis = controller.Player.Move.ReadValue<Vector2>().x;
        float vAxis = controller.Player.Move.ReadValue<Vector2>().y;

        Random randomValue = new(1);
        foreach (var (transform, controller) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<PlayerController>>())
        {
            transform.ValueRW.Position.z += hAxis*controller.ValueRO.moveSpeed*deltaTime;
            transform.ValueRW.Position.x += vAxis*controller.ValueRO.moveSpeed*deltaTime;
        }
    }
}