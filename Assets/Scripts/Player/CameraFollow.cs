using System;
using UnityEngine;

namespace Player {
    public class CameraFollow : MonoBehaviour {
        Mover _player;

        void Start() {
            this._player = FindObjectOfType<Mover>();
        }

        void LateUpdate() {
            this.transform.position = this._player.transform.position;
        }
    }
}