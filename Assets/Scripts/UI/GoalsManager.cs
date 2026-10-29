using System;
using UnityEngine;

public class GoalConfig
{

}

public class GoalsManager : MonoBehaviour
{
    [SerializeField] private Goal _goals;
    //[SerializeField] private GameObject[] _goalPrefabs;


    [SerializeField] private Transform _UIlocation; 

    private void Start()
    {
        //foreach (var goal in _goals)
        //{
        //    CreateGoal(goal.ActionsToCheck);
        //}
    }

    //private void CreateGoal(int actionsToCheck)
    //{
    //    var goal = Instantiate(_goalPrefabs, _UIlocation);
    //    Goal goalSettings = goal.GetComponent<Goal>();
    //    goalSettings.Initialize(actionsToCheck);

    //}


}
