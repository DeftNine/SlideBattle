using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    //移动控制器，主要是把玩家限制在轨道上
    PlayerStateController pm;
    public bool moveable = true;
    [Header("移动参数")]
    public float speed = 10f;
    //public Vector3 respawnPos;
    public bool limitDirection;//是否限制方向
    public Transform newPos;
    [Header("轨道")]
    public Transform slide;
    public Transform head;
    public Transform tail;
    public Vector2 direction;
    public Vector2 centerPoint;
    void Awake()
    {
        pm = transform.GetComponent<PlayerStateController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pm.ifDied) return;
        if(!moveable) return;
        updateDirection();
        //limitCenter();
        movePlayer();
        Debug.DrawLine(centerPoint, transform.position, Color.red);
    }
     void LateUpdate() {
        if(!moveable) return;
        transform.position = newPos.position;
        transform.position = new Vector3(
        Mathf.Clamp(transform.position.x, centerPoint.x - 0.2f, centerPoint.x + 0.2f),
        Mathf.Clamp(transform.position.y, centerPoint.y - 0.2f, centerPoint.y + 0.2f),
        -5);
    }
    void movePlayer()
    {
        newPos=transform;
        float moveX = 0;
        float moveY = 0;
        if (pm.playerID == 1)
        {
            moveX = Input.GetAxis("player1X");
            moveY = Input.GetAxis("player1Y");
        }
        else if(pm.playerID==2)
        {
            moveX = Input.GetAxis("player2X");
            moveY = Input.GetAxis("player2Y");
        }
        if (limitDirection == false)
        {
            float rat = 1;
            //if(moveX!=0&&moveY!=0) rat=1 / 5;
            newPos.Translate(new Vector2(moveX * speed * Time.deltaTime*rat, 0));
            newPos.Translate(new Vector2(0, moveY * speed * Time.deltaTime*rat));
        }
        else
        {
            if(head==null||tail==null) return;//有时候玩家在轨道上死亡，重置时轨道销毁会报错，虽然没影响
            //unity自行处理了垂直情况（即斜率不存在）因此删除了几种情况判定，用一种方法处理所有情况
            float cosX = Mathf.Abs(head.position.x-tail.position.x)/Vector2.Distance(head.position, tail.position);
            float cosY = Mathf.Abs(head.position.y-tail.position.y)/Vector2.Distance(head.position, tail.position);
            int fixX=1, fixY = 1;
            if(direction.x<0)
                fixX = -1;
            if(direction.y<0)
                fixY = -1;
            newPos.Translate(moveX*speed*cosX*Time.deltaTime*direction*fixX);
            newPos.Translate(moveY*speed*cosY*Time.deltaTime*direction*fixY);
        }
    }
    void updateDirection()
    {
        if(slide!=null)
        direction = (head.position - tail.position).normalized;
    }
    void limitCenter()
    {
        if(limitDirection==true&&Vector2.Distance(transform.position,centerPoint)>0.4)
        {
            transform.position = new Vector3(centerPoint.x,centerPoint.y,-5);
        }
    }
}
