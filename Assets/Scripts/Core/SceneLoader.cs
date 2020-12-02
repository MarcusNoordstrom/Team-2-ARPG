using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Core {
    public class SceneLoader : MonoBehaviour {
        public string sceneToLoad;

        public GameObject popup;

        void Start() {
            this.popup.SetActive(false);
        }

        void OnTriggerEnter(Collider other) {
            if ((1 << other.gameObject.layer) != LayerMask.GetMask("Player")) return;
            this.popup.SetActive(true);
        }

        void OnTriggerExit(Collider other) {
            //Todo add on exit shit
        }
    }
}