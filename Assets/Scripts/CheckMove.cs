using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CheckMove : MonoBehaviour
{
    public GameObject GameManager;
    public GameObject Gauge;
    public GameObject weakEffect;
    public GameObject middleEffect;
    public GameObject highEffect;
    int Damage = 0;
    bool first = true;
    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = new Vector2(4.4f, 0);
        GameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (!transform.parent.GetComponentInParent<Brick>().isCreate2)
        {
            if (GameManager.GetComponent<GameManager>().isAttack)
            {
                transform.parent.GetComponentInParent<Brick>().isTouch = true;
            }
            if (!transform.parent.GetComponentInParent<Brick>().isTouch)
            {
                if (transform.localPosition.x >= -4.4f)
                    transform.localPosition = new Vector2(transform.localPosition.x - ((6.0f * GameManager.GetComponent<GameManager>().difficult)) * Time.deltaTime, 0);
                else
                    transform.parent.GetComponentInParent<Brick>().isPass = true;
            }
            else
            {
                if (first)
                {
                    if (transform.localPosition.x >= (Gauge.transform.localPosition.x - 0.45f) && transform.localPosition.x <= (Gauge.transform.localPosition.x + 0.45f))
                        Damage = 3;
                    else if (transform.localPosition.x >= (Gauge.transform.localPosition.x - 1.64f) && transform.localPosition.x <= (Gauge.transform.localPosition.x + 1.64f))
                        Damage = 2;
                    else
                        Damage = 1;

                    transform.parent.GetComponentInParent<Brick>().hp -= Damage;
                    switch (Damage)
                    {
                        case 1: Instantiate(weakEffect, transform.parent.GetComponentInParent<Brick>().transform.localPosition, Quaternion.identity); break;
                        case 2: Instantiate(middleEffect, transform.parent.GetComponentInParent<Brick>().transform.localPosition, Quaternion.identity); break;
                        case 3: Instantiate(highEffect, transform.parent.GetComponentInParent<Brick>().transform.localPosition, Quaternion.identity); break;
                    }
                    
                    

                    GameManager.GetComponent<GameManager>().isAttack = false;
                    Debug.Log(Damage);
                    first = false;
                }
            }
        }
    }
}