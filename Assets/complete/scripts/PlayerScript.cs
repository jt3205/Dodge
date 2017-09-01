using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
    public float speed;
    public ParticleSystem explosionParticle;
    private float xInput;
    private float yInput;
    private Rigidbody2D myRigid;

    private Vector2 topRight;
    private Vector2 bottomLeft;


	void Start () {
        myRigid = GetComponent<Rigidbody2D>();
        bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
    }
	
	void Update () {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        CheckOutOfScreen();
    }

    private void CheckOutOfScreen()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, bottomLeft.x, topRight.x);
        pos.y = Mathf.Clamp(pos.y, bottomLeft.y, topRight.y);

        transform.position = pos;
    }

    private void FixedUpdate()
    {
        myRigid.velocity = new Vector2(xInput, yInput).normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            GameManager.instance.SetGameOver();
            Instantiate(explosionParticle, transform.position, Quaternion.identity);//현위치에 파티클 생성.
            Destroy(gameObject, 0.1f);//0.3초후 게임오브젝트 삭제.
        }
    }
}
