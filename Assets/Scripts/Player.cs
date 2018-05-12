using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    // Movement related variables
    [Tooltip("In meters per second")][SerializeField] float speed = 10f;
    [Tooltip("In meters")][SerializeField] float xRange = 5f;
    [Tooltip("In meters")][SerializeField] float yRange = 5f;

    // Controls the Pitch of the ship when moving up/down so as to always be shooting forward
    [Tooltip("How much pitch when moving up/down")][SerializeField] float posPitchFactor = -6f;
    // Controls the pitch of the ship temp when button is pushed so it looks like it's
    // going up/down on the nose when moving up/down
    [SerializeField] float controlPitchFactor = -30f;
    [SerializeField] float posYawFactor = 6f;
    [SerializeField] float controlRollFactor = -30f;

    // To move ship around
    float xThrow, yThrow;

    // Use this for initialization
    void Start()
    {
		
	}
	
	// Update is called once per frame
	void Update()
    {
        HandleMovement();
    }

    /// <summary>
    /// Handles movement related functions
    /// </summary>
    private void HandleMovement()
    {
        ProcessRotation();
        ProcessTranslation();
    }

    #region Movement
    /// <summary>
    /// Changes the rotation of the ship during movement
    /// </summary>
    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * posPitchFactor;
        float pitchDueToControl = yThrow * controlPitchFactor;
        float pitch =  pitchDueToPosition + pitchDueToControl;

        
        float yaw = transform.localPosition.x * posYawFactor;
        float roll = xThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    /// <summary>
    /// Moves the ship
    /// </summary>
    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        // works out how many meters needed to move in this current frame
        float xOffset = xThrow * speed * Time.deltaTime;
        float yOffset = yThrow * speed * Time.deltaTime;
        float rawXPos = transform.localPosition.x + xOffset;
        float rawYPos = transform.localPosition.y + yOffset;

        //Clamps the ship from moving too far and going off camera
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }
    #endregion
}
