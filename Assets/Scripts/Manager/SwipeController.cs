using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    private static SwipeController i;

    [Header("General Settings")]
    [SerializeField]
    float desktopSwipeResistanceX = 50;
    [SerializeField]
    float handledSwipeResistanceX = 200;

    [Header("Feedback Settings")]
    [SerializeField]
    private GenericSoundController leftSwipSoundCtrl;
    [SerializeField]
    private GenericSoundController rightSwipeSoundCtrl;

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

    public static void LeftSwipe()
    {
        i.leftSwipSoundCtrl.PlayClip();
    }

    public static void RightSwipe()
    {
        i.rightSwipeSoundCtrl.PlayClip();
    }

    private void MouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            float xValue = touchPosition.x - Input.mousePosition.x;

            if (Mathf.Abs(xValue) > desktopSwipeResistanceX)
            {
                direction = (xValue < 0) ? Direction.Right : Direction.Left;
            }
        }
    }

    private void TouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Stationary)
            {
                touchPosition = touch.position;
            }
            
            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Moved)
            {
                float xValue = touchPosition.x - touch.position.x;

                if (Mathf.Abs(xValue) > handledSwipeResistanceX)
                {
                    direction = (xValue < 0) ? Direction.Right : Direction.Left;
                    touchPosition = touch.position;
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
}