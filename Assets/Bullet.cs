using System;
using System.Security.Cryptography;
using Unit;
using UnityEngine;

public class Bullet : MonoBehaviour{
    [SerializeField] private float speed;
    [SerializeField]private Rigidbody rigidbody;
    private GameObject target;
    private int damage;

    public void Setup(GameObject target, int damage){
        this.target = target;
        this.damage = damage;
        Destroy(this.gameObject, 5f);
        this.rigidbody.AddForce(transform.up * this.speed, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other){
        if (other.gameObject == this.target){
            this.target.GetComponent<Health>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
