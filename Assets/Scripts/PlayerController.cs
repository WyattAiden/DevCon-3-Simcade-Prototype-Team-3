using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTiltController : MonoBehaviour
{
    [Header("Tilt Settings")]
    [Tooltip("Maximum tilt angle in degrees on each axis.")]
    public float maxTiltAngle = 15f;

    [Tooltip("How fast the board rotates toward the target tilt.")]
    public float tiltSpeed = 5f;

    [Header("Input Axis Names")]
    [Tooltip("Horizontal input axis (default: Horizontal).")]
    public string horizontalAxis = "Horizontal";

    [Tooltip("Vertical input axis (default: Vertical).")]
    public string verticalAxis = "Vertical";

    // Original rotation to return to when no input
    private Quaternion initialRotation;

    private void Start()
    {
        initialRotation = transform.localRotation;
    }

    private void Update()
    {
        // Read input (-1 to 1)
        float xInput = Input.GetAxisRaw(horizontalAxis); // A/D or Left/Right
        float yInput = Input.GetAxisRaw(verticalAxis);   // W/S or Up/Down

        // This vector lets us automatically support 8 directions:
        // (0,1) N, (0,-1) S, (1,0) E, (-1,0) W and diagonals like (1,1), (-1,1), etc.
        Vector2 inputDir = new Vector2(xInput, yInput);

        Quaternion targetRotation;

        if (inputDir.sqrMagnitude > 0.01f)
        {
            // Optional: normalize so diagonal tilt isn't stronger than cardinal tilt
            inputDir = inputDir.normalized;

            // Map vertical input to tilt around local X, horizontal input to tilt around local Z.
            // Positive yInput tilts the board "forward" (ball rolls forward).
            // Positive xInput tilts the board to the right.
            float tiltX = -inputDir.y * maxTiltAngle; // negative so W/Up makes board tip forward
            float tiltZ = inputDir.x * maxTiltAngle;

            // Apply tilt relative to the initial rotation
            targetRotation = initialRotation * Quaternion.Euler(tiltX, 0f, tiltZ);
        }
        else
        {
            // No input => smoothly return to flat/original rotation
            targetRotation = initialRotation;
        }

        // Smoothly rotate board toward targetRotation
        transform.localRotation = Quaternion.Lerp(
            transform.localRotation,
            targetRotation,
            Time.deltaTime * tiltSpeed
        );
    }
}