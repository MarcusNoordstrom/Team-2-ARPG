using Unit;
using UnityEngine;

namespace UI {
    public class HealthBarUI : MonoBehaviour {
        public GameObject healthTickPrefab;
        public GameObject healthStartTickPrefab;
        public GameObject healthEndTickPrefab;

        //TODO: Fix horizontal layout group OR switch back to Grid Layout or FIND OTHER SOLUTION MF
        public void InstantiateHealthTicks() {
            // for (int x = 0; x < Health.CurrentHealthBars; x++) {
            //     if (x == 0) {
            //         Instantiate(healthStartTickPrefab, gameObject.transform);
            //     }
            //     else if (x == Health.CurrentHealthBars - 1) {
            //         Instantiate(healthEndTickPrefab, gameObject.transform);
            //     }
            //     else {
            //         Instantiate(healthTickPrefab, gameObject.transform);
            //     }
            // }
        }

        public void RemoveHealthTick(int ticksToRemove) {
            for (int x = 0; x < ticksToRemove; x++) {
                Destroy(transform.GetChild(transform.childCount - 1).gameObject);
            }
        }
    }
}