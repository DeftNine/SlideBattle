using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //给子弹预制体挂的
    GameObject boom;
    bool actived=true;
    void Awake()
    {
        boom = (GameObject)Resources.Load("Boom");
        GameState.Instance.bullets.Add(transform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        outBounds();
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.layer==10)//与子弹碰撞
        {
            Debug.Log("[Hit]:" + this.transform.name + " with " + other.transform.name);
            GameObject obj = Instantiate(boom);
            obj.transform.position = transform.position;
            GameState.Instance.bullets.Remove(transform.gameObject);
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.layer==9)//脱离碰撞盒后激活子弹 已废弃，原本是从玩家位置发射，现在是从箭头位置
        {
            actived = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(actived&&other.gameObject.layer==9)//激活的子弹碰到玩家
        {
            if(other.gameObject.transform.parent.GetComponent<PlayerStateController>().hitable==false) return;
            Debug.Log("[Hit]:" + this.transform.name + " with " + other.transform.name);
            GameObject obj = Instantiate(boom);
            obj.transform.position = transform.position;
            other.gameObject.transform.parent.GetComponent<PlayerStateController>().killPlayer();
            GameState.Instance.bullets.Remove(transform.gameObject);
            Destroy(this.gameObject);
        }
    }
    void outBounds()//出界后自然销毁，不知道为什么，有时会越过碰撞盒，目前采用的是边界限制，后面可能会改
    {
        if(transform.position.x<GameState.Instance.boundLeft.x||transform.position.x>GameState.Instance.boundRight.x)
        {
            GameState.Instance.bullets.Remove(transform.gameObject);
            Destroy(this.gameObject);
        }
        if(transform.position.y>GameState.Instance.boundUp.y||transform.position.x<GameState.Instance.boundDown.y)
        {
            GameState.Instance.bullets.Remove(transform.gameObject);
            Destroy(this.gameObject);
        }
    }
}
