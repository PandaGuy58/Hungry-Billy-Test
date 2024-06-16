using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCube : MonoBehaviour
{
    MeshRenderer meshRend;
    // Start is called before the first frame update
    public void Initialise()
    {
        meshRend = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame

    public void InitialiseColour(Color targetColour)
    {
        meshRend.material.SetColor("_Color",targetColour);
    }
}
