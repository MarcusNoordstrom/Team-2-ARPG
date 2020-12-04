using Player;
using UnityEngine;
using UnityEngine.AI;

namespace Unit.Enemy{
    [RequireComponent(typeof(VisibilityCheck),typeof(LookAtTarget),typeof(Attack))]
    public class AI : MonoBehaviour{
        //Rename class to EnemyController?
        private enum State{
            Roaming,
            ChaseTarget,
            GoingBackToStart
        }
        private const int TicksPerUpdate = 15;
        [SerializeField] private State state;
        [SerializeField] private float targetRange = 10;//Part of the EnemySO
        [SerializeField] private float stopChaseDistance = 15f;//Part of the EnemySO
        [SerializeField] private float attackRange = 10f;//Part of weaponSO
        private Attack attack;
        private float distance;
        private LookAtTarget lookAtTarget;
        private NavMeshAgent pathfindingMovement;
        private Vector3 roamPosition;
        private Vector3 startingPosition;
        private Mover target;
        private int ticks;
        private VisibilityCheck visibilityCheck;
        private void Awake(){
            this.pathfindingMovement = GetComponent<NavMeshAgent>();
            this.state = State.Roaming;
            this.lookAtTarget = GetComponent<LookAtTarget>();
            this.attack = GetComponent<Attack>();
            this.visibilityCheck = GetComponent<VisibilityCheck>();
        }

        private void Start(){
            this.target = FindObjectOfType<Mover>();
            this.lookAtTarget.Setup(this.target.transform);
            this.startingPosition = transform.position;
            this.roamPosition = GetRoamingPosition();
            this.ticks = Random.Range(0, TicksPerUpdate);
        }

        private void FixedUpdate(){
            this.ticks++;
            if (this.ticks < TicksPerUpdate)
                return;
            this.ticks -= TicksPerUpdate;

            switch (this.state){
                case State.Roaming:
                    this.pathfindingMovement.SetDestination(this.roamPosition);
                    if (Vector3.Distance(transform.position, this.roamPosition) < 1f)
                        this.roamPosition = GetRoamingPosition();
                    FindTarget();
                    break;
                case State.ChaseTarget:
                    if (!this.visibilityCheck.IsVisible(this.target.gameObject)){
                        this.lookAtTarget.enabled = false;
                        this.pathfindingMovement.isStopped = false;
                        this.state = State.GoingBackToStart;
                        break;
                    }

                    this.pathfindingMovement.SetDestination(this.target.transform.position);
                    if (Vector3.Distance(transform.position, this.target.transform.position) < this.attackRange){
                        this.pathfindingMovement.isStopped = true;
                        this.lookAtTarget.enabled = true;
                        if (Vector3.Angle(transform.forward,(this.target.transform.position - transform.position).normalized) < 50) 
                            this.attack.Range(this.target.gameObject);
                    }
                    else{
                        this.pathfindingMovement.isStopped = false;
                    }

                    if (Vector3.Distance(transform.position, this.target.transform.position) > this.stopChaseDistance){
                        this.lookAtTarget.enabled = false;
                        this.state = State.GoingBackToStart;
                    }

                    break;
                case State.GoingBackToStart:
                    this.pathfindingMovement.SetDestination(this.startingPosition);
                    if (Vector3.Distance(transform.position, this.startingPosition) < 1f){
                        this.state = State.Roaming;
                        break;
                    }

                    FindTarget(); 
                    break;
            }
        }
        private void OnDrawGizmosSelected(){
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, this.targetRange);
        }
        private static Vector3 GetRandomDir(){
            return new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        }
        private Vector3 GetRoamingPosition(){
            return this.startingPosition + GetRandomDir() * Random.Range(3f, 10f);
        }
        private void FindTarget(){
            if (Vector3.Distance(transform.position, this.target.transform.position) < this.targetRange)
                if (this.visibilityCheck.IsVisible(this.target.gameObject)){
                    this.roamPosition = GetRoamingPosition();
                    this.state = State.ChaseTarget;
                }
        }
    }
}