using UnityEngine;

public class LoadingScreen1 : MonoBehaviour
{
    public static LoadingScreen1 instance;

    [SerializeField]
    private GameObject loading_Bar_Holder;

    [SerializeField]
    private GameObject loading_Bar_Process;

    void Awake() {
        MakeSingleton();
    }

    // Start is called before the first frame update
    void Start() { 
        
    }
    void MakeSingleton() {
        if(instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }
   
}
