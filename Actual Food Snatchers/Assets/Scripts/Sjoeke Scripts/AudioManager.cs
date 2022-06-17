using UnityEngine.Audio;
using System;
using UnityEngine;

/// <summary>
/// Followed a tutorial by Brackeys on audio called: "Introduction to AUDIO in Unity". 
/// Added the class Sounds in this script so it's easier to navigate
/// </summary>
[Serializable]
public class Sounds
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;

    [HideInInspector]
    public AudioSource source;

    public bool loop;
}
public class AudioManager : MonoBehaviour
{
    public Sounds[] sounds;

    /// <summary>
    /// Adding an audiosource to all the items in Sounds with coresponding settings: clip, volume, and loop.
    /// </summary>
    private void Awake()
    {
        foreach (Sounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
    }

    /// <summary>
    /// Play or stop a sound in the sounds array, they are called by the name given to them in the inspector.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="play"></param>
    public void Play (string name, bool play)
    {
        foreach (Sounds s in sounds)
        {
            if (play && s.name == name)
            {
                s.source.Play();
            }
            if (!play && s.name == name)
            {
                s.source.Stop();
            }
        }
    }
}
