using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
   [SerializeField] private float movementTime;
   [SerializeField] private float zoomAmount;
   [SerializeField] private float rotateAmount;
   [SerializeField] private float offsetFromTarget;

   [SerializeField] private Transform cameraTransform;
   [SerializeField] private Transform target;

   private Vector3 newZoom;
   private Quaternion newRotation;
   private Vector3 rotateStartPosition;
   private Vector3 rotateCurrentPosition;

   private void Awake()
   {
      newRotation = transform.rotation;
      newZoom = cameraTransform.localPosition;
   }

   private void LateUpdate()
   {
      HandleCameraMovement();
      HandleCameraRotation();
      newZoom += HandleCameraZoom();

      transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
      cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
   }

   private void HandleCameraRotation()
   {
      if(Input.GetMouseButtonDown(1)) {
         rotateStartPosition = Input.mousePosition;
      }
      if(Input.GetMouseButton(1)) {
         rotateCurrentPosition = Input.mousePosition;

         Vector3 difference = rotateStartPosition - rotateCurrentPosition;
         difference.y = difference.x;

         rotateStartPosition = rotateCurrentPosition;
         newRotation *= Quaternion.Euler(Vector3.Scale(Vector3.up, -difference * rotateAmount * Time.deltaTime));
      }
   }

   private Vector3 HandleCameraZoom()
   {
      Vector3 result = Vector3.zero;
      if(IsCursorInScreen()) {
         if(Input.mouseScrollDelta.y != 0) {
            result.y -= Input.mouseScrollDelta.y * zoomAmount;
         }
      }

      result = new Vector3(0, result.y, -result.y);

      return result;
   }

   private void HandleCameraMovement()
   {
      transform.position = new Vector3(target.position.x, target.position.y + offsetFromTarget, target.position.z);
   }

   private bool IsCursorInScreen()
   {
      return Input.mousePosition.x >= 0 &&
                  Input.mousePosition.y >= 0 &&
                  Input.mousePosition.x <= Screen.width &&
                  Input.mousePosition.y <= Screen.height;
   }
}
