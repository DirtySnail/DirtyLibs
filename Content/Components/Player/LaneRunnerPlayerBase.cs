using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerPositionType { Left, Center, Right}

public class LaneRunnerPlayerBase : PlayerBase
{
    [SerializeField] protected List<PlayerPosition> _playerPositionsList = new List<PlayerPosition>();
    [SerializeField] protected TouchInput _touchInput;
    [SerializeField] protected float _laneChangeSpeed;

    protected PlayerPosition _currentPlayerPosition;

    protected virtual void OnEnable()
    {
        _touchInput.SwipeEvent.AddListener(ChangeDirection);
    }

    protected virtual void OnDisable()
    {
        _touchInput.SwipeEvent.RemoveListener(ChangeDirection);
    }

    protected virtual void SetPlayerPosition(PlayerPositionType type)
    {
        PlayerPosition positionToSet = _playerPositionsList.Find(x => x.Type == type);
        _currentPlayerPosition = positionToSet;
    }

    protected virtual void Update()
    {
        if (_currentPlayerPosition == null)
            return;

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, _currentPlayerPosition.Location.localPosition, _laneChangeSpeed * Time.deltaTime);
    }

    protected virtual void ChangeDirection(SwipeType swipeType)
    {
        if (_currentPlayerPosition == null)
            SetPlayerPosition(PlayerPositionType.Center);

        if(swipeType == SwipeType.Left && _currentPlayerPosition.Type != PlayerPositionType.Left)
        {
            if (_currentPlayerPosition.Type == PlayerPositionType.Center)
                SetPlayerPosition(PlayerPositionType.Left);
            else
                SetPlayerPosition(PlayerPositionType.Center);
        }
        else if(swipeType == SwipeType.Right && _currentPlayerPosition.Type != PlayerPositionType.Right)
        {
            if (_currentPlayerPosition.Type == PlayerPositionType.Center)
                SetPlayerPosition(PlayerPositionType.Right);
            else
                SetPlayerPosition(PlayerPositionType.Center);
        }
    }
}

[System.Serializable]
public class PlayerPosition
{
    [field: SerializeField] public PlayerPositionType Type { get; private set; }
    [field: SerializeField] public Transform Location { get; private set; }
}

