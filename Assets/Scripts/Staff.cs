using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour
{
    int keywordMax, tempkeywordMax;
    float makeTime, restTime;

    public int emotion, emotionMax;

    public List<int> keywordLike = new List<int>();
    public List<int> keywordHate = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        emotionMax = Random.Range(GameManager.instance.emotionMaxMin, GameManager.instance.emotionMaxMax);
        emotion = Random.Range((int)(emotionMax * 0.3), (int)(emotionMax * 0.7));

        makeTime = GameManager.instance.makeTime;
        restTime = GameManager.instance.restTime;

        RandomKeyword();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RandomKeyword()
    {   
        keywordMax = Random.Range(GameManager.instance.keywordMax / 2, GameManager.instance.keywordMax);
        int likes = Random.Range(2, keywordMax);
        tempkeywordMax = keywordMax;

        for (int i = 0; i < keywordMax; i++)
        {
            int tempkeyword = Random.Range(0, GameManager.instance.keyword.Length);

            if (i < likes)
            {   
                if (!keywordLike.Contains(tempkeyword)) keywordLike.Add(tempkeyword);
                else tempkeywordMax--;
            }
            else
            {
                if (keywordLike.Contains(tempkeyword))
                {   
                    tempkeywordMax -= 2;
                    keywordLike.Remove(tempkeyword);
                }
                else if (!keywordHate.Contains(tempkeyword)) keywordHate.Add(tempkeyword);
                else tempkeywordMax--;
            }
            keywordMax = tempkeywordMax;
        }
        keywordLike.Sort();
        keywordHate.Sort();
    }

    public void SetInfo()
    {
        Info.instance.emotion = emotion;
        Info.instance.emotionMax = emotionMax;

        for (int i = 0; i < 7; i++)
        {
            if (i < keywordLike.Count) Info.instance.like[i] = GameManager.instance.keyword[keywordLike[i]];
            else Info.instance.like[i] = "";

            if (i < keywordHate.Count) Info.instance.hate[i] = GameManager.instance.keyword[keywordHate[i]];
            else Info.instance.hate[i] = "";
        }
    }


}
