using UnityEngine;

namespace Unit {
    public class VisibilityCheck : MonoBehaviour{
        public bool IsVisible(GameObject to) {
            var direction = (to.transform.position - transform.position).normalized;
            RaycastHit hit;
            Physics.Raycast(transform.position, direction, out hit, 200);
            Debug.DrawRay(transform.position, direction * 200,Color.yellow, 10);
            return hit.collider.gameObject.name == to.name;
        }
    }
}