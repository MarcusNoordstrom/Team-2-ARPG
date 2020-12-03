using UnityEngine;
using UnityEngine.AI;

namespace Player {
    public class Resurrection : MonoBehaviour {
        NavMeshAgent _navMeshAgent;
        public Transform checkPoint;
        
        void Start() {
            _navMeshAgent = FindObjectOfType<Mover>().GetComponent<NavMeshAgent>();
        }

        private void Update() {
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
