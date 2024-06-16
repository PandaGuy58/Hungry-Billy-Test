using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderCollider : MonoBehaviour
{
    public PlayerController playerController;
    bool damageTaken = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Obstacle"))
        {
            playerController.HealthDecrement();
            damageTaken = true;
        }

        if(other.CompareTag("ObstacleWall"))
        {
            playerController.moveAllowed = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ObstacleWall"))
        {
            playerController.moveAllowed = true;
            playerController.WallIncrement();

            if(damageTaken)
            {
                damageTaken = false;
            }
            else
            {
                playerController.PointIncrement();
            }
        }
    }
}
