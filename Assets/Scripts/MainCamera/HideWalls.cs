using UnityEngine;
using UnityEngine.Rendering;

namespace MainCamera{
    public class HideWalls : MonoBehaviour{
        private void OnTriggerEnter(Collider other){
            if (other.gameObject.layer == LayerMask.NameToLayer("Ignore Raycast")){
                other.GetComponent<Renderer>().shadowCastingMode = ShadowCastingMode.ShadowsOnly;
            }
        }

        private void OnTriggerExit(Collider other){
            if (other.gameObject.layer == LayerMask.NameToLayer("Ignore Raycast")){
                other.GetComponent<Renderer>().shadowCastingMode = ShadowCastingMode.On;
            }
        }
    }
}

