using Unit;
using UnityEngine;

public class Bullet : MonoBehaviour{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody rb;
    private int damage;
    private GameObject target;

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.name == this.target.transform.name){
            this.target.GetComponent<Health>().TakeDamage(this.damage);
            Destroy(gameObject);
        }

        Destroy(gameObject);
    }

    public void Setup(GameObject target, int damage){
        this.target = target;
        this.damage = damage;
        Destroy(gameObject, 5f);
        this.rb.AddForce(transform.up * this.speed, ForceMode.Impulse);
    }
}