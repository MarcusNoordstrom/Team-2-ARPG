using System;
using UnityEngine;

namespace Unit{
    public class Attack : MonoBehaviour{
        public int attackDamage = 10; //Change to private and add a propertie
        public string attackLayerName;

        private void OnTriggerEnter(Collider other){
            if (other.gameObject.layer == LayerMask.NameToLayer(attackLayerName)){
                other.GetComponent<Health>().TakeDamage(attackDamage);
            }
        }
    }
}

