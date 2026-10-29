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

    [Header("[GENERAL SETTINGS]")]
    [SerializeField] private GoalVariants[] _variants;

    [SerializeField] private Transform _Uilocation; 



    private void Start()
    {
        if (_variants.Length <= 0) { Debug.Log("Goal Variants aren't assigned"); }

        if (_candleGoalsNb >= 1) { };
        if (_flowerGoalsNb >= 1) { }
        ;
        if (_graveGoalsNb >= 1) { }
        ;
        if (_leafGoalsNb >= 1) { }
        ;

    }

    private void CreateGoal(GoalVariants goal)
    {
        //var goal = Instantiate(_goalPrefabs, _UIlocation);
        //Goal goalSettings = goal.GetComponent<Goal>();
        //goalSettings.Initialize(actionsToCheck);

    }


}
