using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public enum SwipeType { None, Up, Down, Left, Right };

public class TouchInput : MonoBehaviour
{
    [field: SerializeField] public bool SingleTouch { get; private set; }

    public bool IsTouching { get; private set; }
    public Vector2 TouchPosition { get; private set; }

    [HideInInspector] public UnityEvent TouchBeganEvent;
    [HideInInspector] public UnityEvent TouchEndedEvent;
    [HideInInspector] public UnityEvent<SwipeType> SwipeEvent;

    [SerializeField] private float _minSwipeLength = 200f;

    private Vector2 _firstPressPos;
    private Vector2 _secondPressPos;
    private Vector2 _currentSwipe;

    private void Update()
    {
        DetectSwipe();
        DetectTouch();
    }

    private void DetectTouch()
    {

        Touch touch;

        IsTouching = Input.touchCount > 0;

        if (SingleTouch)
        {
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    TouchBeganEvent?.Invoke();
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    TouchEndedEvent?.Invoke();
                }
            }
        }
        else
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                touch = Input.GetTouch(i);

                if (touch.phase == TouchPhase.Began)
                    TouchBeganEvent?.Invoke();
                else if (touch.phase == TouchPhase.Ended)
                    TouchEndedEvent?.Invoke();
            }
        }

        TouchPosition = Input.touchCount > 0 ? Input.GetTouch(0).position : Vector2.zero;
    }

    private void DetectSwipe()
    {
        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);

            if (t.phase == TouchPhase.Began)
            {
                _firstPressPos = new Vector2(t.position.x, t.position.y);
            }

            if (t.phase == TouchPhase.Ended)
            {
                _secondPressPos = new Vector2(t.position.x, t.position.y);
                _currentSwipe = new Vector3(_secondPressPos.x - _firstPressPos.x, _secondPressPos.y - _firstPressPos.y);

                if (_currentSwipe.magnitude < _minSwipeLength)
                    return;

                _currentSwipe.Normalize();

                if (!(_secondPressPos == _firstPressPos))
                {
                    if (Mathf.Abs(_currentSwipe.x) > Mathf.Abs(_currentSwipe.y))
                    {
                        if (_currentSwipe.x < 0)
                        {
                            SwipeEvent?.Invoke(SwipeType.Left);
                        }
                        else
                        {
                            SwipeEvent?.Invoke(SwipeType.Right);
                        }
                    }
                    else
                    {
                        if (_currentSwipe.y < 0)
                        {
                            SwipeEvent?.Invoke(SwipeType.Down);
                        }
                        else
                        {
                            SwipeEvent?.Invoke(SwipeType.Up);
                        }
                    }
                }
            }
        }
    }
}
