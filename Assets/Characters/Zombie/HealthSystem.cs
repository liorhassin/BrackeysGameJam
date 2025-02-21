using UnityEngine;

public class HealthSystem : MonoBehaviour
{

    public int maxHp;
    public bool isDead = false;
    
    private int currHp;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currHp = maxHp;
        if (currHp <= 0) {
            Debug.Log("dead!");
            isDead = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(int dmg){
        currHp -= dmg;
        if (currHp <= 0) {
            Debug.Log("dead!");
            isDead = true;
        }
    }
}
