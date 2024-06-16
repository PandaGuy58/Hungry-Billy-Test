using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGeneration : MonoBehaviour
{                                 // generation values
    float nextZValue = 0;               // next Z spawn location
    float zValueIncrements = 20;
    int totalWallsInScene = 5;          // number of walls in scene (can be increased if spawn is visible) (might break the code)

    public GameObject obstacleWallPrefab;       // prefab to instantiate walls into scene
    int totalNumberOfGaps = 9;                      // gaps in the wall (decreases over time for higher difficulty)

    public List<Color> availableCubeColours;        // allows generation to select different colour for walls
    int colourIndex = 0;

    public int wallsPerDifficulty = 5;              // how often game difficulty increments
    int currentWallsSpawned = 0;
    public int currentDifficulty = 1;


    void Start()
    {
        InitialGeneration();
    }

    void InitialGeneration()
    {
        Vector3 targetCoordinate = Vector3.zero;
        for (int i = 0; i < totalWallsInScene; i++)     // for loop based on number of walls in scene
        {
            targetCoordinate.z = nextZValue;             // calculate wall location
            GameObject newWall = Instantiate(obstacleWallPrefab, targetCoordinate, Quaternion.identity);  // instantiate wall

            ObstacleWall obstacleWall = newWall.GetComponent<ObstacleWall>();       // get wall component and initialise values
            obstacleWall.InitialiseWall(this);
            obstacleWall.InitialiseCubes(totalNumberOfGaps, availableCubeColours[colourIndex]);

            nextZValue += zValueIncrements;     // calculate next z location of wall
            colourIndex += 1;                       // increments
            currentWallsSpawned += 1;

        }
    }

    public void SpawnNextWall(ObstacleWall targetWall)      // upon passing through wall  >  wall is reused and placed in next location
    {
        if (currentWallsSpawned == wallsPerDifficulty && currentDifficulty < 5)     // check if difficulty is increased
        {
            currentWallsSpawned = 0;                    // increment difficulty values
            totalNumberOfGaps -= 2;
            currentDifficulty += 1;
        }

        Vector3 targetCoordinate = Vector3.zero;            // calculate next wall locations
        targetCoordinate.z = nextZValue;
        targetWall.gameObject.transform.position = targetCoordinate;        // change location of the wall
        targetWall.InitialiseCubes(totalNumberOfGaps, availableCubeColours[colourIndex]);       // initialise values

        nextZValue += zValueIncrements;     // next z location
        colourIndex += 1;        

        if(colourIndex > availableCubeColours.Count -1)     // once colour index is maxed  >  reset
        {
            colourIndex = 0;
        }

        currentWallsSpawned += 1;

    }
}
