using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployAsteroid : MonoBehaviour
{
    public GameObject asteroidPrefab;

    public float respawnTime = 1.0f;
    private Vector2 _screenBounds;
    
    // Start is called before the first frame update
    void Start()
    {
        _screenBounds =
            Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        StartCoroutine(AsteroidWave());
    }

    private void SpawnAsteroid()
    {
        GameObject newAsteroid = Instantiate(asteroidPrefab) as GameObject;
        float xSpawnRange = (float) (_screenBounds.x * 1.5);
        float ySpawnRange = (float) (_screenBounds.y * 1.5);
        newAsteroid.transform.position =
            new Vector2(Position(xSpawnRange), Position(ySpawnRange));
        Debug.Log(newAsteroid.transform.position);
    }

    private float Position(float spawnRange)
    {
        return Random.value > 0.5f ? Random.Range(_screenBounds.x, spawnRange) : Random.Range(-spawnRange, 0);
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
