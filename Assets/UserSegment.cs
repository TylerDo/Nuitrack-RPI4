using UnityEngine;
using UnityEngine.UI;

/*
 This script will contain all the information about our user segment.
 */

public class UserSegment : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Update the frame with the user
        NuitrackManager.onUserTrackerUpdate += ColorizeUser;
    }
    
    /*
    When game ends
     */
    void onDestroy()
    {
        //no null reference will be created
        NuitrackManager.onUserTrackerUpdate -= ColorizeUser;
    }

    string msg = "";

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
    }
    /*
    Characteristics for msg variable
     */
    private void onGUI()
    {
        //Set values for message
        GUI.color = Color.green;
        GUI.skin.label.fontSize = 50;
        GUILayout.Label(msg);
    }
}
