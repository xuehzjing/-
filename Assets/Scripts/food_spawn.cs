using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class food_spawn : MonoBehaviour
{
    public GameObject food_prefab;
    public float spawn_time;
    public float width=10;
    public float height=10;
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Place()
    {
        GameObject a = Instantiate(food_prefab) as GameObject;
        a.transform.position = new Vector2(Random.Range(-0.5f * width, +0.5f * width), Random.Range(-0.5f * height, +0.5f * height));
    }
    IEnumerator Spawn() {
        while (true) {
            yield return new WaitForSeconds(spawn_time);
            Place();
        }
    }
}
