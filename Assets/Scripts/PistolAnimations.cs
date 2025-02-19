using UnityEngine;

public class PistolAnimations : MonoBehaviour
{
    private Animator mAnimator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Shoot(){
        if (mAnimator != null){
            mAnimator.SetTrigger("Shoot");
        }
    }
    
    public void Walk(){
        if (mAnimator != null){
            mAnimator.SetTrigger("Walk");
        }
    }

    public void Stand(){
        if (mAnimator != null){
            mAnimator.SetTrigger("Stand");
        }
    }
}
