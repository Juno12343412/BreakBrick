using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public Image timerGauge = null;
    public Canvas titleCanvas = null;
    public Canvas ingameCanvas = null;
    public Canvas gameOverCanvas = null;
    public GameObject PlayerHand = null;
    public GameObject Brick = null;
    public Text scoreText = null;
    //public GameObject Brick = null;
    public Animator PlayerAnim = null;
    bool gameOver = false;
    float Score = 0;
    float HighScore = 0;
    public List<float> scoreList = null;
    public bool isAttack = false;
    public float difficult = 1.0f;
    float temp = 0;
    private void Start()
    {
        timerGauge.fillAmount = 1;
        titleCanvas.gameObject.SetActive(true);
        ingameCanvas.gameObject.SetActive(false);
        gameOverCanvas.gameObject.SetActive(false);
        gameOver = false;
        isAttack = false;
        PlayerAnim = PlayerHand.GetComponent<Animator>();
        PlayerAnim.SetBool("Title", true);

    }
    private void Update()
    {
        if (isGameStart)
        {
            if(scoreList.Count != 0)
            {
                temp += scoreList[0];
                scoreList.RemoveAt(0);
                Debug.Log(temp);
            }
            Score = Mathf.Lerp(Score, temp + 0.1f, 0.1f);

            if (timerGauge.fillAmount <= 0)
            {
                timerGauge.fillAmount = 0;
                gameOver = true;
                GameOver();
                return;
            }
            if (Input.GetMouseButtonDown(0))
            {
                isAttack = true;
                PlayerAnim.SetBool("Attack", true);
            }
        }
        scoreText.text = ((int)Score).ToString();
    }
    
    public IEnumerator CR_TimerGauge()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            timerGauge.fillAmount -= 0.01f;
        }
    }

    public bool isGameStart = false;
    public void GameStart()
    {
        timerGauge.fillAmount = 1;

        StartCoroutine(CR_TimerGauge());
        isGameStart = true;
        titleCanvas.gameObject.SetActive(false);
        ingameCanvas.gameObject.SetActive(true);
        gameOverCanvas.gameObject.SetActive(false);

        gameOver = false;
        isAttack = false;
        PlayerAnim.SetBool("Title", false);
        Instantiate(Brick, new Vector3(6.7f, -3.1f, 0.0f), Quaternion.identity);


    }

    public void GameReStart()
    {
        isGameStart = false;
        titleCanvas.gameObject.SetActive(true);
        ingameCanvas.gameObject.SetActive(false);
        gameOverCanvas.gameObject.SetActive(false);

        gameOver = false;
        PlayerAnim.SetBool("Title", true);

    }

    public void GameOver()
    {
        gameOverCanvas.gameObject.SetActive(true);
        ingameCanvas.gameObject.SetActive(false);
        titleCanvas.gameObject.SetActive(false);

        isGameStart = false;
        StopAllCoroutines();
        PlayerAnim.SetBool("Title", false);

    }
    public bool GetisAttack()
    {
        return isAttack;
    }

    
}
