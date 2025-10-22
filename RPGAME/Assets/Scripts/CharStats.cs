using UnityEngine;

public class CharStats : MonoBehaviour
{
    public string characterName;
    
    public int characterLevel;  
    public int currentExperience;
    public int[] expToNextLevel;
    public int maxLevel = 100;
    public int baseExp = 1000;

    public int currentHealth;
    public int maxHealth = 100;

    public int currentMana;
    public int maxMana = 50;

    public int strength;
    public int defense;

    public int weaponPower;
    public int armorPower;

    public string equippedWeapon;
    public string equippedArmor;    

    public Sprite characterImage;


    void Start()
    {
        expToNextLevel = new int[maxLevel];
        expToNextLevel[1] = baseExp;    
        for(int i = 2; i < expToNextLevel.Length; i++)
        {
             expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i - 1] * 1.05f);   
        }    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
