using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;
    public Animator animator;

    public static PlayerController2D instance;
    public string areaTransitionName;
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;

    public bool canMove = true; 
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
                return;
            }
        }

        if (Camera.main != null)
        {
            var cam = Camera.main.GetComponent<CameraController>();
            if (cam != null)
                cam.target = transform;
        }
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
        if (canMove)
        {
            // Calcular velocidad
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

        if(Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == -1)
        {
            if (canMove)
            {
                animator.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
                animator.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));

            }
        }   

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, bottomLeftLimit.x + 0.5f, topRightLimit.x - 0.5f),
            Mathf.Clamp(transform.position.y, bottomLeftLimit.y + 0.5f, topRightLimit.y - 0.5f),
            transform.position.z
        );
    }

    public void SetBounds(Vector3 bottomLeft, Vector3 topRight)
    {
        bottomLeftLimit = bottomLeft + new Vector3(1f,1f,0f);
        topRightLimit = topRight + new Vector3(-1f,-1f,0f);
    }
}
