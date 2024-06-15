using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCreator : MonoBehaviour
{
    public List<GameObject> asteroidPrefabs;
    public int poolSize = 20;
    private List<GameObject> asteroidPool = new List<GameObject>(); 
    public GameObject player;
    public float asteroidGapYmin;
    public float asteroidGapYmax;
    public float asteroidSizePctOfScreenMin;
    public float asteroidSizePctOfScreenMax;
    private float newX;
    private float newY;
    public CameraController camContr;
    private CameraController.CameraBounds bounds;
    public Vector3 latestAsteroidPos;
    private Camera mainCamera;
    private float screenWidth;
    private float asteroidSize;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;

        bounds = CameraController.GetCameraBounds(camContr.GetComponent<Camera>());
        screenWidth = bounds.TopRight.x - bounds.BottomLeft.x;
          // Calculate the size of the asteroid as a percentage of screen width
        
        // Initialize the object pool
        for (int i = 0; i < poolSize; i++)
        {
            GameObject asteroid = Instantiate(asteroidPrefabs[i]);
            asteroid.SetActive(false);
            asteroidPool.Add(asteroid);
        }

        //SpawnObject(new Vector3(0,5,0));
    }

    // Update is called once per frame
    void Update()
    {
        CreateAsteroid();
    }

    public void CreateAsteroid()
    {
        // only spawn asteroids if player is close enough
        if (DistanceY(latestAsteroidPos, player.transform.position) > 3f)
        {
            return;
        }
        
        //  spawn another asteroid
        bounds = CameraController.GetCameraBounds(camContr.GetComponent<Camera>());
        //float screenWidth = bounds.TopRight.x - bounds.BottomLeft.x;
        //float asteroidSize = screenWidth * asteroidSizePercentageOfScreenWidth;  // Calculate the size of the asteroid as a percentage of screen width

        newX = RandomFloatBetween(bounds.BottomLeft.x + asteroidSize/2, bounds.BottomRight.x - asteroidSize/2);  // Subtract/add half the asteroid size from the bounds
        newY = latestAsteroidPos.y + RandomFloatBetween(asteroidGapYmin,asteroidGapYmax);

        GameObject asteroid = SpawnObject(new Vector3(newX,newY,0));
        //asteroid.transform.localScale = Vector3.one * asteroidSize / asteroid.GetComponent<Renderer>().bounds.size.x; // resize while maintaining aspect ratio
    }

    public static float DistanceY(Vector3 pos1, Vector3 pos2)
    {
        return Mathf.Abs(pos1.y - pos2.y);
    }

    public static float RandomFloatBetween(float min, float max)
    {
        return Random.Range(min, max);
    }

    // Call this method to spawn the object at a specific position
    public GameObject SpawnObject(Vector3 position)
    {
        if (asteroidPool.Count == 0) return null; 

        int randomIndex = Random.Range(0, asteroidPool.Count); 
        GameObject asteroid = asteroidPool[randomIndex]; 
        asteroidPool.RemoveAt(randomIndex); 
        asteroid.transform.position = position;
        asteroid.SetActive(true);

        float pctSize = Random.Range(asteroidSizePctOfScreenMin, asteroidSizePctOfScreenMax);
        asteroidSize = screenWidth * pctSize;
        asteroid.transform.localScale = Vector3.one * asteroidSize / asteroid.GetComponent<Renderer>().bounds.size.x;

        latestAsteroidPos = position;
        return asteroid;
    }
    
    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
        asteroidPool.Add(obj); // Add it back to the pool
    }
}
