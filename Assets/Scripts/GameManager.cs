using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public string[] keyword = new string[25]
    {   
        "개", "고양이", "벌", "뱀", "거미",
        "민트초코", "두리안", "오이", "땅콩", "우유",
        "인형", "라이터", "열쇠", "방망이", "사다리",
        "요리사", "의사", "변호사", "경찰", "광대",
        "카페", "공원", "바다", "산", "광장"
    };

    bool isLoading = true;

    public int xLimit = 2;
    public int zLimit = 2;
    public int emotionMaxMin = 100;
    public int emotionMaxMax = 150;
    public int keywordMax = 10;
    public int dreamKeywordMax = 10;
    public float makeTime = 4.0f;
    public float restTime = 5.0f;
    public GameObject factory, info, staff;
    public RaycastHit hit;
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

        ViewInfo();
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

    void ViewInfo()
    {
        //메인카메라에서 마우스포인터 위치로 레이를 발사
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);

        if (Physics.Raycast(ray, out hit))
        {   //레이어가 Staff인 오브젝트에 적중시 Staff Info 활성화, DrugManager와 OrderManager의 변수를 활성화
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Staff"))
            {
                //Debug.Log("Staff Find!");
                info.SetActive(true);
                hit.transform.SendMessage("SetInfo", SendMessageOptions.DontRequireReceiver);
            }
            else
            {   //미적중시 비활성화
                info.SetActive(false);
            }
        }
    }
}
