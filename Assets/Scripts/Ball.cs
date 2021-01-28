using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : SolidPongObject {

	// Movement
	public float speed = 1.0f;
	Vector2 velocity = Vector2.zero;

	// Event for when ball enters the point area
	public delegate void PointAction(int playerSide);
	public event PointAction GotPoint;

	public void StartRound(Vector2 direction) {
		velocity = direction.normalized * speed;
		rb.velocity = velocity;
	}

	private void OnTriggerEnter2D(Collider2D col) {
		if(col.tag == "PointArea") {
			// Let listening classes know we got a point
			if(GotPoint != null) {
				GotPoint(col.GetComponentInParent<PointArea>().playerSide);
			}
		}
	}

	// Manually handle collisions and bounce off reflected
	void OnCollisionEnter2D(Collision2D col) {
		ContactPoint2D contact = col.contacts[0];

		

		if(col.collider.tag == "Paddle") {
			// If we collided with a paddle, adjust the bounce angle depending on where we hit it
			Rect bounds = col.collider.GetComponentInParent<Paddle>().GetBounds();

			float relativePosition = ChangeRange(bounds.yMin, bounds.yMax, transform.position.y, -1, 1);

			float angle = (velocity.x > 0 ? -1 : 1) * relativePosition * Mathf.PI / 4f;

			Vector2 bounceDirection = velocity.x > 0 ? Vector2.left : Vector2.right;

			velocity = RotateCCW(bounceDirection, angle) * velocity.magnitude;
		} else {
			// Reflect our trajectory off of the line this normal is perpendicular to
			velocity = Vector2.Reflect(velocity, contact.normal);
		}
			
		rb.velocity = velocity;

	}

	float ChangeRange(float a, float b, float x, float c, float d) {
		float t = Mathf.InverseLerp(a, b, x);
		return Mathf.Lerp(c, d, t);
	}

	Vector2 RotateCCW(Vector2 vec, float angle) {
		float cs = Mathf.Cos(angle);
		float sn = Mathf.Sin(angle);
		float tempX = vec.x * cs - vec.y * sn;
		float tempY = vec.x * sn + vec.y * cs;
		vec.x = tempX;
		vec.y = tempY;

		return vec;
	}

	public void ResetVelocity() {
		rb.velocity = Vector2.zero;
	}
	
}
