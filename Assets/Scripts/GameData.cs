﻿using UnityEngine;
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

    //The Variables that actually hold the information
    public int scoreData;
    public int timerData;
    public int healthData;
    public int spiritData;
    public int livesData;

    // Use this for initialization
    void Start() {

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
            createHUD("Ninja " + i, 0, 0, healthEmpty, 355 + i * 9, -37, 8, 12);
        }
        for (int i = 0; i < healthData; i++) {
            createHUD("Health " + i, 0, 0, healthFull, 355 + i * 9, -37, 8, 12);
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
