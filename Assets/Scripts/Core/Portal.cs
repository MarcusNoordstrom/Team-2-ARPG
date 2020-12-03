using System;
using System.Collections;
using Player;
using UI;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Core {
    public class Portal : MonoBehaviour {
        public string sceneToLoad;
        public SpawnPoint spawnPoint;
        Fader _fader;

        public Text toolTipText;
        public string toolTip;

        void Start() {
            this._fader = FindObjectOfType<Fader>();
        }

        void OnEnable() {
            this.toolTipText.text = this.toolTip;
        }

        void OnTriggerEnter(Collider other) {
            if (!Mover.HasClickedOnPortal && (1 << other.gameObject.layer) != LayerMask.GetMask("Player")) return;
            StartCoroutine(Transition());
        }

        IEnumerator Transition() {
            DontDestroyOnLoad(this.gameObject);
            yield return this._fader.FadeIn();
            yield return SceneManager.LoadSceneAsync(FindObjectOfType<Portal>().sceneToLoad);
            UpdatePlayerPosition(GetPortal());
            //Todo Game Designers wants a loading screen, how long should the waiting time be? Also needs to stop movement during the wait period
            //yield return new WaitForSeconds(5f);
            yield return this._fader.FadeOut();
            Destroy(this.gameObject);
        }

        void UpdatePlayerPosition(Portal portal) {
            var player = FindObjectOfType<Mover>().GetComponent<NavMeshAgent>();
            player.Warp(portal.transform.GetChild(0).transform.position);
        }

        Portal GetPortal() {
            foreach (var portal in FindObjectsOfType<Portal>()) {
                if (portal == this) continue;
                if (portal.spawnPoint != this.spawnPoint) continue;
                return portal;
            }

            return null;
        }

        void OnMouseOver() {
            this.toolTipText.enabled = true;
            var yAxis = GetComponent<BoxCollider>().bounds.size.y;
            this.toolTipText.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + yAxis, this.transform.position.z);
        }

        void OnMouseExit() {
            this.toolTipText.enabled = false;
        }
    }
}