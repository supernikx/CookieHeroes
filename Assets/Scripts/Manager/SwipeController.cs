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
    [SerializeField]
    float desktopSwipeResistanceY = 100;
    [SerializeField]
    float handledSwipeResistanceY = 100;

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
            Vector2 deltaSwipe = touchPosition - (Vector2)Input.mousePosition;

            if (Mathf.Abs(deltaSwipe.x) > desktopSwipeResistanceX)
            {
                direction = (deltaSwipe.x < 0) ? Direction.Right : Direction.Left;
            }
            if (Mathf.Abs(deltaSwipe.y) > desktopSwipeResistanceY)
            {
                direction = (deltaSwipe.y < 0) ? Direction.Up : Direction.Down;
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
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                Vector2 deltaSwipe = touchPosition - touch.position;

                if (Mathf.Abs(deltaSwipe.x) > handledSwipeResistanceX)
                {
                    direction = (deltaSwipe.x < 0) ? Direction.Right : Direction.Left;
                }
                if (Mathf.Abs(deltaSwipe.y) > handledSwipeResistanceY)
                {
                    direction = (deltaSwipe.y < 0) ? Direction.Up : Direction.Down;
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