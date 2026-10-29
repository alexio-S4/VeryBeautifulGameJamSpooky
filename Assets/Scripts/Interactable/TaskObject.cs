using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))] 
public class TaskObject : MonoBehaviour
{
    [Header("Paramètres de la tâche")]
    public GoalType taskType = GoalType.Grave; 

    [Header("Skins")]
    public Sprite dirtySprite; 
    public Sprite cleanSprite; 

    private SpriteRenderer _spriteRenderer;
    private bool _isCleaned = false;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = dirtySprite; 
    }

    public void CompleteTask()
    {
        if (_isCleaned) return;

        _isCleaned = true;
        _spriteRenderer.sprite = cleanSprite; 

        // Prévient le GoalsManager que la tâche est accomplie
        if (GoalsManager.Instance != null)
        {
            GoalsManager.Instance.AddProgress(taskType);
        }
    }

    // Détection du joueur spécifique à l'action de nettoyage
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement player = collision.GetComponent<PlayerMovement>();
            if (player != null) player.SetCurrentTask(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement player = collision.GetComponent<PlayerMovement>();
            if (player != null) player.ClearCurrentTask();
        }
    }
}