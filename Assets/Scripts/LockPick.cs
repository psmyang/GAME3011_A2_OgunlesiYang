using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPick : MonoBehaviour
{
    bool isUsing = false;
    Vector3 OriPos;
    Vector3 mousePos;
    Vector3 downPos;
    Vector3 offSet;
    private void Start()
    {
        OriPos = transform.position;
    }

    void Update()
    {
        if(!isUsing && transform.position != OriPos)
        {
            transform.position = Vector3.Lerp(transform.position, OriPos, 2 * Time.deltaTime);
        }
        else if(isUsing)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = Vector3.Lerp(transform.position, new Vector3(mousePos.x, mousePos.y, 0) + new Vector3(offSet.x, offSet.y, 0), 10 * Time.deltaTime);
        }
    }

    void OnMouseDown()
    {
        isUsing = true;
        downPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offSet = transform.position - downPos;
    }

    void OnMouseUp()
    {
        isUsing = false;
    }
}
