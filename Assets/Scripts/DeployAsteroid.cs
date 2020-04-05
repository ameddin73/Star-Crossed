using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployAsteroid : MonoBehaviour
{
    public Asteroid asteroidPrefab;

    public float respawnTime = 1.0f;
    public int startingCount = 5;
    public float minScale = -0.01f;
    public float maxScale = 0.1f;
    private Vector2 _screenBounds;
    private ShapeMaker shapeMaker;
    
    // Start is called before the first frame update
    void Start()
    {
        shapeMaker = Camera.main.GetComponent<ShapeMaker>();
        
        _screenBounds =
            Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));
        StartCoroutine(AsteroidWave());
        while (startingCount > 0)
        {
            SpawnAsteroid();
            startingCount--;
        }
    }

    private void SpawnAsteroid()
    {
        Asteroid newAsteroid = Instantiate(asteroidPrefab);
        float scale = Random.Range(minScale, maxScale);
        Vector3 scaleChange = new Vector2(scale, scale);
        newAsteroid.transform.localScale -= newAsteroid.transform.localScale - scaleChange;
        
        // Spawn in screen-bounding rectangle
        float xSpawnRange = (float) (_screenBounds.x * 1.5);
        float ySpawnRange = (float) (_screenBounds.y * 1.5);
        newAsteroid.transform.position =
            new Vector2(Position(_screenBounds.x, xSpawnRange), Position(_screenBounds.y, ySpawnRange));
        
        shapeMaker.AddAsteroid(newAsteroid);
    }

    private float Position(float screenBound, float spawnRange)
    {
        return Random.value > 0.5f ? Random.Range(screenBound, spawnRange) : Random.Range(-spawnRange, -screenBound);
    }

    IEnumerator AsteroidWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            SpawnAsteroid();
        }
    }
}
