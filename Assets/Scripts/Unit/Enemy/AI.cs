using Player;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using System.Collections;

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
        private Mover target;
        private State state;
        private float targetRange;
        private int ticks;
        const int ticksPerUpdate = 15;


        private IEnumerator rotateTowardsTarget;
        private float distance;
        private void Awake() {
            pathfindingMovement = GetComponent<NavMeshAgent>();
            state = State.Roaming;
        }

        private void Start() {
            target = FindObjectOfType<Mover>();
            startingPosition = transform.position;
            roamPosition = GetRoamingPosition();
            ticks = Random.Range(0, ticksPerUpdate);
        }

        public static Vector3 GetRandomDir() {
            return new Vector3(Random.Range(-1f, 1f), 0, UnityEngine.Random.Range(-1f, 1f)).normalized;
        }


        private void FixedUpdate() {
            ticks++;
            if (this.ticks < ticksPerUpdate)
                return;
            this.ticks -= ticksPerUpdate;

            //this.distance = Vector3.Distance(this.transform.position, this.target.transform.position);
            
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
                    if (!GetComponent<VisibilityCheck>().IsVisible(target.gameObject)){
                        state = State.GoingBackToStart;
                        break;
                    }
                        
                    pathfindingMovement.SetDestination(target.transform.position);
                    var attackRange = 10f;
                    if (Vector3.Distance(transform.position, target.transform.position) < attackRange) {
                        pathfindingMovement.isStopped = true;
                        if (this.rotateTowardsTarget == null){
                            rotateTowardsTarget = RotateTowardTarget();
                        }
                        StopCoroutine(this.rotateTowardsTarget);
                        StartCoroutine(this.rotateTowardsTarget);
                        
                        GetComponent<Attack>().Range(this.target.gameObject);
                    }
                    else{
                        pathfindingMovement.isStopped = false;
                        
                        StopCoroutine(this.rotateTowardsTarget);
                    }

                    float stopChaseDistance = 15f;
                    if (Vector3.Distance(transform.position, target.transform.position) > stopChaseDistance) {
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

        private IEnumerator RotateTowardTarget(){
            while (true){
                yield return new WaitForFixedUpdate();
            Quaternion lookRotation = Quaternion.LookRotation((target.transform.position - transform.position).normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10f * Time.deltaTime);
            }
        }

        private Vector3 GetRoamingPosition() {
            return startingPosition + GetRandomDir() * Random.Range(3f, 10f);
        }

        private void FindTarget() {
            targetRange = 10f;
            if (Vector3.Distance(transform.position, target.transform.position) < targetRange) {
                if (GetComponent<VisibilityCheck>().IsVisible(target.gameObject))
                    state = State.ChaseTarget;
            }
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(this.transform.position, targetRange);
        }
    }
}