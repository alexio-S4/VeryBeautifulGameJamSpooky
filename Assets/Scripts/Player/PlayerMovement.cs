using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 5f;
    private bool _wantToInteract = false;
    private Rigidbody2D _rigidbody;
    private Vector2 _moveInputs;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_wantToInteract) {Debug.Log("Want to interact"); }
    }

    private void FixedUpdate()
    {
        _rigidbody.linearVelocity = _moveInputs.normalized * _movementSpeed;
    }


    public void MoveInputCallback(InputAction.CallbackContext context)
    {
        _moveInputs = context.ReadValue<Vector2>();
    }

    public void InteractInputCallback(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            _wantToInteract = true;

            Debug.Log("Interact");
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            _wantToInteract = false;
        }
    }
}
