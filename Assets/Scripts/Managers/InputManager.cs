using Assets.Scripts.Services.Movement;
using Assets.Scripts.Services.Raycast;
using System.Collections;
using System.Collections.Generic;
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
      if(Input.GetMouseButton(0)) {

         var hitData = raycastService.GetWorldPoint(Input.mousePosition, Camera.main);

         if(hitData.ObjectHit) {
            movementService.MoveUnit(playerPrefab, hitData.WorldPosition);
         }

         Debug.Log($"{hitData}");
      }
   }
}
