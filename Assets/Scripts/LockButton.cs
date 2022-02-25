using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockButton : MonoBehaviour
{
    [SerializeField] SpriteRenderer tumbler;
    public bool Unlocked = false;
    public int Order = 0;
    [SerializeField] float OpenPosY;
    Lock LockMana;
    Vector3 OriginPos;

    void Start()
    {
        OriginPos = transform.position;
        LockMana = FindObjectOfType<Lock>();
    }

    void Update()
    {
        if (Unlocked)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, OpenPosY, transform.position.z), 10.0f * Time.deltaTime);
        }
        else
        {
            if (transform.position != OriginPos)
            {
                transform.position = Vector3.Lerp(transform.position, OriginPos, 10.0f * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Pick" && !Unlocked)
        {
            GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
            Unlocked = true;
            LockMana.TryPin(Order);
        }
    }

    public void ResetPin()
    {
        if (Unlocked)
        {
            GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
            Unlocked = false;
        }
    }

    


}
