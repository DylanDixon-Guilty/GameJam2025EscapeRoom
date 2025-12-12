using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource music;
    [SerializeField] AudioSource sfx;
    public AudioClip museumSong;
    public AudioClip explosion;
    public AudioClip footSteps;
    private int mainScene = 0;
    private static AudioManager audioManagerInstance;
    Scene scene;

    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scene = SceneManager.GetActiveScene();
        if (scene.buildIndex != mainScene)
        {
            music.Stop();
        }
    }
}
