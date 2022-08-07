using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationChangeScript : MonoBehaviour
{
    bool firstimerun = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !firstimerun)
        {
            firstimerun = true;
            PlayerEndSceneMovementScript.Instance.FormationChange();
        }
    }
}
