using System;
using System.Runtime.CompilerServices;
using GameStates;
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
            transform.position += moveUpSpeed * Time.deltaTime;
            color.a -= alphaFadeSpeed * Time.deltaTime;
            textUI.color = color;
        }

        void LateUpdate() {
            if (Time.timeScale == 0) {
                Destroy(gameObject);
            }
        }

        public void SetUp(int damage) {
            textUI.text = $"-{damage}";
            Destroy(gameObject, destroyTime);
            transform.position += transform.up * yPositionOffset;
            moveUpSpeed = transform.up * speed;
            color = textUI.color;
        }
    }
}