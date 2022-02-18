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
      raycastService.GetClickedGameObject();
   }

   void Update()
   {

   }
}
