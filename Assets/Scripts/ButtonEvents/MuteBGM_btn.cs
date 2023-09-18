using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MuteBGM_btn : MonoBehaviour
{
    // Start is called before the first frame update
    SoundController sc;
    Button btn;

    void Start()
    {
        sc = GameObject.Find("Sound").GetComponent<SoundController>();
        btn = transform.GetComponent<Button>();
        btn.onClick.AddListener(sc.muteBGM);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
