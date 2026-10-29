using UnityEngine;

public class InteractableObject : MonoBehaviour
{

    [Header("Contenu du texte")]
    [TextArea(3, 5)]
    public string[] linesToDisplay;

    [Header("Référence UI")]
    public GameObject promptUI;
    public string[] LinesToDisplay => linesToDisplay;

    void Start()
    {
        if (promptUI != null)
        {
            promptUI.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("capte player");
            PlayerMovement player = collision.GetComponent<PlayerMovement>();

            if (player != null)
            {
                player.SetCurrentInteractable(this);
            }

            if(promptUI != null && GameManager.totalInteractionsCount < 3)
            {
                promptUI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            PlayerMovement player = collision.GetComponent<PlayerMovement>();
            if(player != null)
            {
                player.ClearCurrentInteractable();
            }

            if(promptUI != null)
            {
                promptUI.SetActive(false);
            }
        }
    }

    public void OnInteracted()
    {
        if(GameManager.totalInteractionsCount < 3)
        {
            GameManager.totalInteractionsCount++;
        }

        if(promptUI != null)
        {
            promptUI.SetActive(false);
        }
    }
}