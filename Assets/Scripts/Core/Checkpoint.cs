using System;
using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Checkpoint : MonoBehaviour {
    public static Transform CheckpointTransform;
    [SerializeField] Transform spawnPoint;
    // Material thisMat;
    // [SerializeField] float dissolveSpeed;
    // bool isEnabled;
    // bool a;
    
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            CheckpointTransform = spawnPoint;
        }
    }

    // void Start() {
    //     thisMat = GetComponent<Renderer>().material;
    //     thisMat.SetFloat("Dissolve", 1);
    // }
    
    // void OnTriggerExit(Collider other) {
    //     if (thisMat.GetFloat("Dissolve") >= 0 && !isEnabled) {
    //         a = true;
    //         StartCoroutine(FadeOut());
    //     }
    // }
    
    void OnTriggerStay(Collider other) {
        // if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
        //     thisMat.SetFloat("Dissolve", Mathf.MoveTowards(thisMat.GetFloat("Dissolve"), 0, dissolveSpeed * Time.deltaTime));
        //     
        //     if (thisMat.GetFloat("Dissolve") <= 0) {
        //         CheckpointTransform = spawnPoint;
        //         isEnabled = true;
        //         //TODO: Add particle on this Dissolve = 0
        //     }
        // }
    }
    // IEnumerator FadeOut() {
    //     while (a) {
    //         thisMat.SetFloat("Dissolve", Mathf.MoveTowards(thisMat.GetFloat("Dissolve"), 1, dissolveSpeed * Time.deltaTime));
    //         if (thisMat.GetFloat("Dissolve") >= 1) {
    //             a = false;
    //         }
    //         yield return null;
    //     }
    // }
}