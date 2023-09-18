using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FireController : MonoBehaviour
{
    PlayerStateController pm;
    //攻击控制器
    PUIController pUI;
    [Header("子弹限制器")]
    public bool fireable=true;//是否可以进行攻击
    float maxFire = 100f;//当前火力容量
    float nowFire = 0f;//当前火力
    float ratFire = 15f;//每次开火增加
    float fadFire = 10f;//每秒衰减，火力满了以后快速衰减，暂定为三倍时间
    [Header("轨道限制器")]
    public bool createable = true;//是否可以进行创建轨道
    float maxSlide = 50f;//最大存储轨道数
    float nowSlide = 0f;//当前存储轨道
    float costSlide = 15f;//每次创建轨道消耗
    float ratSlide = 5f;//每秒增长
    [Header("玩家对象")]
    public Transform player;
    Transform fireArrowRoot;//瞄准器根
    Transform arrow;//瞄准器的箭头
    public int playerID=1;//玩家id，目前只有1和2(用于本地游玩)，后续可能优化
    public float rotateSpeed=150f;//测试后发现150是个相对比较舒服的旋转速度
    public bool rotLeft=false;//采用bool的形式控制角度的持续旋转
    public bool rotRight = false;
    
    Vector3 targetPos;//目标点位，通过射线检测击中map层级后更新
    public bool legal;//射线检测的合法性，防止在射线没有碰撞点的情况下射击之类的
    GameObject bullet;//存储子弹预制体
    GameObject slide;//存储轨道预制体
    [Header("P1")]
    public KeyCode fire_1=KeyCode.F;
    public KeyCode build_1=KeyCode.G;
    public KeyCode rot_l_1 = KeyCode.Q;
    public KeyCode rot_r_1 = KeyCode.E;
    [Header("P2")]
    public KeyCode fire_2=KeyCode.Semicolon;
    public KeyCode build_2=KeyCode.Quote;
    public KeyCode rot_l_2 = KeyCode.U;
    public KeyCode rot_r_2 = KeyCode.O;
    void Awake()
    {
        pm = transform.GetComponent<PlayerStateController>();
        pUI = transform.GetComponent<PUIController>();
        player = this.transform;
        fireArrowRoot = player.Find("arrowRoot");
        arrow = fireArrowRoot.Find("arrow");
        bullet = (GameObject)Resources.Load("Bullet");
        slide = (GameObject)Resources.Load("Slide");
    }
    void Start()
    {
        playerID = pm.playerID;
        init();
    }
    void init()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(pm.ifDied) return;
        fireLimiter();
        getDir();
        doRotate();
        rayCheck();
        if(createable) createSlide();
        if(fireable) shootBullet();
    }
    void fireLimiter()
    {
        nowFire -= fadFire*Time.deltaTime;
        nowSlide += ratSlide * Time.deltaTime;

        if (nowFire >= 100) { fireable = false; pUI.fireUI.GetComponent<Image>().color = Color.red; fadFire *= 3; }
        if(nowSlide-costSlide<0) createable = false;

        nowFire=Mathf.Clamp(nowFire, 0, maxFire);
        nowSlide=Mathf.Clamp(nowSlide, 0, maxSlide);

        pUI.fireUI.transform.localScale = new Vector3(nowFire / maxFire * 1.0f, 1, 1);
        pUI.slideUI.transform.localScale = new Vector3(nowSlide / maxSlide * 1.0f, 1, 1);

        if (!fireable && nowFire == 0) { fireable = true; pUI.fireUI.GetComponent<Image>().color = Color.yellow; fadFire /= 3; }
        if(nowSlide-costSlide>=0) createable = true;
    }
    void rayCheck()//射线检测，玩家和arrow两点
    {
        RaycastHit2D hit = Physics2D.Raycast(player.position+Vector3.Scale((arrow.position-player.position).normalized,new Vector3(1f,1f,1)), arrow.position - player.position,Mathf.Infinity,1<<8);//map层
        if(hit.collider!=null)
        {
            targetPos = hit.point;
            Debug.DrawLine(player.position, hit.point, Color.red);
            Debug.DrawLine(targetPos, targetPos + new Vector3(0, 0.5f, 0), Color.green);
            if(Vector2.Distance(hit.point,player.position)<Vector2.Distance(arrow.position,player.position))
            legal = false;
            else legal = true;
        }
        else legal = false;
    }
    void createSlide()//创造轨道
    {
        if(!createable) return;
        if (legal==true&&((Input.GetKeyDown(build_1)&&playerID==1)||(Input.GetKeyDown(build_2)&&playerID==2)))
        {
            Debug.Log("[Action]"+player.name+":slide");
            GameObject obj = Instantiate(slide);
            obj.transform.position = new Vector3(player.position.x,player.position.y,0);
            obj.transform.localScale = new Vector3(Vector2.Distance(targetPos, player.position), 1, 1);
            float angle= Vector2.Angle(transform.right, targetPos - player.position);//距离和角度都不要用Vector3的，z轴没有规范，可能出问题
            if(targetPos.y-player.position.y<0)//因为这个api只能返回0~180所以要做调整
                angle *= -1;
            obj.transform.Rotate(0, 0, angle, Space.Self);
            nowSlide -= costSlide;
        }
    }
    void shootBullet()//发射子弹
    {
        if(!fireable) return;
        if(legal==true&&((Input.GetKeyDown(fire_1)&&playerID==1)||(Input.GetKeyDown(fire_2)&&playerID==2)))
        {
            Debug.Log("[Action]"+player.name+":shoot");
            pm.psc.playFire();
            GameObject obj= Instantiate(bullet);
            obj.transform.position = arrow.position;
            obj.GetComponent<Rigidbody2D>().AddForce((targetPos - player.position).normalized*15, ForceMode2D.Impulse);
            nowFire += ratFire;
        }
    }
    void getDir()//按键控制方向
    {
        if (playerID == 1)
        {
            if (Input.GetKeyDown(rot_l_1))
            {
                rotLeft = true;
            }
            if (Input.GetKeyUp(rot_l_1))
            {
                rotLeft = false;
            }
            if (Input.GetKeyDown(rot_r_1))
            {
                rotRight = true;
            }
            if (Input.GetKeyUp(rot_r_1))
            {
                rotRight = false;
            }
        }
        else if(playerID==2)
        {
             if (Input.GetKeyDown(rot_l_2))
            {
                rotLeft = true;
            }
            if (Input.GetKeyUp(rot_l_2))
            {
                rotLeft = false;
            }
            if (Input.GetKeyDown(rot_r_2))
            {
                rotRight = true;
            }
            if (Input.GetKeyUp(rot_r_2))
            {
                rotRight = false;
            }
        }
    }
    void doRotate()
    {
        if(rotLeft)
        fireArrowRoot.Rotate(new Vector3(0, 0, 1), Time.deltaTime * rotateSpeed, Space.World);
        if(rotRight)
        fireArrowRoot.Rotate(new Vector3(0, 0, 1), -1*Time.deltaTime *rotateSpeed, Space.World);
    }
}
