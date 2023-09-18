using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    //已弃用，原本轨道采用线渲染器的形式，后来感觉不好调整外形，就弃用了
    public GameObject head;
    public GameObject tail;
    public GameObject playerOn;//该轨道上的玩家
    void Awake()
    {
        tail = transform.Find("tail").gameObject;
        head = transform.Find("head").gameObject;
        //head.transform.position = transform.position;
        //tail.transform.localPosition=transform.GetComponent<LineRenderer>().GetPosition(1);

        //StartCoroutine(selfDestroy());
    }

    // Update is called once per frame
    void Update()
    {
    }
    IEnumerator selfDestroy()
    {
        yield return new WaitForSeconds(10);
        if(playerOn!=null)
        {
            Debug.Log("player:" + playerOn.name + " dead");
        }
        Destroy(this.gameObject);
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
