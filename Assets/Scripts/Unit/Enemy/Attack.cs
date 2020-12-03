using System.Collections;
using UnityEngine;

namespace Unit.Enemy{
    public class Attack : MonoBehaviour{
        [SerializeField] private int damage = 10; //Part of weaponSO
        [SerializeField] private float speed = 2f; //Part of weaponSO
        //[SerializeField]public float range = 10;//Part of weaponSO
        private IEnumerator attacking;
        private Health targetHealth;
        private float timer;
        /// <summary>
        ///
        ///
        ///Attack:
        ///CheckVisibility:
        ///An event that sends if you can/can't attack.
        ///
        /// 
        ///Unit:
        ///Health
        ///Weapon(melee or range)
        ///visibilitycheck:
        ///
        ///Attack:
        ///Weapon
        ///Attack with ranged weapon logic:
        ///Shoots while it's in shooting range 
        ///
        ///Attack with melee weapon logic:
        ///Move closer to the target then attack
        /// 
        ///Enemy:
        ///Ai:
        ///vision range
        ///Current state
        ///
        ///attack:
        ///???Enemy only: when in middle move to shooting range??? Cowerd behavior
        ///Enemy: Melee when its in melee range
        ///Patrol
        ///Chase target:
        ///
        ///
        /// 
        /// </summary>

        public Transform projectileSpawnPoint;
        public Bullet projectilePrefab;
        private void Awake(){
            this.timer = -this.speed;
        }

        public void Range(GameObject target){
            Bullet bullet = Instantiate(projectilePrefab, this.projectileSpawnPoint.position, this.projectileSpawnPoint.rotation);
            bullet.Setup(target,this.damage);

            //Play an animation
            //Play sound
            //Play particle system
            //Instatiate bullet or draw linerenderer
        }
        public void Melee(GameObject target){
            RaycastHit hit;
            Physics.SphereCast(transform.position, 2f, transform.forward, out hit, 10f);
            Debug.Log(hit.collider.gameObject.name);
            if(hit.collider.gameObject.name == target.name)
                target.GetComponent<Health>().TakeDamage(this.damage);
            //Play an animation
            //Play sound
            //SphereRaycast infront of the player
        }
        //GameObject (if we are going to instatioate a bullet)
        //Target
        //WeaponSO:
        //Timer
        //Range (melee or shooting)
        
        //TODO: Change TakeDamage to startAttack that can deal dmg ex bullet or range of melee weapon.
        // private IEnumerator Attacking(){
        //     while (true){
        //         yield return new WaitForSeconds(0.5f); //Check what works best between 0.1f and 1f!
        //         if (!(Time.time - this.timer > this.speed)) continue;
        //         this.targetHealth.TakeDamage(this.damage); //Temp!
        //         this.timer = Time.time;
        //     }
        // }
        // private void OnTriggerEnter(Collider other){
        //     if (other.gameObject.layer != LayerMask.NameToLayer("Player"))
        //         return;
        //     this.targetHealth = other.gameObject.GetComponent<Health>();
        //     if (this.attacking != null)
        //         StopCoroutine(this.attacking);
        //     this.attacking = Attacking();
        //     StartCoroutine(this.attacking);
        // }
        // private void OnTriggerExit(Collider other){
        //     if (other.gameObject.layer != LayerMask.NameToLayer("Player"))
        //         return;
        //     StopCoroutine(this.attacking);
        //     this.targetHealth = null;
        // }
    }
}