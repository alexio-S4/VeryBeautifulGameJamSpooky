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

    private Rigidbody2D _rigidbody;
    private Vector2 _moveInputs;
    private SpriteRenderer _spriteRenderer;
    private bool _wantToInteract = false;
    private FillBarUI _interactUI;

    private float _interactingTime; 


    private void Awake()
    {
        if (_rigidbody == null)
        { _rigidbody = GetComponent<Rigidbody2D>(); }
        if (_spriteRenderer == null)
        { _spriteRenderer = GetComponentInChildren<SpriteRenderer>(); }

        if(_interactUI == null) { _interactUI = GetComponentInChildren<FillBarUI>();}
    }

    private void Update()
    {
        SwitchSprite();
        UpdateInteract();
    }

    private void FixedUpdate()
    {
        _rigidbody.linearVelocity = _moveInputs.normalized * _movementSpeed;
    }


    /// --- CALLBACKS INPUTS

    public void MoveInputCallback(InputAction.CallbackContext context)
    {
        _moveInputs = context.ReadValue<Vector2>();
    }

    public void InteractInputCallback(InputAction.CallbackContext context)
    {

        if (context.started)
        {
            _wantToInteract = true;
            Debug.Log("Started");
            Interact();
        }

        if (context.canceled)
        {
            Debug.Log("Interact canceled");
            _wantToInteract = false;
            _interactUI.Progress(0f);
        }

    }

    public void EscInputCallback(InputAction.CallbackContext context)
    {
        if ( context.started)
        {
            /// OPEN WINDOW
        }
    }

    ///


    private void SwitchSprite()
    {
        if (_moveInputs == Vector2.zero) return;

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
        }
    }
}
