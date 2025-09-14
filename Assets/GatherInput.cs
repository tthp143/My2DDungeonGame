using UnityEngine;
using UnityEngine.InputSystem;

public class GatherInput : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private Controls myControl;
    public float valueX;
    public bool jumpInput;

    public void Awake()
    {
        myControl = new Controls();
    }
    private void OnEnable()
    {
        myControl.Player.Move.performed += StartMove;
        myControl.Player.Move.canceled += StopMove;
        myControl.Player.Enable();

        myControl.Player.Jump.performed += JumpStart;
        myControl.Player.Jump.canceled += JumpStop;

        myControl.Player.Enable();
    }
    public void OnDisable()
    {
        myControl.Player.Move.performed -= StartMove;
        myControl.Player.Move.canceled -= StopMove;
        myControl.Player.Disable();
        //myControl.Disable();
        //...
        myControl.Player.Jump.performed -= JumpStart;
        myControl.Player.Jump.canceled -= JumpStop;

        myControl.Player.Disable();


    }
    private void StartMove(InputAction.CallbackContext ctx)
    {
        valueX = ctx.ReadValue<float>();
    }
    private void StopMove(InputAction.CallbackContext ctx)
    {
        valueX = 0;
    }
    private void JumpStart(InputAction.CallbackContext ctx)
    {
        jumpInput = true;
    }
    private void JumpStop(InputAction.CallbackContext ctx)
    {
        jumpInput = false;
    }

    public void DisableControls() 
    {
        myControl.Player.Move.performed -= StartMove;
        myControl.Player.Move.canceled -= StopMove;

        myControl.Player.Jump.performed -= JumpStart;
        myControl.Player.Jump.canceled -= JumpStop;

        myControl.Player.Disable();
        valueX = 0;
    }
}
