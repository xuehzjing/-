using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCreater : MonoBehaviour {
    public GameObject Foods;
    public GameObject food;
    public GameObject food1;
    public GameObject food3;
    public GameObject food5;
    public GameObject wall;
    public float xmin = -10f;
    public float xmax = 10f;
    public float ymin = -10f;
    public float ymax = 10f;
    public float CreateInterval = 3f;
    public int FoodFertility = 30; //食物分数总量
    public float MinFoodGap = 5f;

    private Queue<Vector3> FoodPosQueue = new Queue<Vector3> ();

    void Start () { }

    void Update () {
        
        InvokeRepeating ("CreateFood", 0, CreateInterval);
    }

    void CreateFood () {
        if (Foods.transform.childCount < FoodFertility) {
            Debug.Log (Foods.transform.childCount);
            FillInQueue ();
            Vector3 pos = FoodPosQueue.Dequeue ();
            //Instantiate (food, pos, Quaternion.identity, Foods.transform);
            int color = Random.Range (0, 3);
            if (color == 0) { Instantiate (food1, pos, Quaternion.identity, Foods.transform); }
            if (color == 1) { Instantiate (food3, pos, Quaternion.identity, Foods.transform); }
            if (color == 2) { Instantiate (food5, pos, Quaternion.identity, Foods.transform); }
        }
    }

    bool ValidDistance (Vector3 v1, Vector3 v2) {
        return Vector3.Distance (v1, v2) > MinFoodGap;
    }

    bool PointInArea (Vector3 point, Vector3 areaCenter, Vector3 areaScale) {
        return point.x < areaCenter.x + areaScale.x && point.x > areaCenter.x + areaScale.x && point.y < areaCenter.y + areaScale.y && point.y > areaCenter.y + areaScale.y;
    }

    void FillInQueue () {
        bool flag;
        while (FoodPosQueue.Count < 10) {
            flag = true;
            float x = Random.Range (xmin, xmax);
            float y = Random.Range (ymin, ymax);
            Vector3 newPos = new Vector3 (x, y, 0);
            //Debug.Log (newPos);
            if (FoodPosQueue.Count > 0) {
                foreach (Vector3 pos in FoodPosQueue) {
                    if (!ValidDistance (pos, newPos)) {
                        flag = false;
                        break;
                    }
                }
                if (flag) {
                    FoodPosQueue.Enqueue (newPos);
                    //Debug.Log (newPos);
                    //Debug.Log (FoodPosQueue.Count);
                }
            } else {
                FoodPosQueue.Enqueue (newPos);
                //Debug.Log (newPos);
                //Debug.Log (FoodPosQueue.Count);
            }
            
        }
    }
}