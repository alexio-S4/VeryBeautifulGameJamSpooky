using System;
using UnityEngine;
using UnityEngine.UI;

public class InteractUI : MonoBehaviour
{

    public Action OnWheelComplete;

    [SerializeField] PlayerMovement _playerRef;

    [SerializeField] public Image[] _images;
    private void Awake()
    {
        if (_images == null)
        { Debug.Log("Add images in inspector"); }
    }

    private void Start() { Hide(); } //Initialize

    public void Show()
    {
        foreach (var img in _images)
        {
            img.gameObject.SetActive(true);
        }
    }

    public void Hide()
    {
        foreach (var img in _images)
        {
            img.gameObject.SetActive(false);
        }
    }


    private bool _isComplete;

    public void Progress(float value)
    {
        float clamped = Mathf.Clamp01(value);
        _images[1].fillAmount = clamped;

        if (clamped >= 1f)
        {
            if (_isComplete) return;   // déjà déclenché
            _isComplete = true;

            // quand la roue est complète
            _playerRef.CurrentInteractable?.Task.CompleteTask(); 
            Debug.Log("Wheel Complete");
        }
        else
        {
            _isComplete = false;       // la roue est repartie, on réarme
        }
    }

    public void ResetWheel()
    {
        _isComplete = false;
        _images[1].fillAmount = 0f;
    }


}

