using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(ComponentController))]
public class PlayerAgent : Agent
{
    private float _overpowerBuildUp;
    [SerializeField] private LevelSpawner _levelSpawner;


    public ComponentController ComponentController{ get; private set; }

    public event Action EpisodeBeginAction;

    public bool IsDown{ get; private set; }
    
    

    public override void Initialize()
    {
        ComponentController = GetComponent<ComponentController>();
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            actionsOut.DiscreteActions.Array[0] = 0;
        }
        else
        {
            actionsOut.DiscreteActions.Array[0] = 1;
        }
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        if(actions.DiscreteActions[0] == 0)
        {
            IsDown = true;
        }
        else if(actions.DiscreteActions[0] == 1)
        {
            IsDown = false;
        }
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        
    }
    public override void OnEpisodeBegin()
    {
        EpisodeBeginAction?.Invoke();
        
    }

    // public void EndEpisode()
    // {
    //     _levelSpawner.IncreaseTheLevel();
    //     EndEpisode();
    // }
}
