﻿using Core;
using GameStates;
using Unit;
using UnityEngine;
using UnityEngine.AI;

namespace Player {
    [RequireComponent(typeof(Health), typeof(Attack), typeof(NavMeshAgent))]
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : BaseUnit, IAction {
        public static bool HasClickedOnPortal { get; set; }
        public LayerMask layerMask;
        Animator _animator => GetComponent<Animator>();

        void Update() {
            if (!BaseNavMeshAgent.hasPath && !BaseHealth.IsDead) {
                PlayIdleAnimation();
            }

            ShouldMovetoMouse();
        }

        void ShouldMovetoMouse(){
            if (Input.GetMouseButton(0) && Physics.Raycast(GetMouseRay(), out var hit, 10000f, ~layerMask)){
                GetComponent<Action>().StartAction(this);
                //print(hit.collider.gameObject.name);
                Movement(hit.point);
                ClickedPortal(hit);
            }
        }

        void Movement(Vector3 destination) {
            BaseNavMeshAgent.isStopped = false;
            BaseNavMeshAgent.destination = destination;
            if (BaseHealth.IsDead) return;
            PlayRunningAnimation();
        }

        public static Ray GetMouseRay() {
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
            PlayDeathAnimation();
            BaseNavMeshAgent.isStopped = true;
            StateLogic.OnDeath();
        }

        void PlayDeathAnimation() {
            _animator.SetTrigger("Death");
            _animator.ResetTrigger("Idle");
            _animator.ResetTrigger("Running");
        }

        void PlayRunningAnimation() {
            _animator.SetTrigger("Running");
            _animator.ResetTrigger("Idle");
        }

        void PlayIdleAnimation() {
            _animator.SetTrigger("Idle");
            _animator.ResetTrigger("Running");
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

        public void ActionToStart() {
            BaseNavMeshAgent.isStopped = true;
            PlayIdleAnimation();
        }
    }
}