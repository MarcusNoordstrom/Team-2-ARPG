using Unit;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UnitSfxIdEvent : UnityEvent<UnitSfxId>{}

public class TESTSoundController : MonoBehaviour{
    [SerializeField]private UnitSfxIdEvent unitSfxIdEvent;
    private UnitSfxId id;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)){
            Debug.Log("TestA");
            id = UnitSfxId.Death;
            unitSfxIdEvent?.Invoke(id);
        }
        if (Input.GetKeyDown(KeyCode.S)){
            id = UnitSfxId.TakingDamage;
            unitSfxIdEvent?.Invoke(id);
        }
        if (Input.GetKeyDown(KeyCode.D)){
            id = UnitSfxId.NearDeath;
            unitSfxIdEvent?.Invoke(id);
        }
            
            
    }
}