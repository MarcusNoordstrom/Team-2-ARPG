﻿using UnityEngine;

namespace Unit{
    public class CanvasFollowCamera : MonoBehaviour{
        private Transform cameraTransform;

        private void Awake(){
            this.cameraTransform = Camera.main.transform;
            transform.rotation = this.cameraTransform.rotation;
        }

        private void FixedUpdate(){
            transform.rotation = this.cameraTransform.rotation;
        }
    }
}