using System.Collections;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine;

namespace Unit{
    public class DamageUI : MonoBehaviour{
        [SerializeField]private Text textUI;
        [SerializeField]private float speed = 2;
        [SerializeField]private float alphaFadeSpeed = 1f;
        [SerializeField] private float destroyTime = 2f;
        [Range(1.5f,2f)] [SerializeField] private float yOffset = 2f;
        private Transform _cameraTransform;
        private Vector3 _moveUpSpeed;
        private Color _color;
        public void SetUp(int damage){
            textUI.text = $"-{damage}";
            Destroy(this.gameObject, destroyTime);
            transform.position += transform.up * yOffset;
            _moveUpSpeed = transform.up * speed;
            _color = textUI.color;
        }
        private void FixedUpdate(){
            transform.position += _moveUpSpeed * Time.deltaTime;
            _color.a -= alphaFadeSpeed * Time.deltaTime;
            textUI.color = _color;
        }
        
    }
}

