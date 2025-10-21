using UnityEngine;

public class CharStats : MonoBehaviour
{
    public string characterName;
    
    public int characterLevel;  
    public int currentExperience;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
