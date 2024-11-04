using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundsBaseCollection : MonoBehaviour
{
    public static SoundsBaseCollection Instance { get; private set; }

    public AudioSource Soundtrack;
    public AudioClip[] Soundtracks;
    
    [Header("UI")]
    public AudioSource ButtonClick;
    public AudioSource Counter;
    public AudioSource ToBattle;
    
    [Header("Player")]
    public AudioSource Damage;
    public AudioSource Death;
    public AudioSource Explosion;
    public AudioSource Heal;
    public AudioSource Coin;
    public AudioSource Lose;
    public AudioSource Win;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Soundtrack.clip = Soundtracks[SceneManager.GetActiveScene().buildIndex];
            Soundtrack.Play();
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    
    private void Start()
    {
        Button[] buttons = GameObject.FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (var butt in buttons)
        {
            butt.GetComponent<Button>().onClick.AddListener(delegate{SoundsBaseCollection.Instance.ButtonClick.Play();});
        }
        SceneManager.activeSceneChanged += SceneManagerOnactiveSceneChanged;
    }


    private void SceneManagerOnactiveSceneChanged(Scene arg0, Scene arg1)
    {
        Soundtrack.clip = Soundtracks[SceneManager.GetActiveScene().buildIndex];
        Soundtrack.Play();
    }
}
