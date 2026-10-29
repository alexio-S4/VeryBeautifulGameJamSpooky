using System;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("[SETTINGS]")]
    [SerializeField] private float _movementSpeed = 5f;
    [Header("[SPRITES TYPES]")]
    [SerializeField] private Sprite _spriteFront;
    [SerializeField] private Sprite _spriteBack;
    [SerializeField] private Sprite _spriteSide;

    [Header("TEXT SYSTEM")]
    [SerializeField] private Tombstone _textSystem;

    private Rigidbody2D _rigidbody;
    private Vector2 _moveInputs;
    private SpriteRenderer _spriteRenderer;
    private InteractableObject _currentInteractable;


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
    }

    private void Update()
    {
        SwitchSprite();
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


    public void MoveInputCallback(InputAction.CallbackContext context)
    {
        _moveInputs = context.ReadValue<Vector2>();
    }

    public void InteractInputCallback(InputAction.CallbackContext context)
    {

        if (context.started)
        {
            if(_currentInteractable != null && _textSystem != null && !_textSystem.isTalking)
            {
                _textSystem.DisplayText(_currentInteractable.linesToDisplay);
                _currentInteractable.OnInteracted();
            }
        }

    }

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
}
