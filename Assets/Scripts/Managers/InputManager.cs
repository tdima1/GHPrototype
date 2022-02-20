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
      if(Input.GetMouseButtonDown(0)) {
         var clickedObject = raycastService.GetClickedGameObject(Input.mousePosition, Camera.main);

         var worldPoint = raycastService.GetWorldPoint(Input.mousePosition, Camera.main);

         movementService.MoveUnit(playerPrefab, worldPoint);

         Debug.Log($"{worldPoint} - {clickedObject.name}");
      }
   }
}
