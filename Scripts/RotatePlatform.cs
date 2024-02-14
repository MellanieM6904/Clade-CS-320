using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlatform : MonoBehaviour
{
    PlayerControls playerControls;

    public float rAmount = 1; // how much to rotate w mouse movement
    public float rSpeed = 5; // how fast to rotate w mouse movement

    Vector2 cameraInput;

    Vector3 currentR;
    Vector3 targetR;

    private void OnEnable() {
        if (playerControls == null) {
            playerControls = new PlayerControls();
            playerControls.PlayerMovement.Camera.performed += ctx => cameraInput = ctx.ReadValue<Vector2>();
        }
        playerControls.Enable();
    }

    private void Start() {
        currentR = transform.eulerAngles;
        targetR = transform.eulerAngles;
    }

    private void Update() {
        if (cameraInput.x > 0) { // right movement
            targetR.y = targetR.y + rAmount;
        } else if (cameraInput.x < 0) { // left movement
            targetR.y = targetR.y - rAmount;
        }

        currentR = Vector3.Lerp(currentR, targetR, rSpeed*Time.deltaTime);
        transform.eulerAngles = currentR;
    }
}

