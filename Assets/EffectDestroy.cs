using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CR_Destroy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CR_Destroy()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }
}
