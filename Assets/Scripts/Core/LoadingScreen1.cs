using UnityEngine;

public class LoadingScreen1 : MonoBehaviour
{
    public static LoadingScreen1 instance;

    [SerializeField]
    public GameObject loading_Bar_Holder;   

    private 

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
