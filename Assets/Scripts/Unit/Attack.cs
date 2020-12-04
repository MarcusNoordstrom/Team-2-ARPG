using UnityEngine;

namespace Unit{
    public class Attack : MonoBehaviour{
        [SerializeField] private int damage = 10; //Part of weaponSO
        //[SerializeField] private float speed = 2f; //Part of weaponSO
        //[SerializeField]public float range = 10;//Part of weaponSO
        [SerializeField]private Bullet projectilePrefab;//From weaponSO
        [SerializeField]private Transform projectileSpawnPoint;
        //TODO: method that calls attack method based on current weapon.
        public void Range(GameObject target){
            var bullet = Instantiate(this.projectilePrefab, this.projectileSpawnPoint.position, this.projectileSpawnPoint.rotation);
            bullet.Setup(target,this.damage);
        }
        //TODO: Either use raycast, enable a gameobject or spherecast.
        public void Melee(GameObject target){
            target.gameObject.GetComponent<Health>().TakeDamage(this.damage);
        }
        //TODO: Fix attack delay on next attack!
    }
}