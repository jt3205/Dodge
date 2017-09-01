using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineBulletScript : MonoBehaviour {
    public float attackTerm; //공격 텀.
    private float currentTerm; //현재 텀    
    public float bulletSpeed = 2f;
    public float bulletCnt = 20;
    private bool isFiring = false;

	
	// Update is called once per frame
	void Update () {
        if (currentTerm <= 0 && !isFiring)  //공격시점
        {
            isFiring = true;
            StartCoroutine("FireBullet");
        }
        else if (!isFiring)
        {
            currentTerm -= Time.deltaTime; //델타 타임 감소.
        }
    }

    IEnumerator FireBullet()
    {
        for (int i = 0; i < bulletCnt; i++)
        {
            BulletScript tempBscript = BulletManager.instance.GetBullet();
            tempBscript.SetDirection(transform.up * -1); //방향설정.
            tempBscript.SetSpeed(bulletSpeed); //총알 스피드 설정.
            tempBscript.transform.position = transform.position + new Vector3(0, 0, -1); //위치 설정
            tempBscript.gameObject.SetActive(true); //액티브 설정.
            yield return new WaitForSeconds(0.1f); //0.1초 간격으로 발사
        }
        isFiring = false;
        currentTerm = attackTerm;
    }
}
