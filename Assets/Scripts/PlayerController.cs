using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

    // Movement related variables
    [Header("General")]
    [Tooltip("In meters per second")][SerializeField] float controlSpeed = 10f;
    [Tooltip("In meters")][SerializeField] float xRange = 5f;
    [Tooltip("In meters")][SerializeField] float yRange = 5f;
    [SerializeField] GameObject[] guns;

    [Header("Screen-position based")]
    [Tooltip("How much pitch when moving up/down")] [SerializeField] float posPitchFactor = -6f;
    [SerializeField] float posYawFactor = 6f;

    [Header("Control-throw based")]
    [SerializeField] float controlPitchFactor = -30f;
    [SerializeField] float controlRollFactor = -30f;

    // To move ship around
    float xThrow, yThrow;
    bool isControlsEnabled = true;
	
	// Update is called once per frame
	void Update()
    {
        if (isControlsEnabled)
        {
            HandleMovement();
            ProcessFiring();
        }
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
        float xOffset = xThrow * controlSpeed * Time.deltaTime;
        float yOffset = yThrow * controlSpeed * Time.deltaTime;
        float rawXPos = transform.localPosition.x + xOffset;
        float rawYPos = transform.localPosition.y + yOffset;

        //Clamps the ship from moving too far and going off camera
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }
    #endregion

    void OnPlayerDeath() // Called via String Reference
    {
        isControlsEnabled = false;
        print("Controls frozen");
    }

    #region HandleFiring
    void ProcessFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire")) // Fire is Space or Left Mouse Button
        {
            ActivateGuns();
        }
        else
        {
            DeactivateGuns();
        }
    }

    private void ActivateGuns()
    {
        foreach(GameObject gun in guns)
        {
            gun.SetActive(true);
        }
    }

    private void DeactivateGuns()
    {
        foreach (GameObject gun in guns)
        {
            gun.SetActive(false);
        }
    }
    #endregion
}
