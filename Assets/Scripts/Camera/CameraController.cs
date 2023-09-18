using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform CamPos;
    public CinemachineVirtualCamera cinemachine;
    public Transform p1;
    public Transform p2;
    void Start()
    {
        CamPos = transform.Find("CamPos");
    }

    // Update is called once per frame
    void Update()
    {
        CamPos.position = (p1.position + p2.position) / 2;
        cinemachine.m_Lens.OrthographicSize = Vector2.Distance(p1.position, p2.position)/2 + 5;
        cinemachine.m_Lens.OrthographicSize = Mathf.Clamp(cinemachine.m_Lens.OrthographicSize, 20, Mathf.Infinity);
    }
}
