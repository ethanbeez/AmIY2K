using UnityEngine;
using System.Collections;

public class EndFairyAni : MonoBehaviour
{
    [Header("Animator Reference")]
    [SerializeField] private Animator fairyAnimator;

    [Header("Animations")]
    [SerializeField] private AnimationClip lameAnimation;
    [SerializeField] private AnimationClip havocAnimation;
    [SerializeField] private AnimationClip mayhemAnimation;

    [Header("Display Results Reference")]
    [SerializeField] private DisplayResults displayResults;

    private int totalSoulsCollected = 0;

    private void Start()
    {
        if (displayResults != null)
        {
            totalSoulsCollected = displayResults.GetTotalSouls();
            PlayFairyAnimation();
        }
        else
        {
            Debug.LogError("DisplayResults reference is missing in EndFairyAni.");
        }
    }

private void PlayFairyAnimation()
{
    if (fairyAnimator == null)
    {
        Debug.LogError("Animator reference is missing in EndFairyAni.");
        return;
    }

    // Reset triggers
    fairyAnimator.ResetTrigger("Win");
    fairyAnimator.ResetTrigger("Lose");

    if (totalSoulsCollected <= 100)  
    {
        fairyAnimator.SetTrigger("Lose");
        Debug.Log("❌ SetTrigger: Lose (Failure_Clip)");
    }
    else  
    {
        // Force animation instead of using triggers
        fairyAnimator.Play("Victory_Clip", 0, 0);
        Debug.Log("⚡ Forced Victory_Clip to play.");
    }
}

private IEnumerator CheckAnimationState()
{
    yield return new WaitForSeconds(0.1f);
    
    AnimatorStateInfo stateInfo = fairyAnimator.GetCurrentAnimatorStateInfo(0);
    Debug.Log($"🎭 Animator State Changed: {stateInfo.fullPathHash}, Is Victory: {stateInfo.IsName("Victory_Clip")}, Is Failure: {stateInfo.IsName("Failure_Clip")}");
}


}
