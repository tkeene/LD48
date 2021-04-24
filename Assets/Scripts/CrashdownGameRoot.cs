using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashdownGameRoot : MonoBehaviour
{
    public Vector3 defaultCameraOffset = new Vector3(0.0f, 5.0f, 0.0f);
    public float defaultCameraAcceleration = 5.0f;

    private Vector3 currentCameraVelocity = Vector3.zero;

    void Update()
    {
        UpdatePlayers();
        UpdateEnemies();
        UpdateGameLogic();
    }

    private void UpdatePlayers()
    {
        Vector3 cameraAveragedTargetPosition = Vector3.zero;
        int numberOfCameraTargets = 0;

        Vector3 inputRight = Camera.main.transform.right;
        inputRight.y = 0.0f;
        inputRight = inputRight.normalized;
        Vector3 inputUp = Camera.main.transform.up;
        inputUp.y = 0.0f;
        inputUp = inputUp.normalized;

        foreach (CrashdownPlayerController player in CrashdownPlayerController.activePlayerInstances)
        {
            if (!player.IsDead())
            {
                Vector2 input = Vector2.zero;
                if (Input.GetKey(KeyCode.W))
                {
                    input.y += 1.0f;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    input.x -= 1.0f;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    input.y -= 1.0f;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    input.x += 1.0f;
                }
                Vector3 worldspaceInput = inputRight * input.x + inputUp * input.y;

                Vector3 newPosition = player.transform.position + player.GetMaxSpeed() * Time.deltaTime * worldspaceInput;
                player.transform.position = newPosition;

                cameraAveragedTargetPosition += player.transform.position;
                numberOfCameraTargets++;
            }
        }

        // Update the camera after all the players have moved.
        if (numberOfCameraTargets > 0)
        {
            cameraAveragedTargetPosition /= numberOfCameraTargets;
            Vector3 cameraNewPosition = cameraAveragedTargetPosition + defaultCameraOffset;
            cameraNewPosition = Vector3.SmoothDamp(Camera.main.transform.position, cameraNewPosition, ref currentCameraVelocity, 1.0f / defaultCameraAcceleration);
            Camera.main.transform.position = cameraNewPosition;
            Camera.main.transform.LookAt(cameraAveragedTargetPosition, Vector3.forward);
        }
    }

    private void UpdateEnemies()
    {
    }

    private void UpdateGameLogic()
    {
    }

}
