using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    public List<Image> healthImages;
    public TMP_Text scoreText;
    public TMP_Text difficultyText;
    public TMP_Text gameOverText;
    public TMP_Text spaceRestartText;

    public Vector2 targetLocation = new Vector2(-0.5f, -0.5f);
    Rigidbody rb;

    float forwardVelocity = 7.5f;
    float horizontalVerticalVelocity = 5;

    public bool moveAllowed = true;

    int totalPoints = 0;
    int totalWallsPassed = 0;

    public PlayerModelController playerModelController;

    public ObstacleGeneration obstacleGeneration;

    bool gameOver = false;

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
            SceneManager.LoadScene(1);
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

    void Inputs()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (targetLocation.x != -1.5f)
            {
                targetLocation.x -= 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
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
        Vector2 currentLocation = transform.position;

        if (currentLocation.x > targetLocation.x + 0.1f || currentLocation.x < targetLocation.x - 0.1f)
        {
            if (currentLocation.x > targetLocation.x)
            {
                targetVelocity.x = -1;
                playerModelController.currentHorizontalRotation = -1;
            }
            else
            {
                targetVelocity.x = 1;
                playerModelController.currentHorizontalRotation = 1;
            }
        }
        else
        {
            playerModelController.currentHorizontalRotation = 0;
        }

        if (currentLocation.y > targetLocation.y + 0.1f || currentLocation.y < targetLocation.y - 0.1f)
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

        targetVelocity = Vector3.Normalize(targetVelocity);
        targetVelocity *= horizontalVerticalVelocity;


        targetVelocity.z = forwardVelocity;
        rb.velocity = targetVelocity;
    }

    public void HealthDecrement()
    {
        healthImages[healthImages.Count-1].gameObject.SetActive(false);
        healthImages.RemoveAt(healthImages.Count-1);

        if(healthImages.Count == 0)
        {
            moveAllowed = false;
            gameOver = true;
            gameOverText.text = "Game Over!";
            spaceRestartText.text = "Space - Restart";
        }
    }

    public void PointIncrement()
    {
        totalPoints += 1;
        scoreText.text = "Score: " + totalPoints;
    }

    public void WallIncrement()
    {
        totalWallsPassed += 1;
        if(totalWallsPassed == obstacleGeneration.wallsPerDifficulty && obstacleGeneration.currentDifficulty < 6) 
        {
            totalWallsPassed = 0;
            difficultyText.text = "Difficulty: " + obstacleGeneration.currentDifficulty.ToString();
            forwardVelocity += 0.7f;
        }
    }
}



