using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderCollider : MonoBehaviour
{
    public PlayerController playerController;      // code dedicated to determine collision enter of the model
    bool damageTaken = false;

    private void OnTriggerEnter(Collider other)    // upon collision enter and exit  
    {                                               // >  player script is updated with gathered data
        if (other.CompareTag("Obstacle"))
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
            playerController.moveAllowed = true;        // upon entering the wall > player movement disabled
            playerController.WallIncrement();

            if(damageTaken)
            {
                damageTaken = false;
            }
            else
            {
                playerController.PointIncrement();          // if no collision with obstacle  >  add points
            }
        }
    }
}
