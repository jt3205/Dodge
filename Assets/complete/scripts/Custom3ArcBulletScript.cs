using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Custom3ArcBulletScript : MonoBehaviour {

	public float attackTerm = 5; //공격 텀.
	public float bulletTerm = 0.3f; //총알텀
	public int shotBulletCnt = 10; //한번할때
	public float degree = 160; //부채꼴
	public int emptyBulletNum = 1;

	private float currentTerm;
	public float bulletSpeed = 1.5f;
	public float bulletCnt = 5;
	private bool isFiring = false;

	void Start () {
		transform.Rotate(new Vector3(0, 0, 1), -degree/2);
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
		int emptyBullet = UnityEngine.Random.Range(0, shotBulletCnt-emptyBulletNum);
		float dtmp = degree / shotBulletCnt;
		for(int i = 0; i < bulletCnt; i++)
		{
			for(int j=0;j<shotBulletCnt;j++)
			{
				if (emptyBullet <= j && j <= emptyBullet + emptyBulletNum) {
					transform.Rotate(new Vector3(0, 0, 1), dtmp);
				}else{
					transform.Rotate(new Vector3(0, 0, 1), dtmp); //트랜스폼 회전.
					BulletScript tempBscript = BulletManager.instance.GetBullet();
					tempBscript.SetDirection(transform.up * -1); //방향설정.
					tempBscript.SetSpeed(bulletSpeed); //총알 스피드 설정.
					tempBscript.transform.position = transform.position + new Vector3(0, 0, -1); //위치 설정
					tempBscript.gameObject.SetActive(true); //액티브 설정.
				}
			}
			transform.rotation = angle;
			yield return new WaitForSeconds(bulletTerm); //0.1초 간격으로 발사
		}
		isFiring = false;
		currentTerm = attackTerm;
	}
}
