using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    private AsteroidCreator asteroidCreator;
   
    // Start is called before the first frame update
    void Start()
    {
        asteroidCreator = FindObjectOfType<AsteroidCreator>();
    }

    private void OnBecameInvisible()
    {
        // Return this asteroid to the pool when it's no longer visible
        asteroidCreator.ReturnObjectToPool(this.gameObject);

    }
}
