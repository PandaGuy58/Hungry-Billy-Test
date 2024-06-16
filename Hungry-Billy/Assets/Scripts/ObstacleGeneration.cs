using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGeneration : MonoBehaviour
{
    float nextZValue = 0;
    float zValueIncrements = 20;
    int totalWallsInScene = 5;

    public GameObject obstacleWallPrefab;
    int totalNumberOfGaps = 9;

    public List<Color> availableCubeColours;
    int colourIndex = 0;

    public int wallsPerDifficulty = 5;
    int currentWallsSpawned = 0;
    public int currentDifficulty = 1;

    public PlayerController playerController;

    void Start()
    {
        InitialGeneration();
    }

    void InitialGeneration()
    {
        Vector3 targetCoordinate = Vector3.zero;
        for (int i = 0; i < totalWallsInScene; i++)
        {
            targetCoordinate.z = nextZValue;
            GameObject newWall = Instantiate(obstacleWallPrefab, targetCoordinate, Quaternion.identity);

            ObstacleWall obstacleWall = newWall.GetComponent<ObstacleWall>();
            obstacleWall.InitialiseWall(this);
            obstacleWall.InitialiseCubes(totalNumberOfGaps, availableCubeColours[colourIndex]);

            nextZValue += zValueIncrements;
            colourIndex += 1;
            currentWallsSpawned += 1;

        }
    }

    public void SpawnNextWall(ObstacleWall targetWall)
    {
        if (currentWallsSpawned == wallsPerDifficulty && currentDifficulty < 6)
        {
            currentWallsSpawned = 0;
            totalNumberOfGaps -= 2;
            currentDifficulty += 1;
        }

        Vector3 targetCoordinate = Vector3.zero;
        targetCoordinate.z = nextZValue;
        targetWall.gameObject.transform.position = targetCoordinate;
        targetWall.InitialiseCubes(totalNumberOfGaps, availableCubeColours[colourIndex]);

        nextZValue += zValueIncrements;
        colourIndex += 1;        

        if(colourIndex > availableCubeColours.Count -1)
        {
            colourIndex = 0;
        }

        currentWallsSpawned += 1;

    }
}
