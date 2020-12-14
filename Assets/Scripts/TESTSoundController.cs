using Unit;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UnitSfxIdEvent : UnityEvent<UnitSfxId>{}

public class TESTSoundController : MonoBehaviour{
    [SerializeField]private UnitSfxIdEvent unitSfxIdEvent;
    [SerializeField]private UnitSfxIdEvent unitSfxIdEvent2D;
    private UnitSfxId id;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)){
            Debug.Log("Test2D");
            id = UnitSfxId.Death;
            unitSfxIdEvent2D?.Invoke(id);
        }
        if (Input.GetKeyDown(KeyCode.S)){
            id = UnitSfxId.TakingDamage;
            unitSfxIdEvent?.Invoke(id);
        }
        if (Input.GetKeyDown(KeyCode.D)){
            id = UnitSfxId.NearDeath;
            unitSfxIdEvent2D?.Invoke(id);
        }
            
            
    }
}