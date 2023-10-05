using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private Vector2 mousePos;
    private Vector2 targetPos;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetMousePos();
        }

        // transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }

    private void SetMousePos()
    {
        mousePos = Input.mousePosition;
        targetPos = Camera.main.ScreenToWorldPoint(mousePos);
    }
}
