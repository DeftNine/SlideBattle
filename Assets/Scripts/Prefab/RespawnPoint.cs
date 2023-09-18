using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    //给复活点挂的，好在GameState里更新情况
    void Awake()
    {
        GameState.Instance.respownPos.Add(transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
