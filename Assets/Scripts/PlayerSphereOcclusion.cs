using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerSphereOcclusion : MonoBehaviour
{


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log(collider.gameObject.name);
            collider.gameObject.GetComponent<MeshRenderer>().material.color = new Color(1f, 1f, 1f, 0.2f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log(other.gameObject.name);
            other.gameObject.GetComponent<MeshRenderer>().material.color = new Color(1f, 1f, 1f, 1f);
        }
    }

}
