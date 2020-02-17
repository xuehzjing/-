using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class wall_logic : MonoBehaviour
{
    private float toughness;
    void Start()
    {
        transform.localScale = new Vector3(Random.Range(0.5f,2), Random.Range(0.5f, 2),0);
    }

    
    void Update()
    {
        
    }
}
