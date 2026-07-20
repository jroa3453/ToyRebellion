using UnityEngine;

public class UnitAnimationController : MonoBehaviour
{
    private Animator animator;

    public void Awake()

    {
        animator = GetComponent<Animator>();
        if(animator == null)
        {
            Debug.LogError("Animator component is missing.");
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



