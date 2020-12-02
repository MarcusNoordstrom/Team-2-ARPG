using UnityEngine;

namespace Unit{
    public class CanvasFollowCamera : MonoBehaviour{
        private Transform _cameraTransform;
        private void Awake(){
            _cameraTransform = Camera.main.transform;
            transform.rotation = _cameraTransform.rotation;
        }
        private void FixedUpdate(){
            transform.rotation = _cameraTransform.rotation;
        }
    }
}

