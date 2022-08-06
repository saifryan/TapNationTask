using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [Header(" Particles ")]
    [SerializeField] private ParticleSystem[] confettis;

    public void PlayConfettiParticles()
    {
        foreach (ParticleSystem ps in confettis)
            ps.Play();

        //Audio_Manager.instance.play("Level_Complete");
    }
}
