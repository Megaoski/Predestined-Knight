using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldBar : MonoBehaviour
{

    public GameObject Player;
    Block blockScript;

    Image Full;
    Image Half;
    Image Empty;
    // Start is called before the first frame update
    void Start()
    {
        blockScript = Player.GetComponent<Block>();
        Full = transform.Find("FullShield").GetComponent<Image>();
        Half = transform.Find("HalfShield").GetComponent<Image>();
        Empty = transform.Find("EmptyShield").GetComponent<Image>();

        Full.gameObject.SetActive(true);
        Half.gameObject.SetActive(false);
        Empty.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!FindObjectOfType<GameManager>().gameOver)
        {
            if (blockScript.currentBlockPoints == 0)
            {
                Full.gameObject.SetActive(false);
                Half.gameObject.SetActive(false);
                Empty.gameObject.SetActive(true);
            }
            else if (blockScript.currentBlockPoints <= 50 && blockScript.currentBlockPoints != 0)
            {
                Full.gameObject.SetActive(false);
                Half.gameObject.SetActive(true);
                Empty.gameObject.SetActive(false);
            }
            else if (blockScript.currentBlockPoints == 100)
            {
                Full.gameObject.SetActive(true);
                Half.gameObject.SetActive(false);
                Empty.gameObject.SetActive(false);
            }
        }
    }
}
