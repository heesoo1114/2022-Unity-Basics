using TMPro;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    PlayerController _plController;

    private Vector2 touchStartPosition;
    private Vector2 touchEndPosition;
    private bool isTouching;

    public int swipeStandard = 50;

    [SerializeField] private TextMeshProUGUI _debuggingText;

    private void Awake()
    {
        _plController = GetComponent<PlayerController>();    
    }

    private void Update()
    {
        SingleTouch();
    }

    private void SingleTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStartPosition = touch.position;
                isTouching = true;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                if (isTouching)
                {
                    touchEndPosition = touch.position;
                    DetectSwipe();
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isTouching = false;
                _debuggingText.text = "not input";
            }
            // else if (touch.phase == TouchPhase.Stationary)
            // {
            //     // _debuggingText.text = "continue touch";
            //     
            //     if (touch.position.x <= 540)
            //     {
            //         _plController.RotationPlane(-1);
            //         _plController.HorizontalMovement(-1);
            //         _debuggingText.text = "left touch input";
            //     }
            //     else
            //     {
            //         _plController.RotationPlane(1);
            //         _plController.HorizontalMovement(1);
            //         _debuggingText.text = "right touch input";
            //     }
            // }
        }
    }

    private void DetectSwipe()
    {
        float swipeDistance = (touchEndPosition - touchStartPosition).magnitude;

        if (swipeDistance > swipeStandard)
        {
            Vector2 swipeDirection = (touchEndPosition - touchStartPosition).normalized;

            if (swipeDirection.x > 0) // 오른쪽 스와이프
            {
                _debuggingText.text = "right swipe";
                _plController.DashMovement(1);
            }
            else if (swipeDirection.x < 0) // 왼쪽 스와이프
            {
                _debuggingText.text = "left swipe";
                _plController.DashMovement(-1);
            }
        }
    }
}
