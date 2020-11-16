using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int kind;
    public int hp;
    public int score;
    public int time;
    public bool isCreate = true;
    public GameObject GameManager;
    public Sprite[] Images = new Sprite[3];

    private void Start()
    {
        transform.position = new Vector2(6.7f, -3.72f);
        isCreate = true;
        GameManager = GameObject.Find("GameManager");
        kind = Random.Range(1, 3);
        switch (kind)
        {
            case 1: hp = 1; score = 100; time = 10; GetComponent<SpriteRenderer>().sprite = Images[0]; break;
            case 2: hp = 3; score = 300; time = 15; GetComponent<SpriteRenderer>().sprite = Images[1]; break;
            case 3: hp = 2; score = 0; time = -10; GetComponent<SpriteRenderer>().sprite = Images[2]; break;
        }
        GetComponent<SpriteRenderer>().sortingOrder = -4;
    }

    private void Update()
    {
        if (isCreate)
        {
            transform.position = Vector2.Lerp(transform.position, new Vector2(0, -3.72f), 0.3f);
            if (transform.position.x <= 0.01f)
                isCreate = false;
        }
        else
        {
            if (GameManager.GetComponent<GameManager>().GetisAttack())
            {
                hp--;
                if (hp <= 0)
                {
                    GameManager.GetComponent<GameManager>().scoreList.Add(score);
                    Destroy(gameObject);
                }
                GameManager.GetComponent<GameManager>().isAttack = false;
            }
        }
    }
}
