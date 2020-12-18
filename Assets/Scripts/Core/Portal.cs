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
        [SerializeField] VoidEvent startTeleportEvent;

        void OnEnable() {
            toolTipText.enabled = false;
            toolTipText.text = toolTip;
        }

        void OnMouseExit() {
            toolTipText.enabled = false;
        }

        void OnMouseOver() {
            toolTipText.enabled = true;
        }

        void OnTriggerStay(Collider other) {
            if (!PlayerHelper.HasClickedOnPortal) return;
            if (1 << other.gameObject.layer != LayerMask.GetMask("Player")) return;
            StartCoroutine(Transition());
            PlayerHelper.HasClickedOnPortal = false;
        }

        IEnumerator Transition() {
            startTeleportEvent?.Invoke();
            DontDestroyOnLoad(gameObject);
            yield return Fader.FadeIn();
            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            UpdatePlayerPosition(GetPortal());
            //Todo Game Designers wants a loading screen, how long should the waiting time be? Also needs to stop movement during the wait period
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
                print(portal);
                return portal;
            }

            return null;
        }
    }
}