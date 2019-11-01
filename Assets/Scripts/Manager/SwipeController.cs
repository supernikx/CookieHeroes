using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    private static SwipeController i;

    [SerializeField]
    float swipeResistanceX = 50;
    [SerializeField]
    float swipeResistanceY = 100;

    Direction direction;
    Vector2 touchPosition;

    private void Awake()
    {
        i = this;
    }

    // Update is called once per frame
    void Update()
    {
        direction = Direction.None;
        TouchInput();

#if UNITY_EDITOR
        MouseInput();
#endif
    }

    public static bool IsSwiping(Direction _direction)
    {
        if (i.direction == _direction)
            return true;
        else return false;
    }

    private void MouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Vector2 deltaSwipe = touchPosition - (Vector2)Input.mousePosition;

            if (Mathf.Abs(deltaSwipe.x) > swipeResistanceX)
            {
                direction |= (deltaSwipe.x < 0) ? Direction.Right : Direction.Left;
            }
            if (Mathf.Abs(deltaSwipe.y) > swipeResistanceY)
            {
                direction |= (deltaSwipe.y < 0) ? Direction.Up : Direction.Down;
            }
        }
    }

    private void TouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                Vector2 deltaSwipe = touchPosition - touch.position;

                if (Mathf.Abs(deltaSwipe.x) > swipeResistanceX)
                {
                    direction |= (deltaSwipe.x < 0) ? Direction.Right : Direction.Left;
                }
                if (Mathf.Abs(deltaSwipe.y) > swipeResistanceY)
                {
                    direction |= (deltaSwipe.y < 0) ? Direction.Up : Direction.Down;
                }
            }
        }
    }
}

public enum Direction
{
    None,
    Right,
    Left,
    Up,
    Down
}