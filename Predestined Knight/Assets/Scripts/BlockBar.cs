using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockBar : MonoBehaviour
{
    Slider slider;
    public GameObject Player;
    Block blockScript;

    // Start is called before the first frame update
    void Start()
    {
        slider = transform.GetComponent<Slider>();
        blockScript = Player.GetComponent<Block>();
        slider.maxValue = blockScript.maxBlockPoints;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = blockScript.currentBlockPoints;
    }
}
