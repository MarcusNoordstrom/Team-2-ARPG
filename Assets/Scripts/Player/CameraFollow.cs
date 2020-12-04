using System;
using UnityEngine;

namespace Player {
    public class CameraFollow : MonoBehaviour {
        PlayerController _player;

        void Start() {
            this._player = FindObjectOfType<PlayerController>();
        }

        void Update() {
            this.transform.position = this._player.transform.position;
        }
    }
}