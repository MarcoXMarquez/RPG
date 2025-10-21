using UnityEngine;

public class DialogActivator : MonoBehaviour
{
    [TextArea(2, 6)]
    public string[] dialogLines;
    private bool canActivate;

    void Update()
    {
        // Asegurarse de que el di�logo no se active si ya est� mostrando algo
        if (canActivate && Input.GetButtonUp("Fire1") && !DialogManager.instance.dialogBox.activeInHierarchy)
        {
            DialogManager.instance.showDialog(dialogLines);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canActivate = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canActivate = false;
        }
    }
}
