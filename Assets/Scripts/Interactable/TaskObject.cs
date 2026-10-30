using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))] 
public class TaskObject : MonoBehaviour
{
    [Header("[SETTINGS]")]
    [SerializeField] private PlayerMovement _playerRef;

    [Header("Paramètres de la tâche")]
    public GoalType taskType = GoalType.None; 

    [Header("Skins")]
    public Sprite dirtySprite; 
    public Sprite cleanSprite; 

    private SpriteRenderer _spriteRenderer;
    private bool _isComplete = false;

    private InteractUI _UIWheelEvent;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = dirtySprite; 

        if (_playerRef == null) { Debug.LogWarning("ADD PLAYER REF TO TASK "); }
        if (_UIWheelEvent == null) { _UIWheelEvent = _playerRef.GetComponentInChildren<InteractUI>(); }

    }

    //private void OnEnable()
    //{
    //    _UIWheelEvent.OnWheelComplete += CompleteTask;
    //}
    //private void OnDisable()
    //{
    //    _UIWheelEvent.OnWheelComplete -= CompleteTask;
    //}

    public void CompleteTask()
    {
        Debug.Log("Try Complete Task");
        if (_isComplete) return;
        //Debug.Log("Task not Complete");

        if (_playerRef.CurrentInteractable.Task != this) return;


        _isComplete = true;
        _spriteRenderer.sprite = cleanSprite; 

        // Prévient le GoalsManager que la tâche est accomplie
        if (GoalsManager.Instance != null)
        {
            Debug.Log("Complete Task");

            GoalsManager.Instance.AddProgress(_playerRef.CurrentInteractable.Task.taskType);

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