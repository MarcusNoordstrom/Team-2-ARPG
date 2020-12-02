using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour {
    private enum State {
        Roaming,
        ChaseTarget,
    }

    private Vector3 startingPosition;
    private Vector3 roamPosition;
    private NavMeshAgent pathfindingMovement;
    public Transform target;
    private State state;


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

    private void Update() {
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


                break;
        }
    }

    private Vector3 GetRoamingPosition() {
        return startingPosition + GetRandomDir() * Random.Range(3f, 10f);
    }

    private void FindTarget() {
        float targetRange = 7f;
        if (Vector3.Distance(transform.position, target.position) < targetRange) {
            state = State.ChaseTarget;
        }
    }
}