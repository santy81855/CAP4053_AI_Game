using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnShatterCannon : MonoBehaviour
{
    public GameObject originalObject;
    public GameObject fracturedObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // function that needs to be called whenever the cannon needs to explode
    public void SpawnFracturedObject()
    {
        Destroy (originalObject);
        GameObject fractObj = Instantiate (fracturedObject) as GameObject;
        fractObj.GetComponent<Shatter>().Explode();
    }
}
