using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour
{
    //给轨道预制体挂的
    public float maxHealth = 9.0f;
    public float health = 9.0f;
    public GameObject head;
    public GameObject tail;
    public GameObject playerOn;//该轨道上的玩家
    void Awake()
    {
        tail = transform.Find("tail").gameObject;
        head = transform.Find("head").gameObject;
        GameState.Instance.sliders.Add(transform.gameObject);
        //head.transform.position = transform.position;
        //tail.transform.localPosition=transform.GetComponent<LineRenderer>().GetPosition(1);

        //StartCoroutine(selfDestroy());
    }

    // Update is called once per frame
    void Update()
    {
        updateColor();
        selfDestroy();
    }
    void updateColor()
    {
        if(health>=6)
        {
            transform.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else if(health>=3)
        {
            transform.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        else 
        {
            transform.GetComponent<SpriteRenderer>().color = Color.red;

        }

    }
    void selfDestroy()
    {
        if(health>0)
            return;
        if(playerOn!=null)
        {
            //Debug.Log("player:" + playerOn.name + " dead");
            playerOn.GetComponent<PlayerStateController>().killPlayer();
        }
        GameState.Instance.sliders.Remove(transform.gameObject);
        Destroy(transform.parent.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.layer==10)//子弹层级
        {
            Debug.Log("被子弹撞了一次");
            health--;
        }
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.layer==9)
        {
            playerOn = other.transform.parent.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.layer==9)
        {
            playerOn = null;
        }
    }
}
