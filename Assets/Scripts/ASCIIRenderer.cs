using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASCIIRenderer : MonoBehaviour {

	public TMPro.TMP_Text text;
	public int width;
	public int height;

	public SolidPongObject[] boxesToRender;

	public GameManager gameManager;

	// The string array used to hold the text
	char[] textData;

    void Start(){
		// Create the string array
		textData = new char[(width+1)*height];

		ClearBoard();

		// Initialize the text area
		text.SetText(textData);
    }

    void Update(){
		// Clear text
		ClearBoard();

		string[] snippet = gameManager.GetText();

		if(snippet == null) {
			// Add in scores
			string[] p1Score = TextSnippets.NUMBER[gameManager.getPlayer1Score()];
			string[] p2Score = TextSnippets.NUMBER[gameManager.getPlayer2Score()];

			for(int i = 0; i < p1Score.Length; i++) {
				string str = p1Score[i];
				for(int j = 0; j < str.Length; j++) {
					SetText(j + 10, i + 2, str[j]);
				}
			}

			for(int i = 0; i < p2Score.Length; i++) {
				string str = p2Score[i];
				for(int j = 0; j < str.Length; j++) {
					SetText(j + width - 13, i + 2, str[j]);
				}
			}


			// Render areas
			for(int i = 0; i < boxesToRender.Length; i++) {
				RenderBox(boxesToRender[i].GetBounds());
			}

			
		} else {
			// We're in a menu, render the snippet
			int x = gameManager.GetTextPosition()[1];
			int y = gameManager.GetTextPosition()[0];

			for(int i = 0; i < snippet.Length; i++) {
				string str = snippet[i];
				for(int j = 0; j < str.Length; j++) {
					SetText(j + y, i + x, str[j]);
				}
			}
		}

		text.SetText(textData);
	}

	void ClearBoard() {
		for(int i = 0; i < (width+1) * height; i++) {
			if((i % (width + 1))%4 == 0) {
				textData[i] = '.';
			} else if((i / (width + 1))%4 == 0) {
				textData[i] = '.';
			} else {
				textData[i] = ' ';
			}
			
		}

		for(int i = 0; i < height; i++) {
			textData[i * (width+1) + width] = '\n';
		}
	}

	void RenderBox(Rect bounds) {
		float letterWidth = (float)Screen.width / (float)width;
		float letterHeight = (float)Screen.height / (float)height;

		Vector3 minBound = Camera.main.WorldToScreenPoint(new Vector3(bounds.xMin, bounds.yMin, 0));
		Vector3 maxBound = Camera.main.WorldToScreenPoint(new Vector3(bounds.xMax, bounds.yMax, 0));

		int minX = (int)(minBound.x / letterWidth);
		float minXWeight = minBound.x / letterWidth - minX;

		int maxX = (int)(maxBound.x / letterWidth);
		float maxXWeight = maxBound.x / letterWidth - maxX;

		int minY = (int)(minBound.y / letterHeight);
		float minYWeight = minBound.y / letterHeight - minY;

		int maxY = (int)(maxBound.y / letterHeight);
		float maxYWeight = maxBound.y / letterHeight - maxY;

		for(int i = minX; i <= maxX; i++) {
			float weight = 1f;

			if(i == minX) {
				weight = minXWeight;
			} else if(i == maxX) {
				weight = maxXWeight;
			}

			for(int j = minY; j <= maxY; j++) {

				if(j == minX) {
					weight *= minYWeight;
				} else if(j == maxY) {
					weight *= maxYWeight;
				}

				if(i >= 0 && i < width && j >= 0 && j < height) {
					// Camera inverts y coords compared to world space, so flip back
					int y = (height - 1 - j);
					SetText(i, y, GetCharFromWeight(weight));
				}
			}
		}
	}

	void SetText(int x, int y, char c) {
		textData[y * (width+1) + x] = c;
	}

	char GetCharFromWeight(float weight) {
		if(weight > 0.6) {
			return '8';
		} else if(weight > 0.3) {
			return 'O';
		} else {
			return 'x';
		}
	}
}
