using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider slider;
    private PlayerController2D playerController;

    void Start()
    {
        // Obtiene el componente Slider del objeto actual
        slider = GetComponent<Slider>();

        // Busca al jugador con el tag "Player"
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerController = player.GetComponent<PlayerController2D>();
        }
        else
        {
            Debug.LogWarning("⚠️ No se encontró un objeto con tag 'Player'.");
        }
    }

    void Update()
    {
        if (playerController != null)
        {
            slider.maxValue = playerController.maxHealth;
            slider.value = playerController.currentHealth;
        }
    }
}
