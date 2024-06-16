using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    public List<Image> healthImages;            // UI in the scene
    public TMP_Text scoreText;
    public TMP_Text difficultyText;
    public TMP_Text gameOverText;
    public TMP_Text spaceRestartText;

    public Vector2 targetLocation = new Vector2(-0.5f, -0.5f);  // targetLocation of RB (updated through inputs)
    Rigidbody rb;

    float forwardVelocity = 7.5f;               //initial forward velocity (updated upon next difficulties)
    float horizontalVerticalVelocity = 5;           // how fast rb moves side and up/down

    public bool moveAllowed = true;                 // movement disabled/enabled

    int totalPoints = 0;                            // essential game data
    int totalWallsPassed = 0;

    public PlayerModelController playerModelController;     // allows to instruct rotations

    public ObstacleGeneration obstacleGeneration;           // allows to determine current difficulty

    bool gameOver = false;
    bool maxDifficulty = false;
  //  public Vector3 targetVelocity;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moveAllowed)
        {
            Inputs();
        }

        if(gameOver)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(1);
            }
            
        }

    }

    private void FixedUpdate()
    {
        if(!gameOver)
        {
            CalculateVelocity();
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
        
    }

    void Inputs()                           // player inputs  >  entering updates the target location of rigidbody
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (targetLocation.x != -1.5f)      // increment/decrement value by 1
            {
                targetLocation.x -= 1;              // prevent forbidden values
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))           // and so on...
        {
            if (targetLocation.x != 1.5f)
            {
                targetLocation.x += 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (targetLocation.y != 1.5f)
            {
                targetLocation.y += 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (targetLocation.y != -1.5f)
            {
                targetLocation.y -= 1;
            }
        }
    }

    void CalculateVelocity()
    {
        Vector3 targetVelocity = Vector3.zero;
        Vector2 currentLocation = transform.position;       // check if value is close to the destination 
                                                                    // prevents jittery movement of the player transform
        if (currentLocation.x > targetLocation.x + 0.1f || currentLocation.x < targetLocation.x - 0.1f)   
        {
            if (currentLocation.x > targetLocation.x)
            {
                targetVelocity.x = -1;                                    // depending on the direction of rb
                playerModelController.currentHorizontalRotation = -1;        // > instruct the model to rotate in different directions
            }
            else
            {
                targetVelocity.x = 1;
                playerModelController.currentHorizontalRotation = 1;
            }
        }
        else
        {
            playerModelController.currentHorizontalRotation = 0;            // if location is met  >  neutralise rotation
        }

        if (currentLocation.y > targetLocation.y + 0.1f || currentLocation.y < targetLocation.y - 0.1f)     // and so on...
        {
            if (currentLocation.y > targetLocation.y)
            {
                targetVelocity.y = -1;
                playerModelController.currentForwardRotation = -1;
            }
            else
            {
                targetVelocity.y = 1;
                playerModelController.currentForwardRotation = 1;
            }
        }
        else
        {
            playerModelController.currentForwardRotation = 0;
        }

        targetVelocity = Vector3.Normalize(targetVelocity);     // normalise the direction if moving in 2 directions
        targetVelocity *= horizontalVerticalVelocity;


        targetVelocity.z = forwardVelocity;                         // enable forward movement of rb
        rb.velocity = targetVelocity;
    }

    public void HealthDecrement()                   // dedicated to updating player's health
    {
        healthImages[healthImages.Count-1].gameObject.SetActive(false);     // update image list
        healthImages.RemoveAt(healthImages.Count-1);

        if(healthImages.Count == 0)              // if image list == 0  >  game over
        {
            moveAllowed = false;
            gameOver = true;
            gameOverText.text = "Game Over!";
            spaceRestartText.text = "Space - Restart";
        }
    }

    public void PointIncrement()            // update score
    {
        totalPoints += 1;
        scoreText.text = "Score: " + totalPoints;
    }

    public void WallIncrement()         // upon passing through walls  >  update data
    {
        totalWallsPassed += 1;
        if(totalWallsPassed == obstacleGeneration.wallsPerDifficulty && !maxDifficulty)   // upon reaching next difficulty
        {                                                                                   // update speed of rb
            totalWallsPassed = 0;
            difficultyText.text = "Difficulty: " + obstacleGeneration.currentDifficulty.ToString();
            forwardVelocity += 0.7f;

            if(obstacleGeneration.currentDifficulty == 5)       // prevent speed increasing forever
            {
                maxDifficulty = true;
            }
        }
    }
}



