using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 This script will contain all the information about Object collision.
 */

public class CollisionDetection : MonoBehaviour
{
    /*
    CollisionDetection behavior
    */

    //Score
    [SerializeField]
    int scoreValue = 0;

    bool active = true;

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
