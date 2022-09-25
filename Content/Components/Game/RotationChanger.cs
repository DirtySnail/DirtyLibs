using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RotationChanger : MonoBehaviour
{
    public UnityEvent OnPositionReached;

    [SerializeField] private bool _isLooping;

    [SerializeField] private Transform _startingLocation;
    [SerializeField] private Transform _endLocation;
    [SerializeField] private MinMaxFloat _rotationSpeedRange;
    [SerializeField] private MinMaxFloat _restDurationRange;

    private Quaternion _targetRotation;

    private float _currentRotationSpeed;
    private float _currentRestDuration;

    private void Start()
    {
        _targetRotation = Random.value > 0.5f ? _startingLocation.rotation : _endLocation.rotation;
        _currentRotationSpeed = _rotationSpeedRange.GetRandomValue();
    }

    private void Update()
    {
        if (_currentRestDuration > 0f)
        {
            _currentRestDuration -= Time.deltaTime;
            return;
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, _currentRotationSpeed * Time.deltaTime);

        if (_isLooping)
        {
            if (transform.rotation == _targetRotation)
            {
                if (transform.rotation == _startingLocation.rotation)
                {
                    _targetRotation = _endLocation.rotation;
                }
                else
                {
                    _targetRotation = _startingLocation.rotation;
                }

                OnPositionReached?.Invoke();

                _currentRestDuration = _restDurationRange.GetRandomValue();
                _currentRotationSpeed = _rotationSpeedRange.GetRandomValue();
            }
        }
    }
}
