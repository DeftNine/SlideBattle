using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PUIController : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("角色UI")]
    public GameObject rootUI;
    public GameObject fireUI;
    public GameObject slideUI;
    public GameObject deathTextUI;
    public int deathCount=0;
    void Awake()
    {
        fireUI = rootUI.transform.Find("Fire").gameObject;
        slideUI = rootUI.transform.Find("Slide").gameObject;
        deathTextUI = rootUI.transform.Find("Death").Find("Text").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        deathTextUI.GetComponent<Text>().text = deathCount.ToString();
    }
}
