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

    public ComponentController ComponentController{ get; private set; }

    public event Action EpisodeBeginAction;

    public bool IsDown{ get; private set; }
    private Rigidbody _rb;

    private PlayerOverPower _playerOverPower;

    public override void Initialize()
    {
        ComponentController = GetComponent<ComponentController>();
        _playerOverPower = GetComponent<PlayerOverPower>();
        _rb = GetComponent<Rigidbody>();
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        if(Input.GetKey(KeyCode.Space))
        {
            actionsOut.DiscreteActions.Array[0] = 1;
        }
        else
        {
            actionsOut.DiscreteActions.Array[0] = 0;
        }
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        if(actions.DiscreteActions[0] == 0)
        {
            IsDown = false;
        }
        else if(actions.DiscreteActions[0] == 1)
        {
            IsDown = true;
        }

        if(MaxStep > 0)
        {
            AddReward(-1f/MaxStep);
        }
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(_playerOverPower.IsOverPower);
        sensor.AddObservation(_playerOverPower.OverPower);
        sensor.AddObservation(_rb.velocity.y);
    }
    public override void OnEpisodeBegin()
    {
        EpisodeBeginAction?.Invoke();
        
    }
}
