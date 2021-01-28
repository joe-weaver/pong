using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]

// A class that gets some information all Pong solid physics objects need
public class SolidPongObject : MonoBehaviour, Bounded {

	// The rigidbody component of this ball
	protected Rigidbody2D rb;

	// The renderer of this sprite
	protected SpriteRenderer sr;

	protected Vector2 initialPosition;

	void Start(){
		// Get the rigidbody component
		rb = GetComponent<Rigidbody2D>();

		// Get the sprite renderer
		sr = GetComponent<SpriteRenderer>();

		// Get the initial position
		initialPosition = transform.position;
	}

	public Rect GetBounds() {
		// Get the center and the size of the sprite
		Vector2 center = transform.position;
		Vector2 size = sr.bounds.size;

		// Create the bounds object with the top left corner of the sprite
		Rect bounds = new Rect(center - size / 2f, size);

		return bounds;
	}

	public void ResetPosition() {
		transform.position = initialPosition;
	}
}
