using Core;
using UnityEngine;
using UnityEngine.AI;
using Unit;
using UnityEngine.EventSystems;

namespace Player {
    [RequireComponent(typeof(Health), typeof(Attack), typeof(NavMeshAgent))]
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour {
        NavMeshAgent _navMeshAgent;
        [SerializeField] BasicPlayer basicPlayer;
        Attack _attack;

        void Awake() {
            this._attack = GetComponent<Attack>();
            this._navMeshAgent = GetComponent<NavMeshAgent>();
            SetupPlayer();
            this._attack.ChangeWeapon(this.basicPlayer.mainWeapon);
        }

        void SetupPlayer() {
            GetComponent<Health>().MaxHealth = this.basicPlayer.maxHealth;
            this._navMeshAgent.speed = this.basicPlayer.moveSpeed;
        }

        void Update() {
            ShouldMovetoMouse();
        }

        public static bool HasClickedOnPortal { get; set; }

        void ShouldMovetoMouse() {
            var hasHit = Physics.Raycast(GetMouseRay(), out var hit);
            if (Input.GetMouseButton(0) && hit.collider != null) {
                if (hasHit) {
                    Movement(hit.point);
                }
            }

            ClickedPortal(hit);
        }

        void Movement(Vector3 destination) {
            this._navMeshAgent.destination = destination;
        }

        static Ray GetMouseRay() {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            return ray;
        }

        static void ClickedPortal(RaycastHit hit) {
            if (hit.collider == null) {
                return;
            }

            if (Input.GetMouseButtonUp(0) && hit.collider.GetComponent<Portal>() == null)
                HasClickedOnPortal = false;
            if (Input.GetMouseButtonDown(0) && hit.collider.GetComponent<Portal>() != null)
                HasClickedOnPortal = true;
        }
    }
}