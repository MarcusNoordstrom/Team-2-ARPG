using Unit;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class HealthBarUI : MonoBehaviour {
        public GameObject healthTickPrefab;
        public GameObject healthStartTickPrefab;
        public GameObject healthEndTickPrefab;
        
        public void InstantiateHealthTicks() {
            for (int x = 0; x < Health.CurrentHealthBars; x++) {
                if (x == 0) {
                    Instantiate(healthStartTickPrefab, gameObject.transform);
                }
                else if (x == Health.CurrentHealthBars - 1) {
                    Instantiate(healthEndTickPrefab, gameObject.transform);
                }
                else {
                    Instantiate(healthTickPrefab, gameObject.transform);
                }
            }

            GridLayoutGroup gridLayoutGroup = GetComponent<GridLayoutGroup>();
            var amountOfTicks = Health.CurrentHealthBars;

            gridLayoutGroup.cellSize = new Vector2(40 - amountOfTicks, 100);
        }

        public void RemoveHealthTick(int ticksToRemove) {
            for (int x = 0; x < ticksToRemove; x++) {
                Destroy(transform.GetChild(transform.childCount - 1).gameObject);
            }
        }
    }
}