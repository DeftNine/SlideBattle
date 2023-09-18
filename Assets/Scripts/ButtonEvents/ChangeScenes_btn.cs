using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ChangeScenes_btn : MonoBehaviour
{
    // Start is called before the first frame update
    Button btn;
    public string targetName;
    void Start()
    {
        btn = transform.GetComponent<Button>();
        btn.onClick.AddListener(loadScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void loadScene()
    {
        SceneManager.LoadScene(targetName);
    }
}
