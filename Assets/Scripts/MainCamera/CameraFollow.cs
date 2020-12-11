using UnityEngine;

namespace MainCamera {
    public class CameraFollow : MonoBehaviour {
        Transform player;

        void Start() {
            player = FindObjectOfType<Player.PlayerController>().transform;
        }

        void LateUpdate(){
            transform.position = player.position;
        }
    }
}