using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<Collider>().enabled = false;
            Debug.Log("Enter Here!......");
            PlayerEndSceneMovementScript.Instance.RemoveRunner(other.gameObject);
        }
    }
}
