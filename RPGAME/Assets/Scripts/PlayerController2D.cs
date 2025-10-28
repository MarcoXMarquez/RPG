using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    // -------------------- MOVIMIENTO --------------------
    [Header("Movement Settings")]
    public Rigidbody2D rb;
    public float moveSpeed;
    public Animator animator;
    public bool canMove = true;

    // -------------------- SINGLETON --------------------
    public static PlayerController2D instance;
    public string areaTransitionName;
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;

    // -------------------- STATS --------------------
    [Header("Character Stats")]
    public string characterName = "Player";

    public int characterLevel = 1;
    public int currentExperience;
    public int[] expToNextLevel;
    public int maxLevel = 100;
    public int baseExp = 1000;

    public int currentHealth;
    public int maxHealth = 100;
    public int currentMana;
    public int maxMana = 50;
    public int[] manaLevelBonus;

    public int strength;
    public int defense;

    public int weaponPower;
    public int armorPower;

    public string equippedWeapon;
    public string equippedArmor;

    public Sprite characterImage;

    // =====================================================
    //                     UNITY METHODS
    // =====================================================
    void Awake()
    {
        // Singleton (para mantener el player entre escenas)
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Asignar cámara al jugador
        if (Camera.main != null)
        {
            var cam = Camera.main.GetComponent<CameraController>();
            if (cam != null)
                cam.target = transform;
        }
    }

    void Start()
    {
        // Inicializar estadísticas
        currentHealth = maxHealth;
        currentMana = maxMana;

        expToNextLevel = new int[maxLevel];
        expToNextLevel[1] = baseExp;

        for (int i = 2; i < expToNextLevel.Length; i++)
        {
            expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i - 1] * 1.05f);
        }
    }

    void Update()
    {
        HandleMovement();
        HandleLeveling();
    }

    // =====================================================
    //                     MOVIMIENTO
    // =====================================================
    private void HandleMovement()
    {
        // Leer inputs crudos
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // 🔧 Bloquear diagonales → dar prioridad al movimiento horizontal
        if (moveX != 0) moveY = 0;

        if (canMove)
        {
            Vector2 movement = new Vector2(moveX, moveY) * moveSpeed;
            rb.linearVelocity = movement;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }

        // Animaciones
        animator.SetFloat("moveX", rb.linearVelocity.x);
        animator.SetFloat("moveY", rb.linearVelocity.y);

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            if (canMove)
            {
                animator.SetFloat("lastMoveX", moveX);
                animator.SetFloat("lastMoveY", moveY);
            }
        }

        // Limitar movimiento a los bordes del mapa
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, bottomLeftLimit.x + 0.5f, topRightLimit.x - 0.5f),
            Mathf.Clamp(transform.position.y, bottomLeftLimit.y + 0.5f, topRightLimit.y - 0.5f),
            transform.position.z
        );
    }

    // =====================================================
    //                     STATS / NIVEL
    // =====================================================
    private void HandleLeveling()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            AddExperience(1000);
        }
    }

    public void AddExperience(int expToAdd)
    {
        currentExperience += expToAdd;

        if (characterLevel < maxLevel)
        {
            if (currentExperience > expToNextLevel[characterLevel])
            {
                currentExperience -= expToNextLevel[characterLevel];
                characterLevel++;

                // Aumentar atributos alternadamente
                if (characterLevel % 2 == 0)
                    strength++;
                else
                    defense++;

                // Incrementar HP y curar
                maxHealth = Mathf.FloorToInt(maxHealth * 1.05f);
                currentHealth = maxHealth;

                // Incrementar MP con bonus de nivel
                if (manaLevelBonus != null && characterLevel < manaLevelBonus.Length)
                    maxMana += manaLevelBonus[characterLevel];

                currentMana = maxMana;
            }
        }

        if (characterLevel >= maxLevel)
            currentExperience = 0;
    }

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            gameObject.SetActive(false); // “muere” el jugador
            Debug.Log("El jugador ha sido derrotado.");
        }

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    // =====================================================
    //                     LÍMITES DE MAPA
    // =====================================================
    public void SetBounds(Vector3 bottomLeft, Vector3 topRight)
    {
        bottomLeftLimit = bottomLeft + new Vector3(1f, 1f, 0f);
        topRightLimit = topRight + new Vector3(-1f, -1f, 0f);
    }
}
