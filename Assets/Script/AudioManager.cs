using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Settings")]
    public AudioClip[] soundClips; // Array for assigning clips in the Inspector
    public int[] soundClipIDs;    // Integer IDs corresponding to each clip (same length as soundClips)

    private Dictionary<int, AudioClip> clipDictionary;
    private Dictionary<int, AudioSource> loopingAudioSources; // Manage multiple looping sounds
    private List<AudioSource> audioSources;

    [Header("Audio Source Pooling")]
    public int maxAudioSources = 10;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeClipDictionary();
            InitializeAudioSources();
            loopingAudioSources = new Dictionary<int, AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeClipDictionary()
    {
        clipDictionary = new Dictionary<int, AudioClip>();
        for (int i = 0; i < soundClips.Length; i++)
        {
            if (i < soundClipIDs.Length && !clipDictionary.ContainsKey(soundClipIDs[i]))
            {
                clipDictionary.Add(soundClipIDs[i], soundClips[i]);
            }
            else
            {
                Debug.LogWarning($"SoundClip ID mismatch or duplicate: {soundClipIDs[i]}");
            }
        }
    }

    private void InitializeAudioSources()
    {
        audioSources = new List<AudioSource>();
        for (int i = 0; i < maxAudioSources; i++)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSources.Add(audioSource);
        }
    }

    // Method for playing one-time sound effects
    public void PlaySound(int clipID, float volume = 1.0f)
    {
        if (clipDictionary.TryGetValue(clipID, out AudioClip clip))
        {
            AudioSource audioSource = GetAvailableAudioSource();
            if (audioSource != null)
            {
                audioSource.clip = clip;
                audioSource.volume = Mathf.Clamp01(volume);
                audioSource.Play();
            }
            else
            {
                Debug.LogWarning("No available AudioSource to play the sound effect.");
            }
        }
        else
        {
            Debug.LogWarning($"No AudioClip found with ID: {clipID}");
        }
    }

    private AudioSource GetAvailableAudioSource()
    {
        foreach (AudioSource source in audioSources)
        {
            if (!source.isPlaying)
                return source;
        }
        return null; // Return null if no AudioSource is available
    }

    // Method for playing looping sounds
    public void PlayLoopingSound(int clipID, float volume = 1.0f)
    {
        if (clipDictionary.TryGetValue(clipID, out AudioClip clip))
        {
            if (!loopingAudioSources.ContainsKey(clipID))
            {
                AudioSource audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.clip = clip;
                audioSource.loop = true;
                audioSource.volume = Mathf.Clamp01(volume);
                audioSource.Play();
                loopingAudioSources.Add(clipID, audioSource);
            }
        }
        else
        {
            Debug.LogWarning($"No AudioClip found with ID: {clipID}");
        }
    }

    // Method for stopping a specific looping sound
    public void StopLoopingSound(int clipID)
    {
        if (loopingAudioSources.TryGetValue(clipID, out AudioSource audioSource))
        {
            audioSource.Stop();
            loopingAudioSources.Remove(clipID);
            Destroy(audioSource); // Clean up the AudioSource
        }
        else
        {
            Debug.LogWarning($"No looping sound found with ID: {clipID}");
        }
    }

    // Method for stopping all looping sounds
    public void StopAllLoopingSounds()
    {
        foreach (var audioSource in loopingAudioSources.Values)
        {
            audioSource.Stop();
            Destroy(audioSource);
        }
        loopingAudioSources.Clear();
    }
}
