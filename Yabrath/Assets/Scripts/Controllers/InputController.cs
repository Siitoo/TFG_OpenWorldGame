using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{

    [Header("Keyboard configuration")]
    //Keyboard configuration 
    public string sprintButton = "Sprint";
    public string aimButton = "Fire2";
    public string shootButton = "Fire1";
    public string weakAttackButton = "Q";
    public string strongAttackButton = "E";
    public string jumpButton = "Jump";
    public string tumbleButton = "Left Alt";

    private string mouse_x = "Mouse X";
    private string mouse_y = "Mouse Y";
    private string axis_x = "Horizontal";
    private string axis_y = "Vertical";

    //private save data
    private float x_mouse = 0.0f;
    private float y_mouse = 0.0f;
    private float x_axis = 0.0f;
    private float y_axis = 0.0f;

    private bool sprint_button = false;
    private bool aim_button = false;
    private bool shoot_button = false;
    private bool weak_attack_button = false;
    private bool strong_attack_button = false;
    private bool jump_button = false;
    private bool tumble_button = false;

    //public value to read
    public float MouseX { private set { x_mouse = value; } get { return x_mouse; } }
    public float MouseY { private set { y_mouse = value; } get { return y_mouse; } }
    public float AxisX { private set { x_axis = value; } get { return x_axis; } }
    public float AxisY { private set { y_axis = value; } get { return y_axis; } }
    
    public bool SprintButton { private set { sprint_button = value; } get { return sprint_button; } }
    public bool AimButton { private set { aim_button = value; } get { return aim_button; } }
    public bool ShootButton { private set { shoot_button = value; } get { return shoot_button; } }
    public bool WeakAttackButton { private set { weak_attack_button = value; } get { return weak_attack_button; } }
    public bool StrongButton { private set { strong_attack_button = value; } get { return strong_attack_button; } }
    public bool JumpButton { private set { jump_button = value; } get { return jump_button; } }
    public bool TumbleButton { private set { tumble_button = value; } get { return tumble_button; } }


    // Update is called once per frame
    void Update()
    {
       
        //Movement
        AxisX = Input.GetAxis(axis_x);
        AxisY = Input.GetAxis(axis_y);

        //Camera Rotation
        MouseX = Input.GetAxis(mouse_x);
        MouseY = Input.GetAxis(mouse_y);

        //Actions
        if (Input.GetButton(sprintButton) == true) SprintButton = true; else SprintButton = false;
        if (Input.GetButton(shootButton) == true) ShootButton = true; else ShootButton= false;
        if (Input.GetButton(weakAttackButton) == true) WeakAttackButton = true; else WeakAttackButton = false;
        if (Input.GetButton(strongAttackButton) == true) StrongButton = true; else StrongButton = false;
        if (Input.GetButtonDown(jumpButton) == true) JumpButton = true; else JumpButton = false;
        if (Input.GetButton(tumbleButton) == true) TumbleButton = true; else TumbleButton = false;

    }

}
