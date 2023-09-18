using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ActiveObject_btn : MonoBehaviour
{
    // Start is called before the first frame update
    Button btn;
    bool ifActive = false;
    public List<GameObject> list = new List<GameObject>();
    void Start()
    {
        btn = transform.GetComponent<Button>();
        btn.onClick.AddListener(activeObj);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void activeObj()
    {
        for (int i = 0; i < list.Count;i++)
        {

            list[i].SetActive(!ifActive);
        }
        ifActive = !ifActive;
    }
}
