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

    public void Damage(int dmg){
        currHp -= dmg;
        Debug.Log("hit. curr hp: " + currHp);
        if (currHp <= 0) {
            Debug.Log("dead!");
            isDead = true;
        }
    }

    public void Heal(int hp)
{
    // Increase current health by the healing amount
    currHp += hp;

    // Ensure current health doesn't exceed max health
    if (currHp > maxHp)
    {
        currHp = maxHp;
    }

    Debug.Log("healed. curr hp: " + currHp);

    // If the character was previously dead and is now healed, mark them as alive
    if (isDead && currHp > 0)
    {
        isDead = false;
        Debug.Log("Healed and revived!");
    }
}

}
