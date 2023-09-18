using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCheck : MonoBehaviour
{
    //移动检测，防止玩家越界
    MoveController target;
    public bool check;
    public List<GameObject> objList=new List<GameObject>();
    public int count;
    void Awake()
    {
        target = this.transform.parent.GetComponent<MoveController>();
    }

    // Update is called once per frame
    void Update()
    {
        count = objList.Count;
        directionLimited();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        //Debug.Log("enter:" + other.transform.name);
        if(other.gameObject.layer==8)
        objList.Add(other.gameObject);
        if(count==1)
        target.centerPoint=other.ClosestPoint(target.newPos.position);
    }
    private void OnTriggerStay2D(Collider2D other) {
        //if(count==1)
        if(other.gameObject.layer==8)
        target.centerPoint=other.ClosestPoint(target.newPos.position);
        // else
        // target.centerPoint=this.transform.position;
    }
     private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.layer==8)
        objList.Remove(other.gameObject);
    }
    void directionLimited()
    {
        if(count==1)
        {
            target.limitDirection = true;
            target.slide = objList[0].transform;
            target.head = target.slide.Find("head");
            target.tail = target.slide.Find("tail");
        }
        else
        {
            target.limitDirection = false;
        }
    } 
}
