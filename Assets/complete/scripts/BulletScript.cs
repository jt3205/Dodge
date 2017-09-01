using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {
    private Rigidbody2D myRigid;
    public Vector2 direction;
    public float speed;
    private Vector2 bottomLeft;  //좌하단
    private Vector2 topRight;  //우상단

	void Start () {
        myRigid = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
        CheckOutofScreen();
	}

    private void FixedUpdate()
    {
        myRigid.velocity = direction.normalized * speed;
    }

    //화면밖으로 나갔는지 체크하는 함수.
    private void CheckOutofScreen()
    {
        if(transform.position.x > topRight.x || transform.position.x < bottomLeft.x || transform.position.y > topRight.y || transform.position.y < bottomLeft.y)
        {
            //화면밖으로 나갔을 경우.
            gameObject.SetActive(false); //자기 자신을 Active를 false로 변경함.
        }
    }

    //Setter 함수들
    public void SetBorder(Vector2 bottomLeft, Vector2 topRight)
    {
        this.bottomLeft = bottomLeft;
        this.topRight = topRight;
    }

    public void SetDirection(Vector2 dir)
    {
        this.direction = dir;
    }

    public void SetSpeed(float value)
    {
        this.speed = value;
    }
}
