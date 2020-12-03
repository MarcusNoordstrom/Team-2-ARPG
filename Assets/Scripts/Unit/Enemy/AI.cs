using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Unit.Enemy {
    public class AI : MonoBehaviour {
        private enum State {
            Roaming,
            ChaseTarget,
            GoingBackToStart,
        }

        private Vector3 startingPosition;
        private Vector3 roamPosition;
        private NavMeshAgent pathfindingMovement;
        public Transform target;
        private State state;
        private float targetRange;


        private void Awake() {
            pathfindingMovement = GetComponent<NavMeshAgent>();
            state = State.Roaming;
        }

        private void Start() {
            startingPosition = transform.position;
            roamPosition = GetRoamingPosition();
            // pathfindingMovement.SetDestination(target.position);
        }

        public static Vector3 GetRandomDir() {
            return new Vector3(Random.Range(-1f, 1f), 0, UnityEngine.Random.Range(-1f, 1f)).normalized;
        }

        private void FixedUpdate() {
            switch (state) {
                default:
                case State.Roaming:

                    pathfindingMovement.SetDestination(roamPosition);
                    var reachedPositionDistance = 1f;
                    if (Vector3.Distance(transform.position, roamPosition) < reachedPositionDistance) {
                        roamPosition = GetRoamingPosition();
                    }

                    FindTarget();
                    break;
                case State.ChaseTarget:

                    pathfindingMovement.SetDestination(target.position);
                    var attackRange = 5f;
                    if (Vector3.Distance(transform.position, target.position) < attackRange) {
                        pathfindingMovement.isStopped = true;
                    } else pathfindingMovement.isStopped = false;

                    float stopChaseDistance = 15f;
                    if (Vector3.Distance(transform.position, target.position) > stopChaseDistance) {
                        state = State.GoingBackToStart;
                    }

                    break;
                case State.GoingBackToStart:
                    pathfindingMovement.SetDestination(startingPosition);
                    reachedPositionDistance = 1f;
                    if (Vector3.Distance(transform.position, startingPosition) < reachedPositionDistance) {
                        state = State.Roaming;
                    }

                    break;
            }
        }

        private Vector3 GetRoamingPosition() {
            return startingPosition + GetRandomDir() * Random.Range(3f, 10f);
        }

        private void FindTarget() {
            targetRange = 7f;
            if (Vector3.Distance(transform.position, target.position) < targetRange) {
                state = State.ChaseTarget;
            }
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(this.transform.position, targetRange);
        }
    }
}