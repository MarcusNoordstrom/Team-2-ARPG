using GameStates;
using Unit;
using UnityEngine;
using UnityEngine.AI;

namespace Player {
    [RequireComponent(typeof(PlayerHealth), typeof(NavMeshAgent), typeof(Rigidbody))]
    public class PlayerController : BaseUnit, IAction, IResurrect {
        public LayerMask layerMask;


        protected override bool EligibleToAttack => true;

        protected override void Update() {
            if (!BaseNavMeshAgent.hasPath && !BaseHealth.IsDead) {
                PlayAnimation("Idle");
            }

            if (InteractWithCombat()) return;

            ShouldMovetoMouse();
        }

        protected override GameObject CombatTarget {
            get => target;
            set => target = value;
        }

        bool InteractWithCombat() {
            if (Input.GetKeyDown(KeyCode.Mouse1) ||Input.GetKeyUp(KeyCode.Mouse1)) {
                var hits = Physics.RaycastAll(PlayerHelper.GetMouseRay());
                foreach (var raycastHit in hits) {
                    var target = raycastHit.transform.GetComponent<Health>();
                    if (target == null) continue;
                    CombatTarget = target.gameObject;
                    baseEquippedWeapon.ChangeWeapon(basicUnit.meleeWeapon);
                    GetComponent<Action>().StartAction(GetComponent<MeleeAttack>());
                    return true;
                }
            }

            if (Input.GetKeyDown(KeyCode.Mouse0) ||Input.GetKeyUp(KeyCode.Mouse0)) {
                var hits = Physics.RaycastAll(PlayerHelper.GetMouseRay());
                foreach (var raycastHit in hits) {
                    var target = raycastHit.transform.GetComponent<Health>();
                    if (target == null) continue;
                    CombatTarget = target.gameObject;
                    baseEquippedWeapon.ChangeWeapon(basicUnit.rangedWeapon);
                    GetComponent<Action>().StartAction(GetComponent<RangedAttack>());
                    return true;
                }
            }

            return false;
        }

        public override void OnDeath() {
            GetComponent<Collider>().enabled = false;
            animator.ResetTrigger("Idle");
            animator.ResetTrigger("Running");
            PlayAnimation("Death");
            BaseNavMeshAgent.isStopped = true;
            StateLogic.OnDeath();
        }

        public void ActionToStart() {
            BaseNavMeshAgent.ResetPath();
            BaseNavMeshAgent.isStopped = false;
            CombatTarget = null;
            PlayAnimation("Idle");
        }

        void ShouldMovetoMouse() {
            if (Input.GetMouseButton(0) && Physics.Raycast(PlayerHelper.GetMouseRay(), out var hit, 10000f, ~layerMask) && !BaseHealth.IsDead) {
                GetComponent<Action>().StartAction(this);
                //print(hit.collider.gameObject.name);
                Movement(hit.point);
                PlayerHelper.ClickedPortal(hit);
            }
        }

        void Movement(Vector3 destination) {
            //BaseNavMeshAgent.isStopped = false;
            BaseNavMeshAgent.destination = destination;
            if (BaseHealth.IsDead) return;
            PlayAnimation("Running");
        }

        void PlayAnimation(string animationToPlay) {
            animator.SetTrigger(animationToPlay);
        }


        public void OnResurrect(bool onCorpse) {
            GetComponent<Collider>().enabled = true;
            BaseHealth.CurrentHealth = MaxHealth();
            BaseNavMeshAgent.isStopped = false;
            GetComponent<AudioSource>().Stop();
            if (onCorpse || Checkpoint.CheckpointTransform == null) return;

            BaseNavMeshAgent.Warp(Checkpoint.CheckpointTransform.position);
        }
    }
}