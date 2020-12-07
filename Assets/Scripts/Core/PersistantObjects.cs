using UnityEngine;

namespace Core {
    public class PersistantObjects : MonoBehaviour {
        static bool _hasBeenInstaniated;
        public GameObject persistantObjects;

        void Start() {
            if (_hasBeenInstaniated) return;

            SpawnPersistantObjects();

            _hasBeenInstaniated = true;
        }

        void SpawnPersistantObjects() {
            var persistantObject = Instantiate(persistantObjects);
            DontDestroyOnLoad(persistantObject);
        }
    }
}