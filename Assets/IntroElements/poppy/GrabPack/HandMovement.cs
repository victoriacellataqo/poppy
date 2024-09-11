using UnityEngine;
using System.Collections;

public class HandMovement : MonoBehaviour
{
    public Transform handObject; // Reference to the hand object
    public Transform targetObject; // Reference to the target object
    public float moveSpeed = 1f; // Speed at which the hand moves
    public Vector3 originalPosition; // Original position of the hand object
    public Vector3 originalScale; // Original scale of the hand object
    private bool isMovingToTarget = false; // Flag to track if the hand is moving towards the target
    private bool isReturning = false; // Flag to track if the hand is returning to original position
    private bool movementCompleted = false; // Flag to track if the movement is completed
    public GameObject danvas, electrici, elecdoor, otherhand;
    public Behaviour behaviour;

    void Start()
    {
        // Store the original position and scale of the hand object
        originalPosition = handObject.position;
        originalScale = handObject.localScale;
    }

    void Update()
    {
        // Check if the hand is moving towards the target or returning to original position
        if (isMovingToTarget && !isReturning)
        {
            otherhand.SetActive(true);
            // Move the hand towards the target object smoothly using Lerp
            handObject.position = Vector3.Lerp(handObject.position, targetObject.position, moveSpeed * Time.deltaTime);

            // Calculate the distance to the target object
            float distanceToTarget = Vector3.Distance(handObject.position, targetObject.position);

            // Calculate the scale factor based on the distance to the target
            float scaleFactor = Mathf.Clamp(originalScale.x + (distanceToTarget * 0.1f), originalScale.x, originalScale.x * 2f);

            // Apply the new scale to the hand object
        handObject.localScale = new Vector3(0, 0, 0);

            // Check if the hand has reached close to the target object
            if (distanceToTarget < 0.01f)
            {
                // If the hand is close to the target, start the delay coroutine
                StartCoroutine(DelayBeforeReturning());
            }
        }
        else if (isReturning && !isMovingToTarget)
        {
            electrici.SetActive(true); elecdoor.SetActive(false);
            // Move the hand back to its original position smoothly using Lerp
            handObject.position = Vector3.Lerp(handObject.position, originalPosition, moveSpeed * Time.deltaTime);

            // Reset the scale of the hand object to its original size
            handObject.localScale = originalScale;

            // Check if the hand has returned close to its original position
            if (Vector3.Distance(handObject.position, originalPosition) < 0.01f)
            {
                // If the hand is close to the original position, movement is completed
                movementCompleted = true;
                behaviour.enabled = false;
                danvas.SetActive(true);
            }
        }
    }

    public void MoveHandToTarget()
    {
        danvas.SetActive(false);
        originalPosition = handObject.position;
        // Set the flag to indicate that the hand should move towards the target
        isMovingToTarget = true;
        isReturning = false;
        movementCompleted = false;
    }

    IEnumerator DelayBeforeReturning()
    {
        
           // Wait for 2 seconds
           yield return new WaitForSeconds(2f);
        otherhand.SetActive(false);
        // Reset the flag and move the hand back to its original position
        isReturning = true;
        isMovingToTarget = false;
    }

    public bool IsMovementCompleted()
    {
        // Check if the movement is completed
        return movementCompleted;
    }
}
