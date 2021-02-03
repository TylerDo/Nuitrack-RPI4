using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 This script will contain all the information about our user segment.
 */

public class UserSegment : MonoBehaviour
{
    /*
   User Segment behavior
   */

    string msg = "";

    //Array which stands for the colors used for colorizing the users,
    [SerializeField]
    Color32[] colorsList;

    //Add Colliders field for passing the image width and height
    [SerializeField]
    Colliders collider;

    //Add Object field for passing prefabs
    [SerializeField]
    Objects objectSpawner;

    //Rect field, which stands for a rectangular used for framing the sprite in the image
    Rect imageRect;

    //Image field, which stands for the image displayed on the canvas
    [SerializeField]
    Image segmentOut;

    //Texture2D, which is a texture used for displaying the segment
    Texture2D segmentTexture;

    //Sprite
    Sprite segmentSprite;

    //Byte array for processing the sensor input data
    byte[] dataSegment;

    //cols and rows for displaying the matrix of segments
    int cols = 0;
    int rows = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Update the frame with the user
        NuitrackManager.onUserTrackerUpdate += ColorizeUser;
        //Mirror the image recieved from the sensor using SetMirror
        NuitrackManager.DepthSensor.SetMirror(true);
        //Request the output image parameters from the depth sensor
        nuitrack.OutputMode mode = NuitrackManager.DepthSensor.GetOutputMode();
        cols = mode.XRes;
        rows = mode.YRes;
        //Create the rectangle to define the texture boundaries
        imageRect = new Rect(0, 0, cols, rows);
        //Create segment texture and specify width and height. 
        segmentTexture = new Texture2D(cols, rows, TextureFormat.ARGB32, false);//ARGB32 format for the texture that supports an Alpha channel 1 byte per each channel
        //Create an output segment and specify its size in bytes. Multiply the image size by 4 bc there are 4 channels (ARGB32) in every pixel
        dataSegment = new byte[cols * rows * 4];
        //Set Image type to simple (no stretching, etc) retain image aspect ratio
        segmentOut.type = Image.Type.Simple;
        segmentOut.preserveAspect = true;
        //Colliders method pass the columns and rows
        collider.CreateCollider(cols, rows);
        //Object method to start random spawn
        objectSpawner.StartSpawning(cols);
    }

    /*
    When game ends
     */
    void onDestroy()
    {
        //no null reference will be created
        NuitrackManager.onUserTrackerUpdate -= ColorizeUser;
    }

    /*
    Process the recieved frames and check the presence of the user in front of the sensor.
    Declare the msg variable for displaying 'found' or 'not found'
     */
    void ColorizeUser(nuitrack.UserFrame frame)
    {
        if (frame.Users.Length > 0)
        {
            msg = "User found!";
        }
        else
        {
            msg = "User not found!";
        }

        for (int i = 0; i < (cols * rows); i++)
        {
            Color32 currentColor = colorsList[frame[i]];

            int ptr = i * 4;
            dataSegment[ptr] = currentColor.a;
            dataSegment[ptr + 1] = currentColor.r;
            dataSegment[ptr + 2] = currentColor.g;
            dataSegment[ptr + 3] = currentColor.b;
        }

        //Pass an array for texture filling and apply
        segmentTexture.LoadRawTextureData(dataSegment);
        segmentTexture.Apply();
        //Apply texture to sprite
        segmentSprite = Sprite.Create(segmentTexture, imageRect, Vector3.one * 0.5f, 100f, 0, SpriteMeshType.FullRect);
        //Apply Sprite to Image
        segmentOut.sprite = segmentSprite;
        //Call to Colliders
        collider.UpdateFrame(frame);
    }
    /*
    Characteristics for msg variable
     */
    private void OnGUI()
    {
        //Set values for message
        GUI.color = Color.green;
        GUI.skin.label.fontSize = 50;
        GUILayout.Label(msg);
    }
}
