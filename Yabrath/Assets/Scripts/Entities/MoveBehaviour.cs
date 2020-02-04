﻿using UnityEngine;

// MoveBehaviour inherits from GenericBehaviour. This class corresponds to basic walk and run behaviour, it is the default behaviour.
public class MoveBehaviour : GenericBehaviour
{
	public float walkSpeed = 0.15f;                 // Default walk speed.
	public float runSpeed = 1.0f;                   // Default run speed.
	public float sprintSpeed = 2.0f;                // Default sprint speed.
	public float speedDampTime = 0.1f;              // Default damp time to change the animations based on current speed.
	//public string jumpButton = "Jump";              // Default jump button.
	public float jumpHeight = 1.5f;                 // Default jump height.
	public float jumpIntertialForce = 10f;          // Default horizontal inertial force when jumping.

	private float speed, speedSeeker;               // Moving speed.
	private int jumpBool;                           // Animator variable related to jumping.
	private int groundedBool;                       // Animator variable related to whether or not the player is on ground.
	private bool jump;                              // Boolean to determine whether or not the player started a jump.
	private bool isColliding;                       // Boolean to determine if the player has collided with an obstacle.
    private int strongAttackBool;
    private int resetComboBool;
    private bool start_jumping = false;

    private bool strongAttack = false;
    private bool start_attack = false;
    private string[] strongAttackParameters;
    private int combo_index = 0;
	// Start is always called after any Awake functions.
	void Start()
	{
		// Set up the references.
		jumpBool = Animator.StringToHash("Jump");
		groundedBool = Animator.StringToHash("Grounded");
        strongAttackBool = Animator.StringToHash("StrongAttack");
        resetComboBool = Animator.StringToHash("ResetCombo");

		behaviourManager.GetAnim.SetBool(groundedBool, true);
        
		// Subscribe and register this behaviour as the default behaviour.
		behaviourManager.SubscribeBehaviour(this);
		behaviourManager.RegisterDefaultBehaviour(this.behaviourCode);
		speedSeeker = runSpeed;

        //Sito Test attacks
        strongAttackParameters = new string[] { "Kick1", "Kick2", "Kick3", "StrongAttackReset" };

	}

	// Update is used to set features regardless the active behaviour.
	void Update()
	{
		// Get jump input.
		if (!jump && behaviourManager.inputController.JumpButton && behaviourManager.IsCurrentBehaviour(this.behaviourCode) && !behaviourManager.IsOverriding())
		{
            if(behaviourManager.GetAnim.GetCurrentAnimatorStateInfo(0).IsName("Locomotion"))
			    jump = true;
		}

        if(behaviourManager.inputController.StrongButton && behaviourManager.IsCurrentBehaviour(this.behaviourCode) && !behaviourManager.IsOverriding() && !jump)
        {
                strongAttack = true;
        }

	}

	// LocalFixedUpdate overrides the virtual function of the base class.
	public override void LocalFixedUpdate()
	{

        AttackManagement();

		// Call the basic movement manager.
		MovementManagement(behaviourManager.GetH, behaviourManager.GetV);

		// Call the jump manager.
		JumpManagement();
	}

    void AttackManagement()
    {
        if(strongAttack && behaviourManager.IsGrounded() && !start_attack)
        {
            behaviourManager.LockTempBehaviour(this.behaviourCode);
            behaviourManager.GetAnim.SetTrigger(strongAttackParameters[combo_index]);
            combo_index++;
            start_attack = true;
            strongAttack = false;
        }
        
        if (start_attack && behaviourManager.GetAnim.GetCurrentAnimatorStateInfo(2).IsName("Kick1"))
        {
      
            if(strongAttack && (behaviourManager.GetAnim.GetCurrentAnimatorStateInfo(2).normalizedTime <= 0.9f && behaviourManager.GetAnim.GetCurrentAnimatorStateInfo(2).normalizedTime >= 0.3f))
            {
                behaviourManager.GetAnim.SetTrigger(strongAttackParameters[combo_index]);
                combo_index++;
            }
            else if ( behaviourManager.GetAnim.GetCurrentAnimatorStateInfo(2).normalizedTime > 1f)
            {
                combo_index = strongAttackParameters.Length;
            }

            strongAttack = false;
        }

        if (start_attack && behaviourManager.GetAnim.GetCurrentAnimatorStateInfo(2).IsName("Kick2"))
        {

            if (strongAttack && (behaviourManager.GetAnim.GetCurrentAnimatorStateInfo(2).normalizedTime <= 0.9f || behaviourManager.GetAnim.GetCurrentAnimatorStateInfo(2).normalizedTime >= 0.3f))
            {
                behaviourManager.GetAnim.SetTrigger(strongAttackParameters[combo_index]);
                combo_index++;
            }
            else if (behaviourManager.GetAnim.GetCurrentAnimatorStateInfo(2).normalizedTime > 1f)
            {
                combo_index = strongAttackParameters.Length; 
            }

            strongAttack = false;
        }

        if (start_attack && behaviourManager.GetAnim.GetCurrentAnimatorStateInfo(2).IsName("Kick3"))
        {

            if (behaviourManager.GetAnim.GetCurrentAnimatorStateInfo(2).normalizedTime > 1f)
            {
                combo_index++;
            }
        }

        if (start_attack && combo_index == strongAttackParameters.Length && !behaviourManager.GetAnim.GetCurrentAnimatorStateInfo(2).IsName("NoneAttack"))
        {
            combo_index = strongAttackParameters.Length - 1;
            behaviourManager.GetAnim.SetTrigger(strongAttackParameters[combo_index]);

            start_attack = false;
            combo_index = 0;
            behaviourManager.UnlockTempBehaviour(this.behaviourCode);
        }

    }

