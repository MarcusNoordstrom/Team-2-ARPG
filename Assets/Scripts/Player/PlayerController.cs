using System.Collections;
using GameStates;
using UI;
using Unit;
using UnityEngine;
using UnityEngine.AI;

namespace Player {
    [RequireComponent(typeof(PlayerHealth), typeof(NavMeshAgent), typeof(Rigidbody))]
    public class PlayerController : BaseUnit, IAction, IResurrect {
        public LayerMask layerMask;


        protected override bool EligibleToAttack => true;

        bool ignoreRaycast;

        protected override void Update() {
            if (ignoreRaycast) return;
            
            if (!BaseNavMeshAgent.hasPath && !BaseHealth.IsDead) {
                PlayAnimation("Idle");
            }

            if (InteractWithCombat()) return;

            ShouldMovetoMouse();
        }

        bool InteractWithCombat() {
            if (Input.GetKeyUp(KeyCode.Mouse1)) {
                var hits = Physics.RaycastAll(PlayerHelper.GetMouseRay());
                foreach (var raycastHit in hits) {
                    var target = raycastHit.transform.GetComponent<Health>();
                    if (target == null) continue;
                    CombatTarget = target.gameObject;
                    equipped.ChangeWeapon(basicUnit.meleeWeapon);
                    GetComponent<Action>().StartAction(GetComponent<MeleeAttack>());
                    return true;
                }
            }

            if (Input.GetKeyUp(KeyCode.Mouse0)) {
                var hits = Physics.RaycastAll(PlayerHelper.GetMouseRay());
                foreach (var raycastHit in hits) {
                    var target = raycastHit.transform.GetComponent<Health>();
                    if (target == null) continue;
                    CombatTarget = target.gameObject;
                    equipped.ChangeWeapon(basicUnit.rangedWeapon);
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
            Invoke("OnPlayerDeath", 1);
        }

        void OnPlayerDeath() {
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


        public void CallOnResurrect(bool onCorpse) {
            StartCoroutine(DeathFade(onCorpse));
        }

        public void OnResurrect(bool onCorpse) {
            GetComponent<AudioSource>().Stop();
            if (!onCorpse && Checkpoint.CheckpointTransform != null) {
                BaseNavMeshAgent.Warp(Checkpoint.CheckpointTransform.position);
            }
        }

        IEnumerator DeathFade(bool onCorpse) {
            Time.timeScale = 1f;
            ignoreRaycast = true;
            yield return Fader.FadeIn();

            foreach (var resurrect in GetComponents<IResurrect>()) {
                resurrect.OnResurrect(onCorpse);
            }

            yield return new WaitForSecondsRealtime(1f);
            yield return Fader.FadeOut();
            ignoreRaycast = false;
            AfterResurrection();
        }

        void AfterResurrection() {
            BaseNavMeshAgent.isStopped = false;
            GetComponent<Collider>().enabled = true;
            BaseHealth.CurrentHealth = MaxHealth();
        }
    }
}