using Unit;
using UnityEngine;

public class Bullet : MonoBehaviour {
    [SerializeField] float speed;
    [SerializeField] Rigidbody rb;
    int damage;
    GameObject target;

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == target.transform.name) {
            target.GetComponent<Health>().TakeDamage(damage);
            Destroy(gameObject);
        }

        Destroy(gameObject);
    }

    public void Setup(GameObject target, int damage) {
        this.target = target;
        this.damage = damage;
        Destroy(gameObject, 5f);
        rb.AddForce(transform.up * speed, ForceMode.Impulse);
    }
}