using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(BikeController))]
    public class InputManager : MonoBehaviour
    {
        PlayerInput inputMap;
        PlayerInput.ActionMapActions playerControls;
        BikeController movement;

        private void Awake()
        {
            movement = GetComponent<BikeController>();

            inputMap = new PlayerInput();
            playerControls = inputMap.ActionMap;

            playerControls.Move.performed += ctx => movement.DoMove(ctx.ReadValue<Vector2>());
        }

        private void OnEnable() => inputMap.Enable();
        private void OnDestroy() => inputMap.Disable();
    }
}