using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;
using System;
public class ToggleCharacterController : MonoBehaviour
{
    public static Action<bool> Toggle;

    CharacterController characterController;
    ThirdPersonController thirdPersonController;
    BasicRigidBodyPush rigidBodyPush;
    StarterAssetsInputs starterAssetsInputs;
    PlayerInput playerInput;
    Animator animator;
    private void OnEnable()
    {
        Toggle += OnToggle;
    }
    private void OnDisable()
    {
        Toggle -= OnToggle;
    }
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        rigidBodyPush = GetComponent<BasicRigidBodyPush>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        // OnToggle(true);
    }
    void OnToggle(bool active)
    {
        animator.applyRootMotion = !active;
        characterController.enabled = active;
        thirdPersonController.enabled = active;
        rigidBodyPush.enabled = active;
        starterAssetsInputs.enabled = active;
        playerInput.enabled = active;
    }
}
