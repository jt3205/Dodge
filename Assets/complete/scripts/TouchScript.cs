using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchScript : MonoBehaviour {

    public float touchSpeed = 0.05f;
	
	
	// Update is called once per frame
	void Update () {
	    if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDelta = Input.GetTouch(0).deltaPosition;

            transform.Translate(touchDelta.x * touchSpeed, touchDelta.y * touchSpeed, 0);
        }
	}
}
