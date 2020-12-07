using Unit;
using UnityEngine;

public class TestAttack : MonoBehaviour {
    public int damage = 10; //Part of weaponSO
    public float timer;
    float initTimer;
    Health targetHealth;


    void Awake() {
        initTimer = timer;
    }

    // private IEnumerator Attacking(){
    //     while (true){
    //         yield return new WaitForSeconds(0.1f);
    //         if ((Time.time - timer) > speed){
    //             targetHealth.TakeDamage(damage);//Change this!
    //             timer = Time.time;
    //         }
    //     }
    // }

    //A way to do it without Coroutine
    void OnCollisionEnter(Collision other) {
        timer--;
        if (!(timer <= 0) || !other.gameObject.CompareTag("Player")) return;
        Debug.Log("B");
        targetHealth = other.gameObject.GetComponent<Health>();
        targetHealth.TakeDamage(damage);
        timer = initTimer;
    }

    // private void OnTriggerEnter(Collider other){
    //     if (other.gameObject.layer == LayerMask.NameToLayer("Player")){
    //         targetHealth = other.gameObject.GetComponent<Health>();
    //         if (attacking != null)
    //             StopCoroutine(attacking);
    //         attacking = Attacking();
    //         StartCoroutine((attacking));
    //     }
    // }
    // private void OnTriggerExit(Collider other){
    //     if (other.gameObject.layer == LayerMask.NameToLayer("Player")){
    //         StopCoroutine(attacking);
    //         targetHealth = null;
    //     }
    // }
}