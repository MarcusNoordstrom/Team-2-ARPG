using System;
using Core;
using GameStates;
using Unit;
using UnityEngine;
using UnityEngine.AI;

namespace Player {
    [RequireComponent(typeof(Health), typeof(Attack), typeof(NavMeshAgent))]
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : BaseUnit {
        public static bool HasClickedOnPortal { get; set; }

        void Update() {
            ShouldMovetoMouse();
        }

        void ShouldMovetoMouse() {
            var hasHit = Physics.Raycast(GetMouseRay(), out var hit);
            if (Input.GetMouseButton(0) && hit.collider != null)
                if (hasHit)
                    Movement(hit.point);
            ClickedPortal(hit);
        }

        void Movement(Vector3 destination) {
            BaseNavMeshAgent.destination = destination;
        }

        static Ray GetMouseRay() {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            return ray;
        }

        static void ClickedPortal(RaycastHit hit) {
            if (hit.collider == null) return;

            if (Input.GetMouseButtonUp(0) && hit.collider.GetComponent<Portal>() == null)
                HasClickedOnPortal = false;
            if (Input.GetMouseButtonDown(0) && hit.collider.GetComponent<Portal>() != null)
                HasClickedOnPortal = true;
        }

        public override void OnDeath() {
            base.OnDeath();
            BaseNavMeshAgent.isStopped = true;
        }

        public void ResurrectBase() {
            gameObject.layer = LayerMask.NameToLayer("Player");
            BaseHealth.CurrentHealth = MaxHealth();
            BaseNavMeshAgent.isStopped = false;
            BaseHealth.RevivePlayer();
            GetComponent<AudioSource>().Stop();
        }

        public void OnResurrectAtCheckpoint() {
            if (Checkpoint.CheckpointTransform == null) {
                return;
            }
            BaseNavMeshAgent.Warp(Checkpoint.CheckpointTransform.position);
        }
    }
}