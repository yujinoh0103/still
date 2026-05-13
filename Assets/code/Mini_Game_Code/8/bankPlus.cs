using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bankPlus : MonoBehaviour
{
    public Text firstButton;
    public Text secondButton;
    public Text thirdButton;
    public Text fourthButton;

    public void firstplus()
    {
        int num = 0;

        if (firstButton.text != "9")
        {
            num = int.Parse(firstButton.text) + 1;
            firstButton.text = num.ToString();
        }
        else
            firstButton.text = "0";
    }

    public void secondplus()
    {
        int num = 0;

        if (secondButton.text != "9")
        {
            num = int.Parse(secondButton.text) + 1;
            secondButton.text = num.ToString();
        }
        else
            secondButton.text = "0";
    }

    public void thirdplus()
    {
        int num;

        if (thirdButton.text != "9")
        {
            num = int.Parse(thirdButton.text) + 1;
            thirdButton.text = num.ToString();
        }
        else
            thirdButton.text = "0";
    }

    public void fourthplus()
    {
        int num;

        if (fourthButton.text != "9")
        {
            num = int.Parse(fourthButton.text) + 1;
            fourthButton.text = num.ToString();
        }
        else
            fourthButton.text = "0";
    }
}
