using UnityEngine;

namespace Unit {
 
    [CreateAssetMenu]
    public class BasicEnemy : BasicUnit {
        public float targetRange;
        public float stopChaseDistance;
    }
}