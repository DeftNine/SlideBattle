using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
    //玩家状态控制器，总机，每个玩家一个
    public int playerID = 1;
    public bool hitable = true;
    public bool ifDied = false;
    public MoveController mc;
    public FireController fc;
    public PUIController puc;
    public PSoundController psc;
    void Awake()
    {
        mc = transform.GetComponent<MoveController>();
        fc = transform.GetComponent<FireController>();
        puc = transform.GetComponent<PUIController>();
        psc = transform.GetComponent<PSoundController>();
    }
    void Start()
    {
        GameState.Instance.players.Add(transform.gameObject);
        init();
    }
    public void init()
    {
        hitable = true;
        mc.centerPoint = GameState.Instance.getResPos(transform.gameObject);
        mc.moveable = true;
        fc.fireable = true;
        fc.rotLeft = false;
        fc.rotRight = false;
        ifDied = false;
        transform.position = mc.centerPoint;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void killPlayer()
    {
        Debug.Log("玩家死亡:" + transform.gameObject.name);
        psc.playDead();
        ifDied = true;
        hitable = false;
        puc.deathCount++;
        StartCoroutine(doRestart());
        //重生点选择（暂未确定是固定点还是玩家自选）(暂定为重新开始对局)
        //重生无敌时间
        //统计数据更新
    }
    IEnumerator doRestart()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("重新开始游戏");
        GameState.Instance.restartGame();
    }
}
