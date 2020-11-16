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
    bool gameOver = false;
    float Score = 0;
    float HighScore = 0;
    public List<int> scoreList = null;
    public bool isAttack = false;

    private void Start()
    {
        timerGauge.fillAmount = 1;
        titleCanvas.enabled = true;
        ingameCanvas.enabled = false;
        gameOverCanvas.enabled = false;
        gameOver = false;
        isAttack = false;
    }
    private void Update()
    {
        if(isGameStart)
        {
            if (timerGauge.fillAmount <= 0)
            {
                timerGauge.fillAmount = 0;
                gameOver = true;
                GameOver();
                return;
            }
            if(Input.GetMouseButton(1))
            {

            }
        }
    }

    public IEnumerator CR_TimerGauge()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            timerGauge.fillAmount -= 0.01f;
            //Debug.Log(timerGauge.fillAmount);
        }
    }

    bool isGameStart = false;
    public void GameStart()
    {
        timerGauge.fillAmount = 1;

        StartCoroutine(CR_TimerGauge());
        isGameStart = true;
        titleCanvas.enabled = false;
        ingameCanvas.enabled = true;
        gameOverCanvas.enabled = false;

        gameOver = false;
        isAttack = false;
    }

    public void GameReStart()
    {
        isGameStart = false;
        titleCanvas.enabled = true;
        ingameCanvas.enabled = false;
        gameOverCanvas.enabled = false;

        gameOver = false;
    }

    public void GameOver()
    {
        gameOverCanvas.enabled = true;
        isGameStart = false;
        StopAllCoroutines();
    }
    public bool GetisAttack()
    {
        return isAttack;
    }
}
