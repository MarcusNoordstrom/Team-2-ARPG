using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagment;

public class LoadingScreen1 : MonoBehaviour
{
    public static LoadingScreen1 instance;

    SerializeField]
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
            DontDestroyOnLoad(gameobject);
        }

    }
   
}
