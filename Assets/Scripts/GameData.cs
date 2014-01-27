using UnityEngine;
using System.Collections;

public class GameData : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        //Score
        GameObject scoreText = new GameObject("Score");
        scoreText.AddComponent(typeof(GUIText));
        scoreText.transform.position = new Vector3(.07f, .96f, 0);
        scoreText.guiText.text = "SCORE - 000000";
        scoreText.guiText.fontSize = 11;

        //Timer
        GameObject timerText = new GameObject("Timer");
        timerText.AddComponent(typeof(GUIText));
        timerText.transform.position = new Vector3(.07f, .93f, 0);
        timerText.guiText.text = "TIMER - 000";
        timerText.guiText.fontSize = 11;

        //Lives
        GameObject livesText = new GameObject("Lives");
        livesText.AddComponent(typeof(GUIText));
        livesText.transform.position = new Vector3(.07f, .897f, 0);
        livesText.guiText.text = "P - 00";
        livesText.guiText.fontSize = 11;

        //Stage
        GameObject stageText = new GameObject("Stage Text");
        stageText.AddComponent(typeof(GUIText));
        stageText.transform.position = new Vector3(.4f, .96f, 0);
        stageText.guiText.text = "STAGE - 1 - 1";
        stageText.guiText.fontSize = 11;

        //Ryu's Health
        GameObject healthText = new GameObject("Health Text");
        healthText.AddComponent(typeof(GUIText));
        healthText.transform.position = new Vector3(.4f, .93f, 0);
        healthText.guiText.text = "NINJA - ";
        healthText.guiText.fontSize = 11;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
