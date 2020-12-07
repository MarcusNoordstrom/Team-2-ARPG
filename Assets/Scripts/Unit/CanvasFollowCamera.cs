using UnityEngine;

namespace Unit {
    public class CanvasFollowCamera : MonoBehaviour {
        Transform cameraTransform;

        void Awake() {
            cameraTransform = Camera.main.transform;
            transform.rotation = cameraTransform.rotation;
        }

        void LateUpdate() {
            transform.rotation = cameraTransform.rotation;
        }
    }
}