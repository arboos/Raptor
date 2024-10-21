using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimations : MonoBehaviour
{
    public Animator PlayerAnimator { get; private set; }

    [SerializeField] private GameObject flame;
    
    private void Start()
    {
        if (PlayerInfo.Instance.playerAnimations == null)
        {
            PlayerAnimator = GetComponent<Animator>();
            PlayerInfo.Instance.playerAnimations = this;
        }
        else
        {
            Destroy(this);
        }
    }
    
    private void Update()
    {
        Animate();
    }

    private void Animate()
    {
        // Animates movement
        if (PlayerInfo.Instance.playerMovement != null)
        {
            Vector2 InputVector = PlayerInfo.Instance.playerMovement.InputVector;
            if (InputVector.x > 0.1 || InputVector.x < -0.1)
            {
                PlayerAnimator.SetBool("Moving", true);
                PlayerInfo.Instance.PlayerSpriteRenderer.flipX = InputVector.x > 0.1;
            }
            else
            {
                PlayerAnimator.SetBool("Moving", false);
                PlayerInfo.Instance.PlayerSpriteRenderer.flipX = false;
            }

            if (InputVector.y > 0.1)
            {
                flame.SetActive(true);
            }
            else
            {
                flame.SetActive(false);
            }
        }
    }
}
