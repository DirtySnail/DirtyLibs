using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PositionChanger : MonoBehaviour
{
    public UnityEvent OnPositionReached;

    [SerializeField] private bool _startActivated = true;
    [SerializeField] private bool _startInRandomPositionInRange;
    [SerializeField] private bool _isLooping;
    [SerializeField] private bool _resetOnFinish;
    [SerializeField] private bool _lookAtY;

    [SerializeField] private Transform _startingLocation;
    [SerializeField] private Transform _endLocation;
    [SerializeField] private MinMaxFloat _moveSpeedRange;
    [SerializeField] private MinMaxFloat _restDurationRange;

    private Transform _target;

    private bool _isActive;
    private float _currentMoveSpeed;
    private float _currentRestDuration;

    public void SetData(Transform startLocation, Transform endLocation)
    {
        _startingLocation = startLocation;
        _endLocation = endLocation;
    }

    private void Start()
    {
        if (_startInRandomPositionInRange)
        {
            transform.position = Helpers.GetRandomVector3Between(_startingLocation.position, _endLocation.position);

            if (_resetOnFinish)
            {
                _target = _endLocation;
            }
            else
            {
                _target = Random.value > 0.5f ? _endLocation : _startingLocation;
            }
        }
        else
        {
            _target = _endLocation;
        }

        if (_lookAtY)
        {
            transform.LookAtY(_target);
        }

        _currentMoveSpeed = _moveSpeedRange.GetRandomValue();

        if (_startActivated)
            _isActive = true;
    }

    public void SetEnabled(bool value)
    {
        _isActive = value;
    }

    private void Update()
    {
        if (!_isActive)
            return;

        if(_currentRestDuration > 0f)
        {
            _currentRestDuration -= Time.deltaTime;
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, _target.position, _currentMoveSpeed * Time.deltaTime);

        if (_isLooping)
        {
            if (transform.position == _target.position)
            {
                if (transform.position == _startingLocation.position)
                {
                    _target = _endLocation;
                }
                else
                {
                    if (_resetOnFinish)
                    {
                        transform.position = _startingLocation.position;
                    }
                    else
                    {
                        _target = _startingLocation;
                    }
                }

                if (_lookAtY)
                {
                    transform.LookAtY(_target);
                }

                OnPositionReached?.Invoke();

                _currentRestDuration = _restDurationRange.GetRandomValue();
                _currentMoveSpeed = _moveSpeedRange.GetRandomValue();
            }
        }
    }
}
