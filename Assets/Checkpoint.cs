using UnityEngine;

public class Checkpoint : MonoBehaviour {
    public static Transform CheckpointTransform;
    [SerializeField] Transform spawnPoint;
    
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            CheckpointTransform = spawnPoint;
        }
    }
}