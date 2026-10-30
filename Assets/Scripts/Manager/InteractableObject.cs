using UnityEngine;

public class InteractableObject : MonoBehaviour
{

    [Header("Contenu du texte")]
    [TextArea(3, 5)]
    public string[] linesToDisplay;

    [Header("Référence UI")]
    public GameObject promptUI;
    public string[] LinesToDisplay => linesToDisplay;

    [SerializeField] private TaskObject _task;
    public TaskObject Task => _task;

    private void Awake()
    {
        if (_task == null)
        {
            _task = GetComponent<TaskObject>();
        }

    }

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
            //Debug.Log("capte player");
            PlayerMovement player = collision.GetComponent<PlayerMovement>();

            if (player != null)
            {
                player.SetCurrentInteractable(this);
            }

            if(promptUI != null && GameManager.totalInteractionsCount < 1)
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
        if (GameManager.totalInteractionsCount < 1)
        {
            GameManager.totalInteractionsCount++;
        }

        if (promptUI != null)
        {
            promptUI.SetActive(false);
        }
    }
}