using UnityEngine;
using UnityEngine.UI;

namespace Unit{
    public class DamageUI : MonoBehaviour{
        [SerializeField] private Text textUI;
        [SerializeField] private float speed = 2;
        [SerializeField] private float alphaFadeSpeed = 1f;
        [SerializeField] private float destroyTime = 2f;
        [Range(1.5f, 2f)] [SerializeField] private float yPositionOffset = 1.5f;
        private Transform cameraTransform;
        private Color color;
        private Vector3 moveUpSpeed;

        private void FixedUpdate(){
            transform.position += this.moveUpSpeed * Time.deltaTime;
            this.color.a -= this.alphaFadeSpeed * Time.deltaTime;
            this.textUI.color = this.color;
        }

        public void SetUp(int damage){
            this.textUI.text = $"-{damage}";
            Destroy(gameObject, this.destroyTime);
            transform.position += transform.up * this.yPositionOffset;
            this.moveUpSpeed = transform.up * this.speed;
            this.color = this.textUI.color;
        }
    }
}