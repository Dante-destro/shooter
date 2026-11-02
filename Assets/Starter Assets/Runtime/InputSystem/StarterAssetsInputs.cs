using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;

		[Header("Movement Settings")]
		public bool analogMovement;
		public bool lookFromTouch;

        [Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;
		[Header("Shooting Settings")]
		public GameObject projectilePrefab;
		public Transform projectileSpawnPoint;
		public float projectileForce = 20f;

#if ENABLE_INPUT_SYSTEM
        public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				lookFromTouch = false;
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}
        public void OnShoot(InputValue value)
        {
            // Implement shooting logic here
			Debug.Log("Shoot action triggered");
			Shoot();
        }
        private void Shoot()
        {
            // Implement shooting logic here
            Debug.Log("REAL SHOTING LOGIC GOES HERE");
            GameObject newprojectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, transform.rotation);
			newprojectile.GetComponent<Rigidbody>().AddForce(projectileSpawnPoint.up * projectileForce, ForceMode.Impulse);
        }

        public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}
		
		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}