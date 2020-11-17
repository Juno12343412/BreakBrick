using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float pos = Random.Range(-1.9f, 2.3f);
        transform.localPosition = new Vector2(pos, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
