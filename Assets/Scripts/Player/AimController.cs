using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{
    //原本的攻击控制器，适用于鼠标，后来为了本地双人，就弃用了，很多数据没有更新，不要启用！
    public Transform player;
    public Transform crosshair;
    public Vector3 mousePos;
    public Vector3 targetPos;
    public bool legal;
    public GameObject bullet;
    public GameObject wall;
    void Awake()
    {
        bullet = (GameObject)Resources.Load("Bullet");
        wall = (GameObject)Resources.Load("Line");
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rayCheck();
        followMouse();
        crosshairColor();
        showCursor();
        shootBullet();
        createWall();
    }
    void rayCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(player.position+Vector3.Scale((mousePos-player.position).normalized,new Vector3(1f,1f,1)), mousePos - player.position,Mathf.Infinity,1<<8);//map层
        if(hit.collider!=null)
        {
            targetPos = hit.point;
            Debug.DrawLine(player.position, hit.point, Color.red);
            if(Vector2.Distance(hit.point,player.position)<Vector2.Distance(mousePos,player.position))
            legal = false;
            else legal = true;
        }
        else legal = false;
    }
    void followMouse()
    {
        Vector3 tmp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        tmp.z = -5;
        crosshair.position = tmp;
    }
    void createWall()//创造墙体
    {
        if (Input.GetMouseButtonDown(1) && legal == true)
        {
            Debug.Log("[Action]"+player.name+":wall");
            GameObject obj = Instantiate(wall);
            obj.transform.position = player.position;
            obj.GetComponent<LineRenderer>().SetPosition(1, targetPos - player.position);
            List<Vector2> tmp = new List<Vector2>();
            tmp.Add(obj.GetComponent<LineRenderer>().GetPosition(0));
            tmp.Add(obj.GetComponent<LineRenderer>().GetPosition(1));
            obj.GetComponent<EdgeCollider2D>().SetPoints(tmp);
        }
    }
    void shootBullet()//发射子弹
    {
        if(Input.GetMouseButtonDown(0)&&legal==true)
        {
            Debug.Log("[Action]"+player.name+":shoot");
            GameObject obj= Instantiate(bullet);
            obj.transform.position = player.position+(targetPos - player.position).normalized*2;
            obj.GetComponent<Rigidbody2D>().AddForce((targetPos - player.position).normalized*15, ForceMode2D.Impulse);
        }
    }
    void crosshairColor()
    {
        if(legal==true)
            crosshair.GetComponent<SpriteRenderer>().color = Color.green;
        else crosshair.GetComponent<SpriteRenderer>().color = Color.red;
    }
    void showCursor()
    {
        if(Input.GetMouseButtonDown(2))
            Cursor.visible = !Cursor.visible;
    }
}
