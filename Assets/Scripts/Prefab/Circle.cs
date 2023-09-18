using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Circle : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(destory());
        transform.DOScale(new Vector3(3, 3, 3), 1);
    }

    // Update is called once per frame
    void Update()
    {
    }
    IEnumerator destory()
    {
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer==10)//破坏子弹
        {
            Destroy(other.gameObject);
        }
    }

}
