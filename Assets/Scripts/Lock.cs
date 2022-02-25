using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lock : MonoBehaviour
{
    [SerializeField] float ResetTime1 = 0f;
    [SerializeField] float ResetTime2 = 0f;
    [SerializeField] float ResetTime3 = 0f;
    public LockButton[] Pins;
    public bool Unlocked = false;
    public int CurrentOrder = 1;
    bool Started = false;
    public float PlayerSkill = 1;
    [SerializeField] Text SkillLevel;

   

    // Start is called before the first frame update
    void Start()
    {
        AssignOrder();
        StartCoroutine(ResetPins());
    }

   
    IEnumerator ResetPins()
    {
        while (true)
        {
            if (Started || FindObjectOfType<GameMana>().Difficulty == 0)
            {
                ResetTime1 = 10f;
                yield return new WaitForSeconds(ResetTime1);
                ResetLock();
            }

            if (Started || FindObjectOfType<GameMana>().Difficulty == 1)
            {
                ResetTime2 = 7.5f;
                yield return new WaitForSeconds(ResetTime2);
                ResetLock();
            }

            if (Started || FindObjectOfType<GameMana>().Difficulty == 2)
            {
                ResetTime3 = 5f;
                yield return new WaitForSeconds(ResetTime3);
                ResetLock();
            }

            //if (Started)
            //{
            //    yield return new WaitForSeconds(ResetTime);
            //    ResetLock();
            //}
            yield return false;
        }
    }

    void ResetLock(int order)
    {
        if (!Unlocked)
        {
            foreach (LockButton pin in Pins)
            {
                if (pin.Order != order)
                {
                    pin.ResetPin();
                }
            }
            CurrentOrder = 1;
        }
    }

    void ResetLock()
    {
        if (!Unlocked)
        {
            foreach (LockButton pin in Pins)
            {
                pin.ResetPin();
            }
            Started = false;
            CurrentOrder = 1;
        }
    }

    void AssignOrder()
    {
        for (int i = 0; i <5; i++)
        {
            int RandOrder = Random.Range(1, 5);
            if (!hasOrder(RandOrder))
            {
                Pins[i].Order = RandOrder;
                //if (FindObjectOfType<GameMana>().Difficulty > 0)
                //{
                //    FindObjectOfType<GameMana>().Difficulty--;
                //}
            }
            else
            {
                i--;
            }
        }
    }

    bool hasOrder(int order)
    {
        foreach (LockButton pin in Pins)
        {
            if (pin.Order == order)
            {
                return true;
            }
        }
        return false;
    }

    bool DoneTry()
    {
        foreach (LockButton pin in Pins)
        {
            if (!pin.Unlocked)
            {
                return false;
            }
        }
        return true;
    }

    public void TryPin(int order)
    {
        Started = true;
        if (order == CurrentOrder)
        {
            CurrentOrder++;
        }
        else
        {
            ResetLock(order);
        }
        if (CurrentOrder == 5)
        {
            Unlocked = true;
        }
        if (DoneTry())
        {
            ResetLock();
        }
    }

    public void SkillUp()
    {
        if (PlayerSkill < 3)
        {
            PlayerSkill = PlayerSkill + 1;
            SkillLevel.text = PlayerSkill.ToString();
            print(PlayerSkill);
        }
    }

    public void SkillDown()
    {
        if (PlayerSkill > 1)
        {
            PlayerSkill = PlayerSkill - 1;
            SkillLevel.text = PlayerSkill.ToString();
            print(PlayerSkill);
        }
    }
}
