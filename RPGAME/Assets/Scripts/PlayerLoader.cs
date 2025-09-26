using UnityEngine;

public class PlayerLoader : MonoBehaviour
{
    public GameObject playerPrefab;

    void Start()
    {
        // Only spawn the player if one doesn't exist already
        if (PlayerController2D.instance == null)
        {
            GameObject newPlayer = Instantiate(
                playerPrefab,
                transform.position, // spawn at the loader's position
                Quaternion.identity
            );

            // Make sure PlayerController2D.instance points to this new one
            PlayerController2D.instance = newPlayer.GetComponent<PlayerController2D>();
        }
    }
}