using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : singleton<GameState>
{
    //游戏状态控制机，唯一单例
    public List<GameObject> players =new List<GameObject>();
    public List<Transform> respownPos = new List<Transform>();
    public List<GameObject> sliders = new List<GameObject>();
    public List<GameObject> bullets = new List<GameObject>();
    public bool ifPaused = false;
    public GameObject boundRoot;
    public Vector3 boundLeft;
    public Vector3 boundRight;
    public Vector3 boundUp;
    public Vector3 boundDown;

    private void Start() {
        boundRoot = GameObject.Find("Map/Bounds");
        refreshData();
    }
    void Update()
    {
        pauseGame();
    }
    void refreshData()
    {
        boundLeft = boundRoot.transform.Find("left").transform.position;
        boundRight = boundRoot.transform.Find("right").transform.position;
        boundUp = boundRoot.transform.Find("up").transform.position;
        boundDown = boundRoot.transform.Find("down").transform.position;
    }
    public void restartGame()//重新开始对局
    {
        for (int i = 0; i < players.Count;i++)
        {
            players[i].transform.GetComponent<PlayerStateController>().init();
        }
        for (int i = sliders.Count-1; i >= 0;i--)
        {
            Destroy(sliders[i]);
        }
        for (int i = bullets.Count-1; i >= 0;i--)
        {
            Destroy(bullets[i]);
        }
        sliders.Clear();
        bullets.Clear();
    }
    public Vector3 getResPos(GameObject player)
    {
        for (int i = 0; i < players.Count;i++)
        {
            if(players[i]==player)
            {
                return respownPos[i].position;
            }
        }
        return respownPos[0].position;
    }
    public void pauseGame()
    {
        if(ifPaused)
        {
            Time.timeScale = 0;
        }
        else if(!ifPaused)
        {
            Time.timeScale = 1;
        }
    }
}
