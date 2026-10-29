using System;
using UnityEngine;

[Serializable]
public class GoalVariants
{
    public string _name;
    public GameObject _prefab;
    public int _actionsToCheck;
}

public class GoalsManager : MonoBehaviour
{
    [Header("[MISSION CONFIG]")]
    [SerializeField] private int _candleGoalsNb;
    [SerializeField] private int _flowerGoalsNb;
    [SerializeField] private int _graveGoalsNb;
    [SerializeField] private int _leafGoalsNb;
    [SerializeField] private int _cutGoalsNb;
    [SerializeField] private int _grassGoalsNb;
    [SerializeField] private int _lightGoalsNb;

    [Header("[GENERAL SETTINGS]")]
    [SerializeField] private GoalVariants[] _variants;

    [SerializeField] private Transform _Uilocation; 


    private void Start()
    {
        if (_variants.Length <= 0) { Debug.Log("Goal Variants aren't assigned"); }

        if (_candleGoalsNb >= 1) { CreateGoal(_variants[0], _candleGoalsNb); }
        if (_flowerGoalsNb >= 1) { CreateGoal(_variants[1], _flowerGoalsNb); };
        if (_graveGoalsNb >= 1) { CreateGoal(_variants[2], _graveGoalsNb); };
        if (_leafGoalsNb >= 1) { CreateGoal(_variants[3], _leafGoalsNb); };
        if (_cutGoalsNb >= 1) { CreateGoal(_variants[4], _cutGoalsNb); };
        if (_grassGoalsNb >= 1) { CreateGoal(_variants[5], _grassGoalsNb); };
        if (_lightGoalsNb >= 1) { CreateGoal(_variants[6], _lightGoalsNb); };

    }

    private void CreateGoal(GoalVariants goal, int nb)
    {
        for (int i = 0; i < nb; i++)
        {
            var newGoal = Instantiate(goal._prefab, _Uilocation);
            Goal goalSettings = newGoal.GetComponent<Goal>();
            goalSettings.Initialize(goal._actionsToCheck);
        }
    }
}
