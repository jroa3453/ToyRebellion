using UnityEngine;

public class UnitAnimationController : MonoBehaviour
{
    private Animator animator;

    public void Update()
    {
        

    }

    public void Awake()
    {
        animator = GetComponent<Animator>();
        if(animator == null)
        {
            Debug.LogError("Animator component is missing.");
        }
    }
    public void Attack()
    {
        return;
        animator.SetTrigger("Attack");
    }
}
