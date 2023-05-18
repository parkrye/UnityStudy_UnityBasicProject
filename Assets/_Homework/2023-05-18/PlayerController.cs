using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Homework
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] Vector2 moveDir;
        [SerializeField] UnityEvent fireEvent;

        private void Update()
        {
            transform.Translate(moveDir.x, 0f, moveDir.y);
        }

        void OnMove(InputValue inputValue)
        {
            moveDir = inputValue.Get<Vector2>() * 0.01f;
        }

        void OnFire()
        {
            fireEvent.Invoke();
        }
    }

}