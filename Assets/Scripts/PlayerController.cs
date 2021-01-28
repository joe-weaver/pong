using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Paddle))]
public class PlayerController : MonoBehaviour {

	// The paddle this script is controlling
	Paddle paddle;

	// Player movement
	Vector2 direction = new Vector2(0, 0);

	// Player number
	public bool isPlayerOne = true;

	void Start() {
		// Retrieve the paddle component
		paddle = GetComponent<Paddle>();
    }

    void Update() {
		// Get the player input
		if(isPlayerOne) {
			direction.y = (Input.GetKey(KeyCode.W) ? 1 : 0) + (Input.GetKey(KeyCode.S) ? -1 : 0);
		} else {
			direction.y = (Input.GetKey(KeyCode.UpArrow) ? 1 : 0) + (Input.GetKey(KeyCode.DownArrow) ? -1 : 0);
		}
		
    }

	// Use fixed update to move the rigidbody
	void FixedUpdate() {
		paddle.MovePaddle(direction);
	}
}
