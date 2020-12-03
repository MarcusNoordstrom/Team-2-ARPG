using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit {
    public class SpotTarget : MonoBehaviour {
        public bool IsVisible(GameObject to) {
            var direction = (to.transform.position - this.transform.position).normalized;
            RaycastHit hit;
            Physics.Raycast(transform.position, direction, out hit, 200);
            Debug.DrawRay(transform.position, direction * 200,
                Color.yellow, 10);
            ;
            if (hit.collider.gameObject.layer == to.layer) {
                Debug.Log("Did Hit");
                return true;
            } else return false;
        }
    }
}