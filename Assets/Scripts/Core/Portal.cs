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
        public Text toolTipText;
        public string toolTip;

        void OnEnable() {
            toolTipText.text = toolTip;
        }

        void OnMouseExit() {
            toolTipText.enabled = false;
        }

        void OnMouseOver() {
            toolTipText.enabled = true;
            var yAxis = GetComponent<BoxCollider>().bounds.size.y;
            toolTipText.transform.position =
                new Vector3(transform.position.x, transform.position.y + yAxis, transform.position.z);
        }

        void OnTriggerStay(Collider other) {
            if (!PlayerHelper.HasClickedOnPortal) return;
            if (1 << other.gameObject.layer != LayerMask.GetMask("Player")) return;
            StartCoroutine(Transition());
            PlayerHelper.HasClickedOnPortal = false;
        }

        IEnumerator Transition() {
            DontDestroyOnLoad(gameObject);
            var fader = FindObjectOfType<Fader>();
            yield return Fader.FadeIn();
            yield return SceneManager.LoadSceneAsync(FindObjectOfType<Portal>().sceneToLoad);
            UpdatePlayerPosition(GetPortal());
            //Todo Game Designers wants a loading screen, how long should the waiting time be? Also needs to stop movement during the wait period
            //yield return new WaitForSeconds(5f);
            yield return Fader.FadeOut();
            Destroy(gameObject);
        }

        void UpdatePlayerPosition(Portal portal) {
            var player = FindObjectOfType<PlayerController>().GetComponent<NavMeshAgent>();
            player.Warp(portal.transform.GetChild(0).transform.position);
        }

        Portal GetPortal() {
            foreach (var portal in FindObjectsOfType<Portal>()) {
                if (portal == this) continue;
                if (portal.spawnPoint != spawnPoint) continue;
                return portal;
            }

            return null;
        }
    }
}