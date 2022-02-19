using Assets.Scripts.Services.Raycast;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InputManager : MonoBehaviour
{
   [Inject]
   private readonly IRaycastService raycastService;

   void Start()
   {
   }

   void Update()
   {
      if(Input.GetMouseButtonDown(0)) {
         var clickedObject = raycastService.GetClickedGameObject(Input.mousePosition, Camera.main);

         var worldPoint = raycastService.GetWorldPoint(Input.mousePosition, Camera.main);

         Debug.Log($"{worldPoint} - {clickedObject.name}");
      }
   }
}
