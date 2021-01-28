using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	// An array to hold player scores
	int[] scores = new int[2];

	// A reference to the ball so the GameManager can start the game
	public Ball ball;
	public Paddle player1;
	public Paddle player2;

	// Game stats
	bool gameStarted = false;
	bool roundStarted = false;
	bool gameEnded = false;
	Vector2 startDirection = Vector2.left;


	void Start() {
		// Set up events
		ball.GotPoint += HandlePoint;
    }

    void Update() {
		if(gameEnded) {
			if(Input.GetMouseButtonDown(0)) {
				// Reset game on click
				ResetGame();
			}
		} else if(!gameStarted) {
			// Game hasn't started
			if(Input.GetMouseButtonDown(0)) {
				// Start game on click
				StartGame();
			}
		} else {
			// Game has started
			if(!roundStarted) {
				// If round hasn't started, start the round
				StartRound();
			}
			
		}
    }

	void StartGame() {
		gameStarted = true;	
	}

	void StartRound() {
		roundStarted = true;

		// Alternate start direction
		startDirection = startDirection == Vector2.left ? Vector2.right : Vector2.left;

		// Start round after 2 seconds
		Invoke("LaunchBall", 2f);
	}

	void LaunchBall() {
		ball.StartRound(startDirection);
	}

	void HandlePoint(int playerSide) {
		roundStarted = false;

		// Reset positions
		ball.ResetPosition();
		// player1.ResetPosition();
		// player2.ResetPosition();

		// Reset velocity
		ball.ResetVelocity();

		// Update score
		scores[1 - playerSide] += 1;

		if(scores[1 - playerSide] >= 9) {
			// This player just won
			gameEnded = true;
		}
	}

	void ResetGame() {
		gameStarted = false;
		roundStarted = false;
		gameEnded = false;
		scores[0] = 0;
		scores[1] = 0;

		ball.ResetPosition();
		player1.ResetPosition();
		player2.ResetPosition();
	}


	public int getPlayer1Score() {
		return scores[0];
	}

	public int getPlayer2Score() {
		return scores[1];
	}

	public bool hasGameStarted() {
		return gameStarted;
	}

	public string[] GetText() {
		if(gameEnded) {
			if(scores[0] > scores[1]) {
				return TextSnippets.WIN;
			} else {
				return TextSnippets.LOSE;
			}
		} else if(gameStarted) {
			// We are in game
			return null;
		} else {
			// Game has not started yet
			return TextSnippets.PONG;
		}
	}

	public int[] GetTextPosition() {
		if(gameEnded) {
			if(scores[0] > scores[1]) {
				return new int[] {7, 10};
			} else {
				return new int[] {5, 10};
			}
		} else if(gameStarted) {
			// We are in game
			return null;
		} else {
			// Game has not started yet
			return new int[] {1, 6};
		}
	}
}
