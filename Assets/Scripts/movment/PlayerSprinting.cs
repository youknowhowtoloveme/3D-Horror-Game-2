using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

[RequireComponent(typeof(Player))]
public class PlayerSprinting : MonoBehaviour
{
    [SerializeField] float SpeedMultiplier = 0.5f;
    Player player;
    PlayerInput playerInput;
    InputAction sprintAction;

    void Awake()
    {
        player = GetComponent<Player>();
        playerInput = GetComponent<PlayerInput>();
        sprintAction = playerInput.actions["sprint"];
    }

    void OnEnable() => player.OnBeforeMove += OnBeforeMove;
    void OnDisable() => player.OnBeforeMove -= OnBeforeMove;

    void OnBeforeMove()
    {
        var sprintInput = sprintAction.ReadValue<float>();
        player.movementSpeedMultiplier *= sprintInput > 0 ? SpeedMultiplier : 1f;
    }
}