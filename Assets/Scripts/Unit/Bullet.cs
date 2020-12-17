using Unit;
using UnityEngine;

public class Bullet : MonoBehaviour {
    [SerializeField] float speed;
    [SerializeField] Rigidbody rb;
    int damage;
    GameObject target;

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer != BulletFiredBy) {
            other.GetComponent<Health>().TakeDamage(damage);
            Destroy(gameObject);
        }

        Destroy(gameObject);
    }

    public LayerMask BulletFiredBy { get; set; }

    public void Setup(GameObject target, int damage) {
        this.target = target;
        this.damage = damage;
        Destroy(gameObject, 5f);
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);
    }
}