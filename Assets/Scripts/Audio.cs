using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    private AudioSource audioPlayer;
    [SerializeField] AudioClip[] audioClips;

    private void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
    }

    public void PlayAudio(int clip)
    {
        audioPlayer.PlayOneShot(audioClips[clip]);
    }

}
