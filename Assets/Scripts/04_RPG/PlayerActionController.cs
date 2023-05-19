using UnityEngine;

namespace RPG
{
    public class PlayerActionController : MonoBehaviour
    {
        Animator animator;

        // Start is called before the first frame update
        void Awake()
        {
            animator = GetComponent<Animator>();
        }

        void OnAttack()
        {
            animator.SetTrigger("Attack");
        }

        void OnAttackCombo()
        {
            animator.SetInteger("AttackCombo", animator.GetInteger("AttackCombo") + 1);
        }

        void OnAttackEnd()
        {
            animator.SetInteger("AttackCombo", animator.GetInteger("AttackCombo") - 1);
        }

        void OnGuard()
        {
            animator.SetTrigger("GuardUp");
            animator.SetBool("Guard", !animator.GetBool("Guard"));
        }

        void OnTalk()
        {

        }

        public void OnHit()
        {
            animator.SetTrigger("Hit");
        }

        public void OnDead()
        {
            animator.SetTrigger("Dead");
        }
    }
}