	// Execute the idle and walk/run jump movements.
	void JumpManagement()
	{
        if (jump)
        {
            if (behaviourManager.IsGrounded() && !start_jumping)
            {
                behaviourManager.LockTempBehaviour(this.behaviourCode);
                behaviourManager.GetAnim.SetBool(jumpBool, true);
                GetComponent<CapsuleCollider>().material.dynamicFriction = 0f;
                GetComponent<CapsuleCollider>().material.staticFriction = 0f;
                RemoveVerticalVelocity();

                start_jumping = true;
            }

            if (behaviourManager.GetAnim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
            {
                if (start_jumping && behaviourManager.GetAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.6f)
                {
                    // Set jump vertical impulse velocity.
                    float velocity = 2f * Mathf.Abs(Physics.gravity.y) * jumpHeight;
                    velocity = Mathf.Sqrt(velocity);
                    behaviourManager.GetRigidBody.AddForce(Vector3.up * velocity, ForceMode.VelocityChange);
                   // behaviourManager.GetRigidBody.AddForce(Vector3.forward * walkSpeed, ForceMode.VelocityChange);
                }


                if (behaviourManager.GetAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.6f)
                {
                    behaviourManager.GetRigidBody.AddForce(Vector3.down * Mathf.Abs(Physics.gravity.y / 4), ForceMode.VelocityChange);
                  

                    if (behaviourManager.IsGrounded())
                    {
                        start_jumping = false;
                        jump = false;
                        behaviourManager.GetAnim.SetBool(jumpBool, false);
                        behaviourManager.UnlockTempBehaviour(this.behaviourCode);
                    }
                }

                behaviourManager.GetRigidBody.AddForce(transform.forward * jumpIntertialForce/2  * Physics.gravity.magnitude/3  * behaviourManager.GetAnim.GetFloat("Speed")*2, ForceMode.Acceleration);
            }


        }
		// Start a new jump.
		/*if (jump && !behaviourManager.GetAnim.GetBool(jumpBool) && behaviourManager.IsGrounded())
		{
			// Set jump related parameters.
			behaviourManager.LockTempBehaviour(this.behaviourCode);
			behaviourManager.GetAnim.SetBool(jumpBool, true);
			// Is a locomotion jump?
			if (behaviourManager.GetAnim.GetFloat(speedFloat) > 0.1)
			{
				// Temporarily change player friction to pass through obstacles.
				GetComponent<CapsuleCollider>().material.dynamicFriction = 0f;
				GetComponent<CapsuleCollider>().material.staticFriction = 0f;
				// Remove vertical velocity to avoid "super jumps" on slope ends.
				RemoveVerticalVelocity();
				// Set jump vertical impulse velocity.
				float velocity = 2f * Mathf.Abs(Physics.gravity.y) * jumpHeight;
				velocity = Mathf.Sqrt(velocity);
                behaviourManager.GetRigidBody.AddForce(Vector3.up * velocity *10.0f , ForceMode.VelocityChange);

                //behaviourManager.GetRigidBody.AddRelativeForce(Vector3.up * velocity, ForceMode.VelocityChange);
            }
           // behaviourManager.GetRigidBody.velocity = Vector3.up * 40.0f;
           
        }
		// Is already jumping?
		else if (behaviourManager.GetAnim.GetBool(jumpBool))
		{
			// Keep forward movement while in the air.
			if (!behaviourManager.IsGrounded() && !isColliding && behaviourManager.GetTempLockStatus())
			{
				behaviourManager.GetRigidBody.AddForce(transform.forward * jumpIntertialForce/4 * Physics.gravity.magnitude/10 * sprintSpeed, ForceMode.Acceleration);
			}
			// Has landed?
			if ((behaviourManager.GetRigidBody.velocity.y < 0) && behaviourManager.IsGrounded())
			{
				behaviourManager.GetAnim.SetBool(groundedBool, true);
				// Change back player friction to default.
				GetComponent<CapsuleCollider>().material.dynamicFriction = 0.6f;
				GetComponent<CapsuleCollider>().material.staticFriction = 0.6f;
				// Set jump related parameters.
				jump = false;
				behaviourManager.GetAnim.SetBool(jumpBool, false);
				behaviourManager.UnlockTempBehaviour(this.behaviourCode);
			}
		}*/
	}

	// Deal with the basic player movement
	void MovementManagement(float horizontal, float vertical)
	{
		// On ground, obey gravity.
		if (behaviourManager.IsGrounded())
			behaviourManager.GetRigidBody.useGravity = true;
        else if(!jump)
        {
            behaviourManager.GetRigidBody.AddForce(Vector3.down * Mathf.Abs(Physics.gravity.y / 3), ForceMode.VelocityChange);
        }


		// Avoid takeoff when reached a slope end.
		else if (!behaviourManager.GetAnim.GetBool(jumpBool) && behaviourManager.GetRigidBody.velocity.y > 0)
		{
			RemoveVerticalVelocity();
		}

		// Call function that deals with player orientation.
		Rotating(horizontal, vertical);

		// Set proper speed.
		Vector2 dir = new Vector2(horizontal, vertical);
		speed = Vector2.ClampMagnitude(dir, 1f).magnitude;
		// This is for PC only, gamepads control speed via analog stick.
		//speedSeeker += Input.GetAxis("Mouse ScrollWheel"); //SITO
		speedSeeker = Mathf.Clamp(speedSeeker, walkSpeed, runSpeed);
		speed *= speedSeeker;
		if (behaviourManager.IsSprinting())
		{
			speed = sprintSpeed;
		}

		behaviourManager.GetAnim.SetFloat(speedFloat, speed, speedDampTime, Time.deltaTime);

        // behaviourManager.GetRigidBody.AddForce(transform.forward*speed *10.0f,ForceMode.Impulse);
        if(speed > 0.01f && behaviourManager.IsGrounded())
            behaviourManager.GetRigidBody.MovePosition(transform.position + transform.forward * speed* Time.deltaTime);
    }

	// Remove vertical rigidbody velocity.
	private void RemoveVerticalVelocity()
	{
		Vector3 horizontalVelocity = behaviourManager.GetRigidBody.velocity;
		horizontalVelocity.y = 0;
		behaviourManager.GetRigidBody.velocity = horizontalVelocity;
	}

	// Rotate the player to match correct orientation, according to camera and key pressed.
	Vector3 Rotating(float horizontal, float vertical)
	{
		// Get camera forward direction, without vertical component.
		Vector3 forward = behaviourManager.playerCamera.TransformDirection(Vector3.forward);

		// Player is moving on ground, Y component of camera facing is not relevant.
		forward.y = 0.0f;
		forward = forward.normalized;

		// Calculate target direction based on camera forward and direction key.
		Vector3 right = new Vector3(forward.z, 0, -forward.x);
		Vector3 targetDirection;
		targetDirection = forward * vertical + right * horizontal;

		// Lerp current direction to calculated target direction.
		if ((behaviourManager.IsMoving() && targetDirection != Vector3.zero))
		{
			Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

			Quaternion newRotation = Quaternion.Slerp(behaviourManager.GetRigidBody.rotation, targetRotation, behaviourManager.turnSmoothing);
			behaviourManager.GetRigidBody.MoveRotation(newRotation);
			behaviourManager.SetLastDirection(targetDirection);
		}
		// If idle, Ignore current camera facing and consider last moving direction.
		if (!(Mathf.Abs(horizontal) > 0.9 || Mathf.Abs(vertical) > 0.9))
		{
			behaviourManager.Repositioning();
		}

		return targetDirection;
	}

	// Collision detection.
	private void OnCollisionStay(Collision collision)
	{
		isColliding = true;
		// Slide on vertical obstacles
		if (behaviourManager.IsCurrentBehaviour(this.GetBehaviourCode()) && collision.GetContact(0).normal.y <= 0.1f)
		{
			float vel = behaviourManager.GetAnim.velocity.magnitude;
			Vector3 tangentMove = Vector3.ProjectOnPlane(transform.forward, collision.GetContact(0).normal).normalized * vel;
			behaviourManager.GetRigidBody.AddForce(tangentMove, ForceMode.VelocityChange);
		}

	}
	private void OnCollisionExit(Collision collision)
	{
		isColliding = false;
	}
}