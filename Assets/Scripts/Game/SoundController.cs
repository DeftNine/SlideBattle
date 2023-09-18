using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    // Start is called before the first frame update
    private static SoundController instance;
    private static bool origional = true;
    Transform bgm;
    private void Awake() {
        if (origional) {
        instance = this as SoundController;
        origional = false;
        DontDestroyOnLoad(this.gameObject);
    } else {
        Destroy(this.gameObject);
    }
    }
    void Start()
    {
        bgm = transform.Find("BGM");
    }
    // Update is called once per frame
    void Update()
    {
    }
    public void muteBGM()
    {
        if(bgm.GetComponent<AudioSource>().volume>0)
            bgm.GetComponent<AudioSource>().volume = 0;
        else
            bgm.GetComponent<AudioSource>().volume = 0.2f;
    }
}
