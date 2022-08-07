using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairScript : MonoBehaviour
{
    public GameObject ParticleObject;
    bool OneTimeRun = true;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (OneTimeRun)
            {
                OneTimeRun = false;
                if (ParticleObject != null)
                    ParticleObject.SetActive(true);
            }
            other.GetComponent<Collider>().enabled = false;
            //Debug.Log("Enter Here!......");
            PlayerEndSceneMovementScript.Instance.RemoveRunner(other.gameObject);
        }
    }
}
