using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; //this is supposed to bring forth the fill.amount
using System.Collections.Generic;
using System.Collections;
public class LoadingScreen1 : MonoBehaviour
{
    public static LoadingScreen1 instance;

    [SerializeField]
    private GameObject loading_Bar_Holder;

    [SerializeField]
    private GameObject loading_Bar_Progress;

    private float progress_Value = 1.1f;
    private float progress_Multiplier_1 = 0.5f;
    private float progress_Multiplier_2 = 0.07f;
    void Awake()
    {
        MakeSingleton();
    }

    // Start is called before the first frame update
    void Start() { 
        
      
    }
    void Update() {
        ShowloadingScreen();

    }
    void MakeSingleton() {
        if(instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void Loadlevel(string levelname) { //Need to apply the scene we want to transition to

        loading_Bar_Holder.SetActive(true);

        progress_Value = 0f;

        //Time.timeScale = 0f;
        //To be added 
        SceneManager.LoadScene(levelname);
      
    }
    
    void ShowloadingScreen() {

        if (progress_Value < 1f) {

            progress_Value += progress_Multiplier_1 *progress_Multiplier_2;
           // loading_Bar_Progress.fillAmount = progress_Value; //I'm supposed to be able to write fillAmount to make the progress bar fill up but fillAmount isn't working on my end. Think someone else can get it to work?


        }

    }
}
