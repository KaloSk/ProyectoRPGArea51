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
            transform.Find("PanelFace/Image").GetComponent<Image>().sprite = o.Face;
            transform.Find("PanelFace/TextHP").GetComponent<Text>().text = o.StatsInGame.HP.ToString("0000");
            transform.Find("PanelStats/TextName").GetComponent<Text> ().text = o.Name;
            transform.Find("PanelStats/TextAtk").GetComponent<Text>().text = o.StatsInGame.ATK.ToString("000");
            transform.Find("PanelStats/TextDef").GetComponent<Text>().text = o.StatsInGame.DEF.ToString("000");
            transform.Find("PanelStats/TextMag").GetComponent<Text>().text = o.StatsInGame.MAG.ToString("000");
            transform.Find("PanelStats/TextRes").GetComponent<Text>().text = o.StatsInGame.MDF.ToString("000");
            transform.Find("PanelStats/TextSpe").GetComponent<Text>().text = o.StatsInGame.SPE.ToString("000");
        }
    }
}
