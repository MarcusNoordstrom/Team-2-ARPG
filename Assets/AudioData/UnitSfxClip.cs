using UnityEngine;


namespace Unit{
    [CreateAssetMenu(menuName = "Sound/Sfx/Unit")]
    public class UnitSfxClip : ScriptableObject{
        public UnitSfxId id;
        public AudioClip audioClip;
    }

    [System.Serializable]
    public enum UnitSfxId{
        Walk,
        Shoot,
        TakingDamage,
        Melee,
        NearDeath,
        Death,
        Resurrect
    }
}

