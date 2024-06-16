using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModelController : MonoBehaviour
{
    public int currentHorizontalRotation = 0;
    float currentHorizontalAngle = 0;
    float currentHorizontalAngleTwo = 0;

    public int currentForwardRotation = 0;
    float currentForwardAngle = 0;

    float maxHorizontalRotateValue = 30;
    float maxHorizontalRotateValueTwo = 90;
    float maxForwardRotateValue = 40;

    void Update()
    {
        HorizontalRotation();
        ForwardRotation();
        ApplyRotation();
    }

    void HorizontalRotation()
    {
        if(currentHorizontalRotation == 1)
        {
            currentHorizontalAngle = Mathf.Lerp(currentHorizontalAngle, maxHorizontalRotateValue, Time.deltaTime * 3);
            currentHorizontalAngleTwo = Mathf.Lerp(currentHorizontalAngleTwo, -maxHorizontalRotateValueTwo, Time.deltaTime * 4);
        }
        else if(currentHorizontalRotation == -1)
        {
            currentHorizontalAngle = Mathf.Lerp(currentHorizontalAngle, -maxHorizontalRotateValue, Time.deltaTime * 3);
            currentHorizontalAngleTwo = Mathf.Lerp(currentHorizontalAngleTwo, maxHorizontalRotateValueTwo, Time.deltaTime * 4);
        }
        else
        {
            currentHorizontalAngle = Mathf.Lerp(currentHorizontalAngle, 0, Time.deltaTime * 3);
            currentHorizontalAngleTwo = Mathf.Lerp(currentHorizontalAngleTwo, 0, Time.deltaTime * 4);
        }
    }

    void ForwardRotation()
    {
        if (currentForwardRotation == 1)
        {
            currentForwardAngle = Mathf.Lerp(currentForwardAngle, -maxForwardRotateValue, Time.deltaTime * 3);
        }
        else if (currentForwardRotation == -1)
        {
            currentForwardAngle = Mathf.Lerp(currentForwardAngle, maxForwardRotateValue, Time.deltaTime * 3);
        }
        else
        {
            currentForwardAngle = Mathf.Lerp(currentForwardAngle, 0, Time.deltaTime * 3);
        }

    }

    void ApplyRotation()
    {
        Vector3 calculateRotation = Vector3.zero;
        calculateRotation.y = currentHorizontalAngle;
        calculateRotation.z = currentHorizontalAngleTwo;
        calculateRotation.x = currentForwardAngle;

        transform.eulerAngles = calculateRotation;
    }
}

       