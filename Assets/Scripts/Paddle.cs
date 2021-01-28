using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// An interface for moving the paddle. This is used both by a player controller and an AI
public class Paddle : SolidPongObject {

	public float speed = 8.0f;

	// Expose a way for the player or an AI to move the paddle
	public void MovePaddle(Vector2 direction) {
		// Check for collisions with the wall
		Rect bounds = GetBounds();

		Vector2 raycastStart;

		if(direction.y > 0) {
			raycastStart = new Vector2(bounds.xMin, bounds.yMax + 0.01f);
		} else {
			raycastStart = new Vector2(bounds.xMin, bounds.yMin - 0.01f);
		}

		RaycastHit2D hitInfo = Physics2D.Raycast(raycastStart, direction, 0.1f);

		if(hitInfo) {
			// We hit something, so prevent movement
			rb.velocity = Vector2.zero;
		} else {
			// Move the paddle
			rb.velocity = direction * speed;
		}
	}
}
