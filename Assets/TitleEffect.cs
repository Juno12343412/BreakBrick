using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleEffect : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(CR_ShakeOutline());
    }

    void Update()
    {
    }
    IEnumerator CR_ShakeOutline()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.03f);
            GetComponent<Outline>().effectDistance = new Vector2(Random.Range(1, 35), Random.Range(1, 35));
        }
    }
}
