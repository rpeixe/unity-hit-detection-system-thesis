using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class FighterInputController : MonoBehaviour
{
    public event Action<ControllerButtons> OnButtonPressed;
    public event Action<ControllerButtons> OnButtonReleased;
    public event Action<Vector2> OnLeftStickUpdated;

    public Vector2 LeftStickPosition { get; private set; }

    private PlayerInput m_playerInput;

    private void Awake()
    {
        m_playerInput = GetComponent<PlayerInput>();

        var actions = m_playerInput.actions;
        actions["Jump"].performed += HandleJumpPerformed;
        actions["Jump"].canceled += HandleJumpCanceled;
        actions["Attack"].performed += HandleAttackPerformed;
        actions["Attack"].canceled += HandleAttackCanceled;
    }

    private void OnDestroy()
    {
        var actions = m_playerInput.actions;
        actions["Jump"].performed -= HandleJumpPerformed;
        actions["Jump"].canceled -= HandleJumpCanceled;
        actions["Attack"].performed -= HandleAttackPerformed;
        actions["Attack"].canceled -= HandleAttackCanceled;
    }

    private void Update()
    {
        if (m_playerInput.inputIsActive)
        {
            Vector2 leftStick = m_playerInput.actions["LeftStick"].ReadValue<Vector2>();
            LeftStick(leftStick);
        }
    }

    public void LeftStick(Vector2 value)
    {
        LeftStickPosition = value;
        OnLeftStickUpdated?.Invoke(value);
    }

    public void HandleJumpPerformed(InputAction.CallbackContext context)
    {
        PressJump();
    }

    public void HandleJumpCanceled(InputAction.CallbackContext context)
    {
        ReleaseJump();
    }

    public void PressJump()
    {
        OnButtonPressed?.Invoke(ControllerButtons.Jump);
    }

    public void ReleaseJump()
    {
        OnButtonReleased?.Invoke(ControllerButtons.Jump);
    }

    public void HandleAttackPerformed(InputAction.CallbackContext context)
    {
        PressAttack();
    }

    public void HandleAttackCanceled(InputAction.CallbackContext context)
    {
        ReleaseAttack();
    }

    public void PressAttack()
    {
        OnButtonPressed?.Invoke(ControllerButtons.Attack);
    }

    public void ReleaseAttack()
    {
        OnButtonReleased?.Invoke(ControllerButtons.Attack);
    }
}
