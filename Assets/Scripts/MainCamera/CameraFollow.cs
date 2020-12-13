using UnityEngine;

namespace MainCamera {
    public class CameraFollow : MonoBehaviour {
        [SerializeField] Camera uiCamera;
        Transform player;

        void Start() {
            player = FindObjectOfType<Player.PlayerController>().transform;
            uiCamera.transform.position = Camera.main.transform.position;
            uiCamera.transform.rotation = Camera.main.transform.rotation;
        }

        void LateUpdate() {
            transform.position = player.position;
        }
    }
}