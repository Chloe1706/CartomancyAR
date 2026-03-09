using UnityEngine;

public class FreezeAnimationAtPoint : MonoBehaviour
{
    // Reference to the Animator component
    private Animator animator;

    // The point where I want the animation to stop (0 = start, 1 = end)
    [Range(0f, 1f)]
    public float stopAtNormalizedTime = 0.75f;

    // Prevent freezing multiple times
    private bool hasFrozen = false;

    void Start()
    {
        // Get the Animator attached to this GameObject
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // If the clip has already been frozen, do nothing 
        if (hasFrozen) return;

        // Get info about the currently playing animation state
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // normalizedTime increases from 0 to 1 during playback
        if (stateInfo.normalizedTime >= stopAtNormalizedTime)
        {
            // Freeze animation exactly at this frame
            animator.speed = 0f;

            hasFrozen = true;
        }
    }
}