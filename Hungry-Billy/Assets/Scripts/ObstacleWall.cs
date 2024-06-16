using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleWall : MonoBehaviour
{
    public List<ObstacleCube> cubes;
    public ObstacleGeneration obstacleGeneration;


    public void InitialiseWall(ObstacleGeneration obstacleGeneration)
    {
        this.obstacleGeneration = obstacleGeneration;

        for (int i = 0; i < cubes.Count; i++)
        {
            cubes[i].Initialise();
        }
    }

    public void InitialiseCubes(int disabledCubes, Color cubeColor)
    {
        List<ObstacleCube> tempCubes = new (cubes);
        for(int i = 0; i < cubes.Count; i++)
        {
            cubes[i].InitialiseColour(cubeColor);
            cubes[i].gameObject.SetActive(true);
        }
        
        for(int i = 0; i < disabledCubes; i++)
        {
            int targetIndex = Random.Range(0, tempCubes.Count);
            tempCubes[targetIndex].gameObject.SetActive(false);
            tempCubes.RemoveAt(targetIndex);
        }

    }


    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            obstacleGeneration.SpawnNextWall(this);
        }
    }
}
