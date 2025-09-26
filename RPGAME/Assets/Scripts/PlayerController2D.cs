using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;
    public Animator animator;

    public static PlayerController2D instance;
    public string areaTransitionName;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        Camera.main.GetComponent<CameraController>().target = transform;

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        // Leer inputs crudos
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // 🔧 Bloquear diagonales → dar prioridad al movimiento horizontal
        if (moveX != 0)
        {
            moveY = 0;
        }

        // Calcular velocidad
        Vector2 movement = new Vector2(moveX, moveY) * moveSpeed;
        rb.linearVelocity = movement;

        // Animaciones
        animator.SetFloat("moveX", rb.linearVelocity.x);
        animator.SetFloat("moveY", rb.linearVelocity.y);

        if (moveX != 0 || moveY != 0)
        {
            animator.SetFloat("lastMoveX", moveX);
            animator.SetFloat("lastMoveY", moveY);
        }
    }
}
