using UnityEngine;
using UnityEngine.AI;

namespace Player {
    public class Resurrection : MonoBehaviour {
        public Transform checkPoint;
        NavMeshAgent _navMeshAgent;

        void Start() {
            _navMeshAgent = FindObjectOfType<PlayerController>().GetComponent<NavMeshAgent>();
        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.K)) {
                DisableComponent(false);
                _navMeshAgent.isStopped = true;
            }
        }

        public void ResurrectAtCorpse() {
            _navMeshAgent.Warp(_navMeshAgent.transform.position * 2f);
            DisableComponent(true);
            _navMeshAgent.isStopped = false;
        }

        public void ResurrectAtCheckpoint() {
            _navMeshAgent.Warp(checkPoint.position);
            DisableComponent(true);
            _navMeshAgent.isStopped = false;
        }

        void DisableComponent(bool disable) {
            var _canvasgroup = GetComponent<CanvasGroup>();
            if (disable) {
                _canvasgroup.alpha = 0;
                _canvasgroup.interactable = false;
                _canvasgroup.blocksRaycasts = false;
            }
            else {
                _canvasgroup.alpha = 1;
                _canvasgroup.interactable = true;
                _canvasgroup.blocksRaycasts = true;
            }
        }
    }
}