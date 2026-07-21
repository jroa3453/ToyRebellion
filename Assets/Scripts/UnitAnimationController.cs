using UnityEngine;

public class UnitAnimationController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        if (animator != null)
        {
            animator.ResetTrigger("Attack");
        }
    }


    public void PlayAttack()
    {
        if(animator == null)
        {
            Debug.LogError("Animator component is missing.");
            return;
        }
        animator.SetTrigger("Attack");
    }
}



