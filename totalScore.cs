using UnityEngine;
using System.Collections;

public class totalScore : MonoBehaviour {
	public static int gtScore = 0;
	public GUIStyle style;

	public Font myFont;
	public float ypos = 0;
	public float xpos = 0;

	void OnGUI(){				

	 string pScore = gtScore.ToString();

		GUIStyle myStyle = new GUIStyle();

		myStyle.font = myFont;

		myStyle.fontSize = (int)(40.0f * (float)(Screen.width)/1920.0f); //scale size font

		myStyle.normal.textColor = Color.white;
		

		myStyle.alignment = TextAnchor.UpperLeft;

		GUI.Label (new Rect (10, 100, 500, 20),"SCORE  "+pScore,style);
}