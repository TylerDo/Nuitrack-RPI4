using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NativeAvatar : MonoBehaviour{

    //Joints of skeleton
    public nuitrack.JointType[] typeJoint;
    GameObject[] CreatedJoint;
    public GameObject PrefabJoint;

    string message = "";

    // Start is called before the first frame update
    void Start(){

        CreatedJoint = new GameObject[typeJoint.Length];

        // Create copies
        for (int i = 0; i < typeJoint.Length; i++)
        {
            CreatedJoint[i] = Instantiate(PrefabJoint);
            CreatedJoint[i].transform.SetParent(transform);
        }

        message = "Skeleton created!";

    }

    // Update is called once per frame
    void Update(){
        // Check user presence in frame
        if(CurrentUserTracker.CurrentUser != 0)
        {
            message = "Skeleton found!";
            nuitrack.Skeleton skeleton = CurrentUserTracker.CurrentSkeleton; // Get current user skeleton info
            for (int i  = 0; i < typeJoint.Length; i++)
            {
                // Get current joint data
                nuitrack.Joint joint = skeleton.GetJoint(typeJoint[i]); 
                Vector3 newPosition = 0.001f * joint.ToVector3(); // Convert to milimeters
                CreatedJoint[i].transform.localPosition = newPosition;
            }
        }
        else
        {
            message = "Skeleton not found!";
        }
    }

    // OnGUI method to display the message
    void OnGUI(){
        GUI.color = Color.red;
        GUI.skin.label.fontSize = 50;
        GUILayout.Label(message);
    }

}
