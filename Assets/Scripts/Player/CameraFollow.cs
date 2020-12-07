using UnityEngine;

namespace Player {
    public class CameraFollow : MonoBehaviour {
        PlayerController _player;

        void Start() {
            _player = FindObjectOfType<PlayerController>();
        }

        void Update() {
            transform.position = _player.transform.position;
        }
    }
}