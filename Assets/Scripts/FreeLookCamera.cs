using UnityEngine;

public class FreeLookCamera : MonoBehaviour
{
    public float sensitivity; // Adjust the sensitivity of camera rotation
    private Vector2 lastTouchPosition; // Store the last touch position 
    public float maxYAngle = 60f; // Maximum angle to look up
    public float minYAngle = -60f; // Maximum angle to look down
    public RectTransform touchArea; // Reference to the RectTransform of the touch area panel
    private float currentXAngle = 0f;
    private float currentYAngle = 0f;
    public GameObject ccnvas;

    void Start()
    {
        sensitivity = PlayerPrefs.GetFloat("Sensitivity", 1f);

    }

    void Update()
    {if(ccnvas.activeSelf)
        { 
        if (Input.touchCount > 0) // Check if there are touches
        {
            sensitivity = PlayerPrefs.GetFloat("Sensitivity", 1f);
            Touch touch = Input.GetTouch(0);

            // Check if the touch is within the defined touch area
            if (!IsTouchInArea(touch.position))
                return;

            // Ignore touch if it's directly over a UI element
            if (IsTouchOverUI(touch.position))
                return;

            if (touch.phase == TouchPhase.Moved) // Check if the touch has moved
            {
                Vector2 deltaPosition = touch.position - lastTouchPosition; // Calculate the difference in touch positions
                float rotationX = deltaPosition.y * sensitivity; // Calculate rotation around X-axis
                float rotationY = -deltaPosition.x * sensitivity; // Calculate rotation around Y-axis

                currentXAngle -= rotationX;
                currentYAngle -= rotationY;

                // Clamp rotation angles to avoid looking too far up or down
                currentXAngle = Mathf.Clamp(currentXAngle, minYAngle, maxYAngle);

                transform.rotation = Quaternion.Euler(currentXAngle, currentYAngle, 0f);

                lastTouchPosition = touch.position; // Update the last touch position
            }
            else if (touch.phase == TouchPhase.Began) // Check if touch has just begun
            {
                lastTouchPosition = touch.position; // Store the initial touch position
            }
        }
        }
    }

    bool IsTouchInArea(Vector2 touchPosition)
    {
        // Convert touch position to local position of the touch area panel
        Vector2 localPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(touchArea, touchPosition, null, out localPosition);

        // Check if the local position is within the bounds of the touch area panel
        Rect rect = touchArea.rect;
        return rect.Contains(localPosition);
    }

    bool IsTouchOverUI(Vector2 touchPosition)
    {
        // Cast a ray to check if touch position is over a UI element
        UnityEngine.EventSystems.PointerEventData eventDataCurrentPosition = new UnityEngine.EventSystems.PointerEventData(UnityEngine.EventSystems.EventSystem.current);
        eventDataCurrentPosition.position = touchPosition;
        System.Collections.Generic.List<UnityEngine.EventSystems.RaycastResult> results = new System.Collections.Generic.List<UnityEngine.EventSystems.RaycastResult>();
        UnityEngine.EventSystems.EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
