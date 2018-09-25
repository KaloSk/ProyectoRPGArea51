using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomMessage : MonoBehaviour {

	// Use this for initialization
	void Start () {
        int o = Random.Range(0, 2);
        transform.GetComponent<Text>().text = IntroText(o);
    }

    public void BuyItem()
    {
        int o = Random.Range(0, 2);
        transform.GetComponent<Text>().text = BuyText(o);
    }

    public void NotCashItem()
    {
        int o = Random.Range(0, 2);
        transform.GetComponent<Text>().text = NotCashText(o);
    }


    string IntroText(int value)
    {
        switch (value)
        {
            case 0:
               return "Welcome!";
            case 1:
                return "What are you buying'?";
            case 2:
                return "Got a selection of good things on sale, stranger!";
            default:
               return "????";
        }
        
    }

    string BuyText(int value)
    {
        switch (value)
        {
            case 0:
                return "Thank you!";
            case 1:
                return "It's that all, stranger?";
            case 2:
                return "Got a lot of good things on sale, stranger!";
            default:
                return "????";
        }

    }

    string NotCashText(int value)
    {
        switch (value)
        {
            case 0:
                return "Not enough Cash, stranger!";
            case 1:
                return "Stranger, STRANGER...!";
            case 2:
                return "It's that all, stranger?";
            default:
                return "????";
        }

    }

    string Leave(int value)
    {
        switch (value)
        {
            case 0:
                return "It's that all, stranger?";
            case 1:
                return "Come back anytime...";
            default:
                return "????";
        }

    }
}
