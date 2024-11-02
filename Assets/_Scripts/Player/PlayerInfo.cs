using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(SpriteRenderer))]
public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo Instance { get; private set; }

    [HideInInspector] public PlayerMovement playerMovement;
    [HideInInspector] public PlayerAnimations playerAnimations;
    [HideInInspector] public PlayerAttack playerAttack;
    [HideInInspector] public PlayerHealth playerHealth;
    
    [HideInInspector] public SpriteRenderer PlayerSpriteRenderer;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            PlayerSpriteRenderer = GetComponent<SpriteRenderer>();
        }
        else
        {
            Debug.LogError("More than one PlayerInfo on scene! GO.name = " + gameObject.name);
            Destroy(gameObject);
        }
    }
    
}
