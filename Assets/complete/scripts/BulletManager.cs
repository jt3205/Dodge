using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour {

    public int initBulletCnt = 50;
    public GameObject bulletPrefab;
    private List<BulletScript> bList;

    public static BulletManager instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("경고! 다중 인스턴스가 생성되었습니다.");
        }
        instance = this;
    }

    private void Start()
    {
        //Awake 시점에서는 카메라가 생성되어 있지 않을 수도 있기 때문에 이 작업은 Start에서 해주는 게 맞다.
        Vector2 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector2 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));

        bList = new List<BulletScript>();
        for (int i = 0; i < initBulletCnt; i++)
        {
            GameObject bObject = Instantiate(bulletPrefab, transform); //매니저의 자식으로 프리팹 생성.
            BulletScript bScript = bObject.GetComponent<BulletScript>();
            bScript.SetBorder(bottomLeft, topRight); //각 총알에 경계선 설정.
            bList.Add(bScript); //스크립트 가져와서 넣어주고
            bObject.gameObject.SetActive(false); //액티브를 false로 둠.
        }
    }

    //리스트에서 총알 하나 가져오기.
    public BulletScript GetBullet()
    {
        BulletScript bullet = bList.Find(x => (!x.gameObject.activeSelf));
        if (bullet == null)
        {
            GameObject bObject = createBullet();
            bullet = bObject.GetComponent<BulletScript>();
        }

        return bullet;
    }

    private GameObject createBullet()
    {
        GameObject bObject = Instantiate(bulletPrefab, transform); //매니저의 자식으로 프리팹 생성.
        bList.Add(bObject.GetComponent<BulletScript>()); //스크립트 가져와서 넣어주고

        return bObject;
    }
}
