using Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI {
    public class EnterPortal : MonoBehaviour {
        public void Enter() {
            FindObjectOfType<Fader>().LoadShit();
        }

        public void Close(GameObject popupWindow) {
            popupWindow.SetActive(false);
        }
    }
}