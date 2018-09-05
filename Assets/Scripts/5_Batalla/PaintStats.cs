using UnityEngine;
using UnityEngine.UI;

public class PaintStats : MonoBehaviour {

    readonly int Turn = 0;
    public RTP GameRol;
	
	// Update is called once per frame
	void Update () {
        if (Turn != -1)
        {
			var o = GameRol.GetCharacterTurn(GameRol.GetCharacterTurn ());
			transform.Find ("TextName").GetComponent<Text> ().text = o.Name;
            transform.Find("TextAtk").GetComponent<Text>().text = o.Stats.ATK.ToString("000");
            transform.Find("TextDef").GetComponent<Text>().text = o.Stats.DEF.ToString("000");
            transform.Find("TextMag").GetComponent<Text>().text = o.Stats.MAG.ToString("000");
            transform.Find("TextRes").GetComponent<Text>().text = o.Stats.MDF.ToString("000");
            transform.Find("TextSpe").GetComponent<Text>().text = o.Stats.SPE.ToString("000");
            transform.Find("TextLck").GetComponent<Text>().text = o.Stats.LUK.ToString("000");
        }
    }
}
