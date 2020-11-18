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
    public bool isCreate2 = true;
    public bool isDead = true;
    public bool isPass = false;
    public bool isTouch = false;
    bool first = false;
    bool first2 = false;
    public GameObject GameManager;
    
    public Sprite[] Images = new Sprite[3];
    public Animator Anim;
    public Animation Anima;

    private void Start()
    {
        Anim = GetComponent<Animator>();
        transform.position = new Vector2(6.7f, -3.1f);
        isCreate = true;
        GameManager = GameObject.Find("GameManager");
        kind = Random.Range(1,4);
        Anim.SetInteger("BrickKind", kind);
        GetComponent<SpriteRenderer>().sortingOrder = 6;
        StartCoroutine(CR_MoveWait());
        switch (kind)
        {
            case 1: hp = 2; score = 10.0f; time = 20; GetComponent<SpriteRenderer>().sprite = Images[0]; break;
            case 2: hp = 3; score = 30.0f; time = 30; GetComponent<SpriteRenderer>().sprite = Images[1]; break;
            case 3: hp = 1; score = 20.0f; time = -20; GetComponent<SpriteRenderer>().sprite = Images[2]; break;
        }
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
                {
                    isCreate = false;
                }
            }
            if (isCreate2)
            {
                if (transform.position.x <= 0.5f)
                {
                    isCreate2 = false;
                }
            }
        }
        
        if (isTouch)
        {
            if (isDead)
            { 
                if (hp <= 0)
                {
                    Anim.SetBool("isDead", true);
                    if (kind != 3)
                    {
                        GameManager.GetComponent<GameManager>().scoreList.Add(score);
                        GameManager.GetComponent<GameManager>().timerGauge.fillAmount += (time / 100.0f);
                    }
                    else
                    {
                        GameManager.GetComponent<GameManager>().timerGauge.fillAmount += time / 100.0f;
                        StartCoroutine(CR_Fail());

                        GameManager.GetComponent<GameManager>().fail.gameObject.SetActive(true);
                    }
                }
                isDead = false;
                GameManager.GetComponent<GameManager>().difficult += 0.5f * Time.deltaTime;
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
                else
                {
                    StartCoroutine(CR_Fail());
                    GameManager.GetComponent<GameManager>().fail.gameObject.SetActive(true);
                }
                isDead = false;
                GameManager.GetComponent<GameManager>().difficult += 0.1f;
                Instantiate(GameManager.GetComponent<GameManager>().Brick, new Vector3(6.7f, -3.1f, 0.0f), Quaternion.identity);
            }
            
        }

    }

    IEnumerator CR_Fail()
    {
        yield return new WaitForSeconds(0.1f);
        GameManager.GetComponent<GameManager>().fail.gameObject.SetActive(false);
        
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
                    transform.position = Vector2.Lerp(transform.position, new Vector2(-6, -3.1f), 5.0f * Time.deltaTime);
                else
                {
                    Destroy(gameObject);
                }
            }
            else if (isDead && isCreate )
            {
                if (!first2)
                {
                    yield return new WaitForSeconds(0.2f);
                    first2 = true;
                }
                transform.position = Vector2.Lerp(transform.position, new Vector2(0, -3.1f), 5.0f * Time.deltaTime);
            }
            yield return null;
        }
    }
    
}