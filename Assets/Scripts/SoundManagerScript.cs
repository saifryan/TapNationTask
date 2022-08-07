using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static SoundManagerScript Instance;
    public AudioClip LevelCompleteSound;
    public AudioClip LevelFailedSound;
    public AudioClip AddCharacter;
    public AudioClip[] DieSound;

    public AudioSource MainObject;
    public List<AudioSource> AS = new List<AudioSource>();
    public int TotalSpawn = 100;
    int count = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for(int i = 0; i < TotalSpawn; i++)
        {
            AudioSource TempAS = Instantiate(MainObject, transform.position, transform.rotation, transform);
            AS.Add(TempAS);
        }
    }

    public AudioSource GetAudioSource()
    {
        count++;
        if(count > AS.Count)
        {
            count = 1;
        }
        return AS[count - 1];
    }

    public void LevelCompletPlaySound(AudioSource tempAS)
    {
        tempAS.clip = LevelCompleteSound;
        tempAS.Play();
    }

    public void LevelFailedPlaySound(AudioSource tempAS)
    {
        tempAS.clip = LevelFailedSound;
        tempAS.Play();
    }

    public void AddCharacterPlaySound(AudioSource tempAS)
    {
        tempAS.clip = AddCharacter;
        tempAS.Play();
    }

    public void DiePlaySound(AudioSource tempAS)
    {
        tempAS.clip = DieSound[Random.Range(0, DieSound.Length)];
        tempAS.Play();
    }
}
