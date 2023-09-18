using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PauseGame_btn : MonoBehaviour
{
    // Start is called before the first frame update
    Button btn;
    void Start()
    {
        btn = transform.GetComponent<Button>();
        btn.onClick.AddListener(pauseGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void pauseGame()
    {
        GameState.Instance.ifPaused = !GameState.Instance.ifPaused;
    }
}
