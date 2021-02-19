using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 This script will contain all the information about Objects Spawning.
 */

public class Objects : MonoBehaviour
{
    /*
   Objects behavior
   */

    //Array of objects
    [SerializeField]
    GameObject[] gameObjects;

    //Specify min and max time interval between objects
    [Range(0.5f, 2f)]
    [SerializeField]
    float minTimeInterval = 1; //Min

    [Range(2f, 4f)]
    [SerializeField]
    float maxTimeInterval = 2; //Max

    //Defines the distance from the center of the image to the edges in width
    float halfWidth;

    public void StartSpawning(float widthImage)
    {
        halfWidth = widthImage / 2;
        StartCoroutine(SpawnObject(0f));
    }

    IEnumerator SpawnObject(float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime); // delay 

        float randX = Random.Range(-halfWidth, halfWidth); // random X position
        Vector3 localSpawnPosition = new Vector3(randX, 0, 0); // position for object spawning 

        GameObject currentObject = Instantiate(gameObjects[Random.Range(0, gameObjects.Length)]); // create a random object from the array

        currentObject.transform.SetParent(gameObject.transform, true); // set a parent 
        currentObject.transform.localPosition = localSpawnPosition; // set a local position

        StartCoroutine(SpawnObject(Random.Range(minTimeInterval, maxTimeInterval))); // restart the coroutine for the next object
    }
    
    
}
