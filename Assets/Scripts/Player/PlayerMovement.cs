using System;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("[SETTINGS]")]
    [SerializeField] private float _movementSpeed = 5f;
    [SerializeField] private float _interactMaxTime = 3f;

    [Header("[SPRITES TYPES]")]
    [SerializeField] private Sprite _spriteFront;
    [SerializeField] private Sprite _spriteBack;
    [SerializeField] private Sprite _spriteSide;

    [Header("TEXT SYSTEM")]
    [SerializeField] private Tombstone _textSystem;

    private Rigidbody2D _rigidbody;
    private Vector2 _moveInputs;
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private InteractableObject _currentInteractable;
    private TaskObject _currentTask;

    private bool _wantToInteract = false;
     private InteractUI _interactUI;
    private float _interactingTime;



    private void Awake()
    {
        if (_rigidbody == null)
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }
        if (_spriteRenderer == null)
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
        if (_interactUI == null) { _interactUI = GetComponentInChildren<InteractUI>(); }

    }

    private void Update()
    {
        SwitchSprite();
        UpdateInteract();

    }

    private void FixedUpdate()
    {
        // On s'assure que le système de texte est assigné, qu'il est actif dans la scène ET qu'on parle
        if (_textSystem != null && _textSystem.isActiveAndEnabled && _textSystem.isTalking)
        {
            _rigidbody.linearVelocity = Vector2.zero;
            return;
        }

        _rigidbody.linearVelocity = _moveInputs.normalized * _movementSpeed;
    }

    /// CALL BACKS

    public void MoveInputCallback(InputAction.CallbackContext context)
    {
        _moveInputs = context.ReadValue<Vector2>();
    }

    public void InteractInputCallback(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (_currentInteractable != null && _textSystem != null && !_textSystem.isTalking)
            {
                Debug.Log("Show Dialogue");
                _textSystem.DisplayText(_currentInteractable.linesToDisplay);
                _currentInteractable.OnInteracted();
            }
        }
    }

    public void EscInputCallback(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (PauseManager.Instance != null)
            {
                PauseManager.Instance.TogglePause();
            }
        }
    }


    /// INTERACTABLE METHODS 
    public void SetCurrentInteractable(InteractableObject interactable)
    {
        _currentInteractable = interactable;
    }

    public void ClearCurrentInteractable()
    {
        _currentInteractable = null;
    }

    private void SwitchSprite()
    {
        // On bloque l'animation de marche pendant le dialogue
        if (_textSystem != null && _textSystem.isActiveAndEnabled && _textSystem.isTalking)
        {
            return;
        }

        if (_moveInputs == Vector2.zero)
        {
            return;
        }

        if (Mathf.Abs(_moveInputs.x) > Mathf.Abs(_moveInputs.y))
        {
            _spriteRenderer.sprite = _spriteSide;
            _spriteRenderer.flipX = _moveInputs.x < 0;
        }
        else
        {
            _spriteRenderer.flipX = false;
            _spriteRenderer.sprite = _moveInputs.y > 0 ? _spriteBack : _spriteFront;
        }
    }

    private void Interact()
    {
        Debug.Log("Try interact");
        // If I can interact with...
        _interactingTime = 0f;
        _interactUI.Progress(0f);
        _interactUI.Show();
    }

    private void UpdateInteract()
    {
        if (!_wantToInteract) return;

        _interactingTime += Time.deltaTime;
        _interactUI.Progress(_interactingTime / _interactMaxTime);

        if (_interactingTime >= _interactMaxTime)
        {
            _wantToInteract = false;
            _interactingTime = 0f;
            _interactUI.Progress(0f);
            _interactUI.Hide();

            if (_currentTask != null)
            {
                _currentTask.CompleteTask();
                _currentTask = null;
            }
        }
    }

    public void SetCurrentTask(TaskObject task) { _currentTask = task; }
    public void ClearCurrentTask() { _currentTask = null; }

    public void TaskInputCallback(InputAction.CallbackContext context)
    {
        if (context.started && _currentTask != null)
        {
            _wantToInteract = true;
            Interact(); // Lance la roue de progression
        }

        if (context.canceled)
        {
            _wantToInteract = false;
            _interactUI.Hide(); // Annule la roue si on relâche F
        }
    }
}
