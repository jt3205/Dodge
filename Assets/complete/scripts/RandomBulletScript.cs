using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBulletScript : MonoBehaviour {
	public float attackTerm = 1; //공격 텀.
	private float currentTerm;
	public float maxDegree = 80; //회전각도
	public float bulletSpeed = 4f;
	public float bulletCnt = 40;
	private bool isFiring = false;

	void Start () {

	}

	void Update () {
		if (currentTerm <= 0 && !isFiring)  //공격시점
		{
			isFiring = true;
			StartCoroutine("FireBullet");
		}else if (!isFiring)
		{
			currentTerm -= Time.deltaTime; //델타 타임 감소.
		}
	}

	IEnumerator FireBullet()
	{
		Quaternion angle = transform.rotation;
		int lastMove = 0;
		for(int i = 0; i < bulletCnt; i++)
		{
			int random = Random.Range (0, (int)maxDegree);
			transform.Rotate(new Vector3(0, 0, 1), random-lastMove); //트랜스폼 회전.
			lastMove = random;
			BulletScript tempBscript = BulletManager.instance.GetBullet();
			tempBscript.SetDirection(transform.up * -1); //방향설정.
			tempBscript.SetSpeed(bulletSpeed); //총알 스피드 설정.
			tempBscript.transform.position = transform.position + new Vector3(0, 0, -1); //위치 설정
			tempBscript.gameObject.SetActive(true); //액티브 설정.
			yield return new WaitForSeconds(0.05f); //0.1초 간격으로 발사
		}

		transform.rotation = angle; //각도 원상복귀.
		isFiring = false;
		currentTerm = attackTerm;
	}
}
