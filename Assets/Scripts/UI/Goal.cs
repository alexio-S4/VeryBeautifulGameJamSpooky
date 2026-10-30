using System;
using UnityEngine;
using UnityEngine.UI;

public enum GoalPhase { None, Pause, InProgress, Checked }
public enum GoalType { None, Candle, Flower, Grave, Leaf, Cut, Grass, Light }

public class Goal : MonoBehaviour
{
    public GoalType _type = GoalType.None;
    public GoalPhase _phase = GoalPhase.None;
    public Sprite[] _spritesBank = new Sprite[4];
    public int _actionsChecked;
    public int _actionsToCheck;

    /*[SerializeField] */private Image _image;

    public Action<GoalPhase> OnPhaseChange;

    public GoalPhase Phase
    {
        get => _phase;
        set
        {
            if (_phase == value) return;
            _phase = value;
            OnPhaseChange?.Invoke(_phase);
        }
    }

    public int ActionsToCheck => _actionsToCheck;

    private void Awake()
    {
        if (_image == null) { _image = GetComponentInChildren<Image>(); }
    }

    private void OnEnable()
    {
        OnPhaseChange += UpdateGoalSprite;
    }

    private void OnDisable()
    {
        OnPhaseChange -= UpdateGoalSprite;
    }

    private void Update()
    {
    }

    /// Only to show in inspector with enum
    private GoalPhase _lastPhase;

    private void OnValidate()
    {
        if (!Application.isPlaying) return;
        if (_phase != _lastPhase)
        {
            _lastPhase = _phase;
            OnPhaseChange?.Invoke(_phase);
        }
    }

    ///

    public void Initialize(int actionsToCheck)
    {
        //Debug.Log($"Try Initialize");
        Phase = GoalPhase.None;
        _actionsToCheck = actionsToCheck;
    }

    private void UpdateGoalSprite(GoalPhase phase)
    {
        //Debug.Log("Try Update Sprite");

        switch (phase)
        {
            case GoalPhase.None:
                _image.sprite = _spritesBank[0];
                break;
            case GoalPhase.Pause:
                _image.sprite = _spritesBank[1];
                break;
            case GoalPhase.InProgress:
                _image.sprite = _spritesBank[2];
                break;
            case GoalPhase.Checked:
                _image.sprite = _spritesBank[3];
                break;
        }
    }
}
