using UnityEngine;

public abstract class HandItem : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool use = Input.GetKey(KeyCode.Mouse0);
        if (use){
            Use();
            return;
        }

        bool stopUse = Input.GetKeyUp(KeyCode.Mouse0);
        if (stopUse){
            StopUsing();
        }
    }

    public abstract void Use();
    public abstract void StopUsing();
}
