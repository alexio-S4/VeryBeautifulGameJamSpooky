using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(InteractUI))]
public class UIBARcopy : MonoBehaviour
{
    [SerializeField] private PlayerMovement _player;
    private InteractUI _interactUI;
    private InteractUI _UIWheelEvent;

    private InteractableObject _currentInteractable;

    [SerializeField] private float _progressBarTimer = 2f;
    [SerializeField] private float _fillSpeed = 2f;

    private float _displayedValue;
    private float _targetValue;
    private float _hideTimer;
    private bool _isChecked;

    private Goal _currentGoal;
    public bool IsChecked => _isChecked;


    private void Awake()
    {
        if (_interactUI == null) { _interactUI = GetComponent<InteractUI>(); }
        if (_UIWheelEvent == null) { _UIWheelEvent = _player.GetComponentInChildren<InteractUI>(); }

    }

    private void Update()
    {

        UpdateProgressBar();
    }
    private void OnEnable()
    {
        if (GoalsManager.Instance != null)
            GoalsManager.Instance.OnTaskComplete+= ActiveProgressBarAnimation;
    }

    private void Start()
    {
        if (GoalsManager.Instance != null)
        {
            GoalsManager.Instance.OnTaskComplete -= ActiveProgressBarAnimation;
            GoalsManager.Instance.OnTaskComplete += ActiveProgressBarAnimation;
        }
    }

    private void OnDisable()
    {
        if (GoalsManager.Instance != null)
            GoalsManager.Instance.OnTaskComplete -= ActiveProgressBarAnimation;
    }

    public void ActiveProgressBarAnimation(Goal goal)
    {
        _currentGoal = goal;

        _isChecked = true;
        _targetValue = (float)_currentGoal._actionsChecked / _currentGoal._actionsToCheck;
        _hideTimer = 0f;
        _interactUI.Show();
    }

    private void UpdateProgressBar()
    {
        if (!_isChecked) return;

        //Debug.Log("Try Update Progress Bar;");

        _displayedValue = Mathf.MoveTowards(_displayedValue, _targetValue, _fillSpeed * Time.deltaTime);
        _interactUI.Progress(_displayedValue);

        // Une fois la cible atteinte, on attend un peu puis on cache
        if (Mathf.Approximately(_displayedValue, _targetValue))
        {
            _hideTimer += Time.deltaTime;
            if (_hideTimer >= _progressBarTimer)
            {
                _isChecked = false;
                _interactUI.Hide();
            }
        }
    }

    public void ProgressBar(Goal goal)
    {
        _interactUI.Show();
        float value = (float)goal._actionsChecked / goal._actionsToCheck;

        float clamped = Mathf.Clamp01(value);
        _interactUI._images[1].fillAmount = clamped;

    }

}
