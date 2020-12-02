using System.Collections;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine;

namespace Unit{
    public class DamageUI : MonoBehaviour{
        //TODO: move the text
        public Text textUI;
        private Vector3 moveUpSpeed;
        public float speed;
        
        public void SetUp(int damage){
            textUI.text = $"-{damage}";
        }

        private void Start(){
            Destroy(this.gameObject, 2f);
            moveUpSpeed.y = speed;
        }

        private void FixedUpdate(){
            transform.position += moveUpSpeed * Time.deltaTime;
            //textUI.color.a -= * Time.deltaTime;
        }
        
    }
}

