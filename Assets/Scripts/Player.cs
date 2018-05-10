using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    // Movement related variables
    [Tooltip("In meters per second")][SerializeField] float xSpeed = 4f;
    [Tooltip("In meters per second")][SerializeField] float ySpeed = 4f;
    [Tooltip("In meters")][SerializeField] float xRange = 5f;
    [Tooltip("In meters")][SerializeField] float yRange = 5f;

    // Use this for initialization
    void Start()
    {
		
	}
	
	// Update is called once per frame
	void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        // works out how many meters needed to move in this current frame
        float xOffset = xThrow * xSpeed * Time.deltaTime;
        float yOffset = yThrow * ySpeed * Time.deltaTime;
        float rawXPos = transform.localPosition.x + xOffset;
        float rawYPos = transform.localPosition.y + yOffset;

        //Clamps the ship from moving too far and going off camera
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }
}
