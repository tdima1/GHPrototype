﻿using Assets.Scripts.Services.Movement;
using Assets.Scripts.Services.Raycast;
using UnityEngine;
using Zenject;

public class InputManager : MonoBehaviour
{
   [Inject] private readonly IRaycastService raycastService;
   [Inject] private readonly IMovementService movementService;

   [SerializeField] private Transform playerPrefab;

   void Start()
   {
   }

   void Update()
   {
      if(Input.GetMouseButtonDown(0)) {

         var hitData = raycastService.GetWorldGroundPoint(Input.mousePosition, Camera.main);

         if(hitData.ObjectHit) {
            movementService.MoveUnit(playerPrefab, hitData.WorldPosition);
         }

         Debug.Log($"{hitData}");
      }
   }
}
