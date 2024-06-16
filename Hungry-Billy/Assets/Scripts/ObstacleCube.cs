using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCube : MonoBehaviour
{
    MeshRenderer meshRend;  
    public void Initialise()
    {
        meshRend = GetComponent<MeshRenderer>();
    }

    public void InitialiseColour(Color targetColour)
    {
        meshRend.material.SetColor("_Color",targetColour);
    }
}
