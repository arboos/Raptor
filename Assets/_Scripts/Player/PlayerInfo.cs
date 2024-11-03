using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(SpriteRenderer))]
public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo Instance { get; private set; }
    
    [HideInInspector] public PlayerMovement playerMovement;
    [HideInInspector] public PlayerAnimations playerAnimations;
    [HideInInspector] public PlayerAttack playerAttack;
    [HideInInspector] public PlayerHealth playerHealth;
    [HideInInspector] public PlayerEconomic playerEconomic;
    [HideInInspector] public SpriteRenderer PlayerSpriteRenderer;

    private int moneyInStart = 0;
    private int weaponsInStart = 1;
    private int currentSceneIndex;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            PlayerSpriteRenderer = GetComponent<SpriteRenderer>();
            currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        }
        else
        {
            Debug.LogError("More than one PlayerInfo on scene! GO.name = " + gameObject.name);
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SceneManager.activeSceneChanged += SceneManagerOnactiveSceneChanged;
    }

    private void SceneManagerOnactiveSceneChanged(Scene arg0, Scene arg1)
    {
        if (arg1.buildIndex == currentSceneIndex)
        {
            print("IFFFFFF CHAAAAANGED");
            transform.position = new Vector3(0f, -3f, 0f);
            playerEconomic.money = moneyInStart;
            playerAttack.canShoot = true;
            playerMovement.canMove = true;
            playerHealth.Health = playerHealth.MaxHealth;
            for (int i = weaponsInStart; i < playerAttack.weaponList.Count; i++)
            {
                playerAttack.weaponList.Remove(playerAttack.weaponList[i]);
            }
        }
        else
        {
            print("ELSEEEEEE CHAAAAANGED");
            moneyInStart = playerEconomic.money;
            weaponsInStart = playerAttack.weaponList.Count;
            currentSceneIndex = arg1.buildIndex;
        }
    }
}
