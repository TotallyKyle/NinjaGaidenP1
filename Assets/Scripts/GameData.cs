using UnityEngine;
using System.Collections;

public class GameData : MonoBehaviour {

    //Stupid Textures
    public Texture2D score;
    public Texture2D timer;
    public Texture2D lives;
    public Texture2D stage;
    public Texture2D ninja;
    public Texture2D enemy;
    public Texture2D healthFull;
    public Texture2D healthEmpty;
    public Texture2D spiritPower;
    public Texture2D powerUp;
    public Texture2D zero;
    public Texture2D one;
    public Texture2D two;
    public Texture2D three;
    public Texture2D four;
    public Texture2D five;
    public Texture2D six;
    public Texture2D seven;
    public Texture2D eight;
    public Texture2D nine;
    public Texture2D dash;
    public Texture2D shuriken;
    public Texture2D fireball;
    public Texture2D windmill;
    public Texture2D jumpSlash;
    public static Texture2D currentItem;

    //The Variables that actually hold the information
    public static int scoreData = 0;
    public static int timerData = 150;
    public static int healthData = 16;
    public static int enemyHealthData = 0;
    public static int spiritData = 0;
    public static int livesData = 2;

    void Start() {
    }

    public void Reset() {
        timerData = 150;
        healthData = 16;
        spiritData /= 2;
        livesData--;
        Application.LoadLevel("Prod Scene");
    }

    // Update is called once per frame
    void Update() {
        Transform gameHUD = GameObject.Find("Game HUD").transform;
        foreach (Transform child in gameHUD)
            Destroy(child.gameObject);

        //Array of Textures for Hashing
        Texture2D[] numberArray = { zero, one, two, three, four, five, six, seven, eight, nine };

        //Score
        createHUD("Score", .15f, 0, score, 100, -25, 5, 10);

        //Score Digits
        for (int i = 1; i < 7; i++) {
            createHUD("Score " + i + " Digit", 0, 0, numberArray[(int)Mathf.Floor(scoreData / Mathf.Pow(10, 6 - i)) % 10], 154 + (i - 1) * 11, -25, 10, 10);
        }

        //Timer
        createHUD("Timer", .15f, 0, timer, 100, -37, 5, 10);

        //Timer Digits
        for (int i = 1; i < 4; i++) {
            createHUD("Timer " + i + " Digit", 0, 0, numberArray[(int)Mathf.Floor(timerData / Mathf.Pow(10, 3 - i)) % 10], 154 + (i - 1) * 11, -37, 10, 10);
        }

        //Lives
        createHUD("Lives", .02f, 0, lives, 67, -51, 10, 10);

        //Lives Digits
        for (int i = 1; i < 3; i++) {
            createHUD("Lives " + i + " Digit", 0, 0, numberArray[(int)Mathf.Floor(livesData / Mathf.Pow(10, 2 - i)) % 10], 90 + (i - 1) * 11, -51, 10, 10);
        }

        //Spiritual Power
        createHUD("Spirit Power", .025f, 0, spiritPower, 129, -52, 10, 10);

        //Spirit Power Digits
        for (int i = 1; i < 4; i++) {
            createHUD("Spirit Power " + i + " Digit", 0, 0, numberArray[(int)Mathf.Floor(spiritData / Mathf.Pow(10, 3 - i)) % 10], 154 + (i - 1) * 11, -51, 10, 10);
        }

        //Stage
        createHUD("Stage", .15f, 0, stage, 300, -25, 5, 10);

        //Power Up
        createHUD("Power Up", .1f, 0.05f, powerUp, 218, -41, 10, 3);

        //Ryu's Health
        createHUD("Ninja", .15f, 0, ninja, 300, -37, 5, 10);

        //Ryu's Health Bars
        for (int i = healthData; i < 16; i++) {
            createHUD("Ninja Empty " + i, 0, 0, healthEmpty, 355 + i * 9, -37, 8, 12);
        }
        for (int i = 0; i < healthData; i++) {
            createHUD("Ninja Health " + i, 0, 0, healthFull, 355 + i * 9, -37, 8, 12);
        }

        //Boss Health
        createHUD("Enemy", .15f, 0, enemy, 300, -52, 8, 12);

        //Boss Health Bars
        for (int i = enemyHealthData; i < 16; i++) {
            createHUD("Enemy Empty " + i, 0, 0, healthEmpty, 355 + i * 9, -52, 8, 12);
        }
        for (int i = 0; i < Mathf.Min(enemyHealthData, 16); i++) {
            createHUD("Enemy Health " + i, 0, 0, healthFull, 355 + i * 9, -52, 8, 12);
        }

        //Current Item
        if (currentItem) {
            createHUD("Current Item", 0.03f, 0.03f, currentItem, 218, -43, 10, 10);
        }
    }

    private void createHUD(string gameObjectName, float localScaleX, float localScaleY, Texture2D textureUsed, float pixelInsetX, float pixelInsetY, float pixelInsetW, float pixelInsetH) {
        GameObject createdObject = new GameObject(gameObjectName);
        createdObject.AddComponent(typeof(GUITexture));
        createdObject.transform.position = new Vector3(0, 1, 0);
        createdObject.transform.localScale = new Vector3(localScaleX, localScaleY, 1);
        createdObject.guiTexture.texture = textureUsed;
        createdObject.guiTexture.pixelInset = new Rect(pixelInsetX, pixelInsetY, pixelInsetW, pixelInsetH);
        createdObject.transform.parent = GameObject.Find("Game HUD").transform;
    }
}
