using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

[RequireComponent(typeof(Player))]
public class PlayerSprinting : MonoBehaviour
{
    [SerializeField] float speedMultiplier = 0.5f;
    [SerializeField] float staminaMax = 100f;
    [SerializeField] float staminaDepletionRate = 10f;
    [SerializeField] float staminaRegenRate = 20f;
    [SerializeField] float sprintCost = 20f;

    Player player;
    PlayerInput playerInput;
    InputAction sprintAction;

    float currentStamina;

    void Awake()
    {
        player = GetComponent<Player>();
        playerInput = GetComponent<PlayerInput>();
        sprintAction = playerInput.actions["sprint"];
        currentStamina = staminaMax;
        //UpdateStaminaUI();
    }

    void OnEnable() => player.OnBeforeMove += OnBeforeMove;
    void OnDisable() => player.OnBeforeMove -= OnBeforeMove;

    void OnBeforeMove()
    {
        var sprintInput = sprintAction.ReadValue<float>();

        if (sprintInput > 0 && currentStamina >= sprintCost)
        {
            player.movementSpeedMultiplier *= speedMultiplier;
            currentStamina -= sprintCost * Time.deltaTime * staminaDepletionRate;
        }
        else
        {
            player.movementSpeedMultiplier = 1f;
            currentStamina += Time.deltaTime * staminaRegenRate;
            //UpdateStaminaUI();
        }

        currentStamina = Mathf.Clamp(currentStamina, 0f, staminaMax);
        //UpdateStaminaUI();
    }


    /**void UpdateStaminaUI()
    {
        if (staminaBar != null)
        {
            staminaBar.fillAmount = currentStamina / staminaMax;
        }
    }
    
    */

}
