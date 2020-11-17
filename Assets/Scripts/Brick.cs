using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int kind;
    public int hp;
    public float score;
    public int time;
    public int Damage = 0;
    public bool isCreate = true;
    public bool isDead = true;
    public bool isPass = false;
    public bool isTouch = false;
    bool first = false;
    bool first2 = false;
    public GameObject GameManager;
    public Sprite[] Images = new Sprite[3];

    private void Start()
    {
        transform.position = new Vector2(6.7f, -3.1f);
        isCreate = true;
        GameManager = GameObject.Find("GameManager");
        kind = Random.Range(1,4);
        switch (kind)
        {
            case 1: hp = 2; score = 10.0f; time = 10; GetComponent<SpriteRenderer>().sprite = Images[0]; break;
            case 2: hp = 3; score = 30.0f; time = 15; GetComponent<SpriteRenderer>().sprite = Images[1]; break;
            case 3: hp = 1; score = 20.0f; time = -20; GetComponent<SpriteRenderer>().sprite = Images[2]; break;
        }
        GetComponent<SpriteRenderer>().sortingOrder = 6;
        StartCoroutine(CR_MoveWait());
    }

    private void Update()
    {
        if(!GameManager.GetComponent<GameManager>().isGameStart)
            Destroy(gameObject);

        if (isDead)
        {
            if (isCreate)
            {
                if (transform.position.x <= 0.1f)
                    isCreate = false;
            }
        }
        
        if (isTouch)
        {
            if (isDead)
            { 
                if (hp <= 0)
                {
                    if (kind != 3)
                    {
                        GameManager.GetComponent<GameManager>().scoreList.Add(score);
                        GameManager.GetComponent<GameManager>().timerGauge.fillAmount += time / 100.0f;
                    }
                    else
                    {
                        GameManager.GetComponent<GameManager>().timerGauge.fillAmount += time / 100.0f;
                    }
                }
                isDead = false;
                GameManager.GetComponent<GameManager>().difficult += 0.05f;
                Instantiate(GameManager.GetComponent<GameManager>().Brick, new Vector3(6.7f, -3.1f, 0.0f), Quaternion.identity);
            }

            

        }
        if (isPass)
        {
            if (isDead)
            {
                if (kind == 3)
                {
                    GameManager.GetComponent<GameManager>().scoreList.Add(score);
                    GameManager.GetComponent<GameManager>().timerGauge.fillAmount -= time / 100.0f; 
                }
                
                isDead = false;
                GameManager.GetComponent<GameManager>().difficult += 0.05f;
                Instantiate(GameManager.GetComponent<GameManager>().Brick, new Vector3(6.7f, -3.1f, 0.0f), Quaternion.identity);
            }
            
        }

    }

    IEnumerator CR_MoveWait()
    {
        while (true)
        {
            if (isTouch || isPass)
            {
                if (!first)
                {
                    yield return new WaitForSeconds(0.2f);
                    first = true;
                }
                if (transform.position.x >= -5.9)
                    transform.position = Vector2.Lerp(transform.position, new Vector2(-6, -3.1f), 0.04f);
                else
                    Destroy(gameObject);
            }
            else if (isDead && isCreate )
            {
                if (!first2)
                {
                    yield return new WaitForSeconds(0.2f);
                    first2 = true;
                }
                transform.position = Vector2.Lerp(transform.position, new Vector2(0, -3.1f), 0.04f);
            }
            yield return null;
        }
    }
    
}