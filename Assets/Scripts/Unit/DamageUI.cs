using UnityEngine;
using UnityEngine.UI;

namespace Unit {
    public class DamageUI : MonoBehaviour {
        [SerializeField] Text textUI;
        [SerializeField] float speed = 2;
        [SerializeField] float alphaFadeSpeed = 1f;
        [SerializeField] float destroyTime = 2f;
        [Range(1.5f, 2f)] [SerializeField] float yPositionOffset = 1.5f;
        Transform cameraTransform;
        Color color;
        Vector3 moveUpSpeed;

        void FixedUpdate() {
            this.transform.position += this.moveUpSpeed * Time.deltaTime;
            this.color.a -= this.alphaFadeSpeed * Time.deltaTime;
            this.textUI.color = this.color;
        }

        public void SetUp(int damage) {
            this.textUI.text = $"-{damage}";
            Destroy(this.gameObject, this.destroyTime);
            this.transform.position += this.transform.up * this.yPositionOffset;
            this.moveUpSpeed = this.transform.up * this.speed;
            this.color = this.textUI.color;
        }
    }
}