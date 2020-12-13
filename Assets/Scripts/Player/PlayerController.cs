using System;
using Core;
using GameStates;
using UI;
using Unit;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Player {
    [RequireComponent(typeof(Health), typeof(Attack), typeof(NavMeshAgent))]
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : BaseUnit, IAction, IResurrect {
        public static bool HasClickedOnPortal { get; set; }
        public LayerMask layerMask;

        
        Animator _animator => GetComponent<Animator>();

        void Update() {
            if (!BaseNavMeshAgent.hasPath && !BaseHealth.IsDead) {
                PlayAnimation("Idle");
            }

            ShouldMovetoMouse();
        }

        public override void OnDeath() {
            base.OnDeath();
            _animator.ResetTrigger("Idle");
            _animator.ResetTrigger("Running");
            PlayAnimation("Death");
            BaseNavMeshAgent.isStopped = true;
            StateLogic.OnDeath();
        }

        public void ActionToStart() {
            BaseNavMeshAgent.isStopped = true;
            PlayAnimation("Idle");
        }


        void ShouldMovetoMouse() {
            if (Input.GetMouseButton(0) && Physics.Raycast(GetMouseRay(), out var hit, 10000f, ~layerMask) && !BaseHealth.IsDead) {
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
            PlayAnimation("Running");
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


        void PlayAnimation(string animationToPlay) {
            _animator.SetTrigger(animationToPlay);
        }


        public void OnResurrect(bool onCorpse) {

            
            gameObject.layer = LayerMask.NameToLayer("Player");
            BaseHealth.CurrentHealth = MaxHealth();
            BaseNavMeshAgent.isStopped = false;
            GetComponent<AudioSource>().Stop();
            if (onCorpse || Checkpoint.CheckpointTransform == null) return;

            BaseNavMeshAgent.Warp(Checkpoint.CheckpointTransform.position);
        }
    }
}