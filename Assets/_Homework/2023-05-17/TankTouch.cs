using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Homework
{
    public class TankTouch : MonoBehaviour
    {
        [SerializeField] Animator animator;
        [SerializeField] Vector3 touchPos, movePos;

        public void OnFireButton()
        {
            animator.SetTrigger("Fire");
        }

        void Update()
        {
            
        }

        public void OnTouchDownButton()
        {
            touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition).normalized;
        }

        public void OnTouchingButton()
        {
            movePos = Camera.main.ScreenToWorldPoint(Input.mousePosition).normalized;
        }

        public void OnTouchUpButton()
        {
            touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition).normalized;
            movePos = Camera.main.ScreenToWorldPoint(Input.mousePosition).normalized;
        }
    }

}