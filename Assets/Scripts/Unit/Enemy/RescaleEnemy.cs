using System;
using UnityEngine;


//some enemies scale really weird.
//Normal size when working in scene, but changes in game (wtf?!?!?)
namespace Unit {
    public class RescaleEnemy : MonoBehaviour {
        void Start() {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}