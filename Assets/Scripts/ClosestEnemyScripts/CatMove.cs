using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMove : MonoBehaviour {

	float dirX, dirY, movespeed = 5f;

	// Update is called once per frame
	void Update () {
		dirX = Input.GetAxis ("Horizontal") * movespeed * Time.deltaTime;
		dirY = Input.GetAxis ("Vertical") * movespeed * Time.deltaTime;

		transform.position = new Vector2 (transform.position.x + dirX, transform.position.y + dirY);
	}
}
