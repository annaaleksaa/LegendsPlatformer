using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildPlayer : MonoBehaviour
{
    // keeps the player from slipping off the platform as it glides
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.parent = transform;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.parent = null;
        }
    }
}
