using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    [SerializeField] Lock LockMana;
    float MaxX = -2;
    public GameObject Win_Panel;

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
        if (!isUsing && transform.position != OriPos)
        {
            transform.position = Vector3.Lerp(transform.position, OriPos, 2 * Time.deltaTime);
        }
        else if (isUsing)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = Vector3.Lerp(transform.position, new Vector3(mousePos.x, transform.position.y, 0) + new Vector3(offSet.x, 0, 0), 10 * Time.deltaTime);
            if(LockMana.Unlocked)
            {
                MaxX = -2.0f;

                StartCoroutine(ShowWinPanel());
            }
            else
            {
                MaxX = 0.6f;
            }
            if(transform.position.x < MaxX || transform.position.x > OriPos.x)
            {
                if(transform.position.x > OriPos.x)
                {
                    transform.position = OriPos;
                }
                isUsing = false;
            }
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

    public IEnumerator ShowWinPanel()
    {
        yield return new WaitForSeconds(2f);
        Win_Panel.SetActive(true);
    }
}
