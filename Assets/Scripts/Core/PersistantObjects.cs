using UnityEngine;

namespace Core {
    public class PersistantObjects : MonoBehaviour {
        public GameObject persistantObjects;

        static bool _hasBeenInstaniated = false;

        void Start() {
            if (_hasBeenInstaniated) return;

            SpawnPersistantObjects();

            _hasBeenInstaniated = true;
        }

        void SpawnPersistantObjects() {
            var persistantObject = Instantiate(this.persistantObjects);
            DontDestroyOnLoad(persistantObject);
        }
    }
}