using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 This script will contain all the information about colliders.
 */

public class Colliders : MonoBehaviour
{
    /*
    Colliders behavior
    */

    //Parent object for colliders
    [SerializeField]
    Transform parentObject;

    //Object that acts as a user pixel
    [SerializeField]
    GameObject userPixelPrefab;

    //Bottom line object
    [SerializeField]
    GameObject bottomLinePrefab;

    //Matrix of colliders (game objects)
    GameObject[,] colliderObjects;

    //cols x rows matrix
    int cols = 0;
    int rows = 0;

    //Collider details
    [Range(0.1f, 1)]
    [SerializeField]
    float colliderDetails = 1f;
    
    /*
    Takes the input data from the sensor. Calculates the new size of colliders
    in accordance with the level of detail of colliders, that we've set(colliderDetails)
    by multiplying the number of columns and rows to the level of detail
    */
    public void CreateCollider(int imageCols, int imageRows)
    {
        cols = (int)(colliderDetails * imageCols);
        rows = (int)(colliderDetails * imageRows);
        //Create array of objects and set its size
        colliderObjects = new GameObject[cols, rows];
        //Scale the size of the matrix of colliders and the image.
        float imageScale = Mathf.Min((float)Screen.width / cols, (float)Screen.height / rows);
        //Fill array with objects
        for (int x = 0; x < cols; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                GameObject currentCollider = Instantiate(userPixelPrefab); //Create an object from userPixel
                currentCollider.transform.SetParent(parentObject, false); //Set parent
                currentCollider.transform.localPosition = new Vector3((cols / 2 - x) * imageScale, (rows / 2 - y) * imageScale, 0); //Update the local position, arrange pixel objects relative to the Image center
                currentCollider.transform.localScale = Vector3.one * imageScale; // set the scale to make it larger
                colliderObjects[x, y] = currentCollider; //Put collider into the matrix of colliders
            }
        }
        //Create a bottom line and set up its characteristics, set parent, define position and scale
        GameObject bottomLine = Instantiate(bottomLinePrefab);
        bottomLine.transform.SetParent(parentObject, false);
        bottomLine.transform.localPosition = new Vector3(0, - (rows / 2) * imageScale, 0);
        bottomLine.transform.localScale = new Vector3(imageScale * cols, imageScale, imageScale); // stretch by the image wid
    }

    /*
    If a user is in the frame, the game objects for displaying are activated
    otherwise they are hidden
    */
    public void UpdateFrame(nuitrack.UserFrame frame) //update the frame
    {
        for (int x = 0; x < cols; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                ushort userID = frame[(int)(x / colliderDetails), (int)(y / colliderDetails)]; //Request a user id according to colliderDetails
                //Not found
                if(userID == 0)
                {
                    colliderObjects[x, y].SetActive(false);
                }
                //Found
                else
                {
                    colliderObjects[x, y].SetActive(true);
                }
            }
        }
    }

}
