
using UnityEngine;

//[RequireComponent(typeof(BikeMovement))]
public class InputManager : MonoBehaviour
{
    PlayerInput inputMap;
    PlayerInput.ActionMapActions playerControls;
    MyBikeControll1 movement;

    private void Awake()
    {
        movement = GetComponent<MyBikeControll1>();

        inputMap = new PlayerInput();
        playerControls = inputMap.ActionMap;

        playerControls.Move.performed += ctx => movement.DoMove(ctx.ReadValue<Vector2>());
    }

    private void OnEnable() => inputMap.Enable();
    private void OnDestroy() => inputMap.Disable();
}
