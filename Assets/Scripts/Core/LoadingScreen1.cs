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
    public float Load_level_time = 2f;
    void Awake()
    {
        MakeSingleton();
    }

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(LoadingSomeLevel());
      
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
         
        SceneManager.LoadScene(levelname);
      
    }
    
    void ShowloadingScreen() {

        if (progress_Value < 1f) {

            progress_Value += progress_Multiplier_1 *progress_Multiplier_2;
            loading_Bar_Progress.GetComponent<Image>().fillAmount = progress_Value;

            if(progress_Value < 1f) {

                progress_Value = 1.1f;

                loading_Bar_Progress.GetComponent<Image>().fillAmount = 0f;

                loading_Bar_Holder.SetActive(false);

                //Time.timeScale = 1f;
            }
        } // if progress < 1
    }
    IEnumerator LoadingSomeLevel(){
        yield return new WaitForSeconds(Load_level_time);
        Loadlevel("LavaLevel");
    }
} //class
