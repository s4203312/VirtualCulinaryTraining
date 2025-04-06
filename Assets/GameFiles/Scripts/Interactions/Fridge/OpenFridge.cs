using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenFridge : MonoBehaviour
{
    public void OpenDoor()
    {
        GameObject pivotPoint = transform.GetChild(0).gameObject;
        GameObject door = pivotPoint.transform.GetChild(0).gameObject;
        StartCoroutine(OpeningDoor(pivotPoint, door));
    }

    public IEnumerator OpeningDoor(GameObject pivotPoint, GameObject door)
    {
        float targetAngle = 100;
        float rotatedAngle = 0f;
        float rotationSpeed = 35f;

        while (rotatedAngle < targetAngle)
        {
            float rotationStep = rotationSpeed * Time.deltaTime;
            door.transform.RotateAround(pivotPoint.transform.position, Vector3.up, rotationStep);
            rotatedAngle += rotationStep;
            yield return null;
        }
    }
}
