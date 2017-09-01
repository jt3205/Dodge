using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseArcBulletScript : MonoBehaviour {

    public float attackTerm; //공격 텀.
    private float currentTerm;
    public float degree = 10; //회전각도
    public float bulletSpeed = 2f;
    public float bulletCnt = 20;
    public float pulseTerm = 1f;
    public float pulseCnt = 3; //몇개의 펄스를 쏠지.
    public bool isTurning = false;
    private bool isFiring = false;

    void Start()
    {

    }

    void Update()
    {
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
        Quaternion angle = transform.rotation;
        for (int i = 0; i < pulseCnt; i++)
        {
            if (isTurning)
            {
                //교차사격이 활성화되어 있다면
                transform.Rotate(new Vector3(0, 0, -30 * (i % 2)));
            }
            for (int j = 0; j < bulletCnt; j++)
            {
                transform.Rotate(new Vector3(0, 0, 1), degree); //트랜스폼 회전.
                BulletScript tempBscript = BulletManager.instance.GetBullet();
                tempBscript.SetDirection(transform.up * -1); //방향설정.
                tempBscript.SetSpeed(bulletSpeed); //총알 스피드 설정.
                tempBscript.transform.position = transform.position + new Vector3(0, 0, -1); //위치 설정
                tempBscript.gameObject.SetActive(true); //액티브 설정.
            }
            transform.rotation = angle; //각도 원상복귀.

            yield return new WaitForSeconds(pulseTerm); //펄스텀을 쉬고 다시 발사.
        }

        isFiring = false;
        currentTerm = attackTerm;
    }
}
