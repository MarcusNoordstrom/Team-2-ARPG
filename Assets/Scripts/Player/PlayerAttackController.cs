using Unit;
using UnityEngine;
using UnityEngine.AI;

namespace Player {
    public class PlayerAttackController : MonoBehaviour, IAction {
        GameObject _target;


        //TODO add a shift + left click to attack from current position towards where mouse is?
        void Update() {
            if (!Physics.Raycast(PlayerController.GetMouseRay(), out var hit)) return;
            if (hit.collider.GetComponent<StationaryEnemy>() == null) return;

            GetComponent<Action>().StartAction(this);
            
            if (Input.GetKeyUp(KeyCode.Mouse0)) {
                GetComponent<Animator>().SetTrigger("RangedAttack");
                _target = hit.collider.gameObject;
                transform.LookAt(_target.transform.position);
            }
        }

        //animation event
        void Shoot() {
            if (_target == null || _target.GetComponent<Health>().IsDead) return;
            GetComponent<Attack>().SpawnBullet();
            //TODO play muzzle effect when shooting
        }

        //animation event
        void ShootFinish() {
            if (_target == null || _target.GetComponent<Health>().IsDead) return;
            GetComponent<Animator>().SetTrigger("RangedAttack");
        }

        //TODO check if in melee range when using melee attack


        public void ActionToStart() {
            //GetComponent<NavMeshAgent>().isStopped = true;
        }
    }
}