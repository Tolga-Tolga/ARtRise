using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField, Range(0, 1000)]
    private int initialHP;
    private int currentHP;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHP = initialHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setHp(int hp){
        currentHP = Mathf.Clamp(hp, 0, initialHP);
    }

    public void SetMaxHP()
    {
        currentHP = initialHP;
    }
    public int getHp(){
        return currentHP;
    }

    public float GetHpPercentage(){
        return (float)currentHP/initialHP;
    }
    
    public void SetDmg(int dmg)
    {
        Debug.Log("[VOICE] Got new dmg with value: " + dmg + ", and I am currently at hp: " + currentHP);
        int hpAfterDmg = currentHP-dmg;
        setHp(hpAfterDmg);
    }
}
