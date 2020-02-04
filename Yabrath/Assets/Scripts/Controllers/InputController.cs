using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class InputController : MonoBehaviour
{
    //Todo if have time: reconfiguration inputs
    //Xbox Controller configuration
    private bool player_index_set = false;
    private PlayerIndex player_index = PlayerIndex.One;
    private GamePadState state;
    private GamePadState previous_state;

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
    private Vector2 left_stick = Vector2.zero;
    private Vector2 right_stick = Vector2.zero;
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
    public Vector2 LeftStick { private set { left_stick = value; }  get { return left_stick; } }
    public Vector2 RightStick { private set { right_stick = value; } get { return right_stick; } }
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

    // Start is called before the first frame update
    void Start()
    {
        SetController();
    }

    // Update is called once per frame
    void Update()
    {
        previous_state = state;
        state = GamePad.GetState(player_index);
        SetController();

        //Movement
        AxisX = Input.GetAxis(axis_x);
        AxisY = Input.GetAxis(axis_y);
        LeftStick = new Vector2 (state.ThumbSticks.Left.X, state.ThumbSticks.Left.Y);

        //Camera Rotation
        MouseX = Input.GetAxis(mouse_x);
        MouseY = Input.GetAxis(mouse_y);
        Vector2 tmp = new Vector2(state.ThumbSticks.Right.X, state.ThumbSticks.Right.Y);
        RightStick = tmp;

        //Actions
        if (Input.GetButton(sprintButton) == true || state.Buttons.LeftStick == ButtonState.Pressed) SprintButton = true; else SprintButton = false;
        if (Input.GetButton(aimButton) == true || state.Buttons.LeftShoulder == ButtonState.Pressed) AimButton = true; else AimButton = false;
        if (Input.GetButton(shootButton) == true || state.Buttons.RightShoulder == ButtonState.Pressed) ShootButton = true; else ShootButton= false;
        if (Input.GetButton(weakAttackButton) == true || state.Buttons.X == ButtonState.Pressed) WeakAttackButton = true; else WeakAttackButton = false;
        if (Input.GetButton(strongAttackButton) == true || state.Buttons.Y == ButtonState.Pressed) StrongButton = true; else StrongButton = false;
        if (Input.GetButton(jumpButton) == true || state.Buttons.A == ButtonState.Pressed) JumpButton = true; else JumpButton = false;
        if (Input.GetButton(tumbleButton) == true || state.Buttons.B == ButtonState.Pressed) TumbleButton = true; else TumbleButton = false;


    }

    private void SetController()
    {
        if (!player_index_set || !previous_state.IsConnected)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    player_index = testPlayerIndex;
                    player_index_set = true;
                    break;
                }
            }
        }
    }
}
