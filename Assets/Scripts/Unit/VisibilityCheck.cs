using System;
using UnityEngine;

namespace Unit {
    public class VisibilityCheck : MonoBehaviour {
        public bool IsVisible(GameObject to, float range) {
            var direction = (new Vector3(to.transform.position.x, to.transform.position.y + 4, to.transform.position.z) - transform.position).normalized;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit, range)) {
                Debug.DrawRay(transform.position, direction * 200, Color.yellow, 10);
                return hit.collider.gameObject.name == to.name;
            }
            else {
                return false;
            }
        }
    }
}