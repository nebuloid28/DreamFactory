using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool isLoading = true;

    public int xLimit = 2;  //min 1, max 9
    public int zLimit = 2;
    public GameObject factory;
    public GameObject staff;
    public static GameManager instance;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(GameStart());
    }

    IEnumerator GameStart()
    {
        if (isLoading)
        {
            isLoading = false;
            FactoryGen();
            yield return new WaitForSeconds(1);
            StaffGen();
        }
    }

    private void FactoryGen()
    {   //최대 방 수에 맞춰서 방 생성
        for (int j = 0; j < zLimit; j++)
        {
            for (int i = 0; i < xLimit; i++)
            {
                //Debug.Log("factory spawn!");

                GameObject factoryObj = Instantiate(factory);
                factoryObj.transform.SetParent(GameObject.Find("Factory").transform);

                factoryObj.transform.position = new Vector3(i * 100, 0, j * 100);

                //번호에 맞춰 공장 이름 변경
                if (j == 0)
                {
                    factoryObj.name = "Factory_0" + (i + 1);
                }
                else
                {
                    factoryObj.name = "Factory_" + (j * 10 + i + 1);
                }
            }
        }
    }

    private void StaffGen()
    {   //실행시 공장의 모든 방에 직원 4명씩 생성
        for (int j = 0; j < GameManager.instance.zLimit; j++)
        {
            for (int i = 1; i <= GameManager.instance.xLimit; i++)
            {
                for (int k = 1; k <= 4; k++)
                {
                    string factoryName;
                    if (j == 0) factoryName = "Factory_0" + i;
                    else factoryName = "Factory_" + (j * 10 + i);

                    GameObject staffObj = Instantiate(staff);
                    staffObj.transform.SetParent(GameObject.Find("Staff").transform);
                    Vector3 spawnPos = GameObject.Find("Factory/" + factoryName + "/Bed/Bed_" + k).transform.position;
                    staffObj.transform.position = new Vector3(spawnPos.x, spawnPos.y + 1, spawnPos.z);
                    // 스태프 이름 변경
                    if (j == 0) staffObj.name = "Staff_0" + i + "_" + k;
                    else staffObj.name = "Staff_" + (j * 10 + i) + "_" + k;
                }
            }
        }
    }
}
