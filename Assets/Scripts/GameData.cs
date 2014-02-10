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

    //The Variables that actually hold the information
    public static int scoreData = 0;
    public static int timerData = 150;
    public static int healthData = 16;
    public static int enemyHealthData = 32;
    public static int spiritData = 0;
    public static int livesData = 2;
    public static int currentItem = NO_ITEM;

    public const int NO_ITEM = -1;
    public const int ITEM_SHURIKEN = 0;
    public const int ITEM_FIREBLAST = 1;
    public const int ITEM_WINDMILL_SHURIKEN = 2;
    public const int ITEM_JUMP_SLASH = 3;

    // Sound to play for counting score
    public AudioClip scoreClip;

    void Start() {
    }

    public static void Reset() {
        timerData = 150;
        healthData = 16;
        spiritData /= 2;
        livesData--;
        currentItem = NO_ITEM;
        Application.LoadLevel(Application.loadedLevelName);
    }

    public void Winner() {
        GameObject.Find("Main Camera").GetComponent<TimerScript>().Stop();
        StartCoroutine("CountScore");
    }

    IEnumerator CountScore() {
        while (timerData-- > 0) {
            scoreData += 100;
            AudioSource.PlayClipAtPoint(scoreClip, transform.position);
            yield return new WaitForSeconds(0.1f);
        }
        timerData = 150;
        healthData = 16;
        spiritData = 0;
        livesData = 2;
        currentItem = NO_ITEM;
        scoreData = 0;
        enemyHealthData = 32;
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
        createHUD("Score", .1f, 0.025f, score, .239f, .96f);

        //Score Digits
        for (int i = 1; i < 7; i++) {
            createHUD("Score " + i + " Digit", 0.018f, 0.025f, numberArray[(int)Mathf.Floor(scoreData / Mathf.Pow(10, 6 - i)) % 10], .305f + (i - 1) * .021f, .96f);
        }

        //Timer
        createHUD("Timer", .1f, 0.025f, timer, .239f, .93f);

        //Timer Digits
        for (int i = 1; i < 4; i++) {
            createHUD("Timer " + i + " Digit", .018f, .025f, numberArray[(int)Mathf.Floor(timerData / Mathf.Pow(10, 3 - i)) % 10], .304f + (i - 1) * .021f, .93f);
        }

        //Lives
        createHUD("Lives", .045f, 0.025f, lives, .215f, .9f);

        //Lives Digits
        for (int i = 1; i < 3; i++) {
            createHUD("Lives " + i + " Digit", .018f, 0.025f, numberArray[(int)Mathf.Floor(livesData / Mathf.Pow(10, 2 - i)) % 10], .259f + (i - 1) * .021f, .9f);
        }

        //Spiritual Power
        createHUD("Spirit Power", .045f, 0.025f, spiritPower, .325f, .9f);

        //Spirit Power Digits
        for (int i = 1; i < 4; i++) {
            createHUD("Spirit Power " + i + " Digit", 0.018f, 0.025f, numberArray[(int)Mathf.Floor(spiritData / Mathf.Pow(10, 3 - i)) % 10], .367f + (i - 1) * .021f, .9f);
        }

        //Stage
        createHUD("Stage", .1f, 0.025f, stage, .6f, .96f);
        createHUD("Stage Digit 1", .018f, .025f, one, .663f, .96f);
        createHUD("Stage Digit Dash", .01f, .005f, dash, .685f, .96f);
        createHUD("Stage Digit 2", .018f, .025f, two, .706f, .96f);

        //Power Up
        createHUD("Power Up", .1f, 0.09f, powerUp, .48f, .93f);

        //Ryu's Health
        createHUD("Ninja", .1f, 0.025f, ninja, .6f, .93f);

        //Ryu's Health Bars
        for (int i = healthData; i < 16; i++) {
            createHUD("Ninja Empty " + i, 0.01f, 0.025f, healthEmpty, .663f + i * .012f, .928f);
        }
        for (int i = 0; i < healthData; i++) {
            createHUD("Ninja Health " + i, 0.01f, 0.025f, healthFull, .663f + i * .012f, .928f);
        }

        //Boss Health
        createHUD("Enemy", .1f, .025f, enemy, .6f, .9f);

        //Boss Health Bars
        for (int i = enemyHealthData; i < 16; i++) {
            createHUD("Enemy Empty " + i, 0.01f, 0.025f, healthEmpty, .663f + i * .012f, .898f);
        }
        for (int i = 0; i < Mathf.Min(enemyHealthData, 16); i++) {
            createHUD("Enemy Health " + i, 0.01f, 0.025f, healthFull, .663f + i * .012f, .898f);
        }

        switch (currentItem) {
            case ITEM_SHURIKEN:
                createHUD("Current Item", 0.07f, 0.07f, shuriken, .48f, .93f);
                break;
            case ITEM_FIREBLAST:
                createHUD("Current Item", 0.07f, 0.07f, fireball, .48f, .93f);
                break;
            case ITEM_WINDMILL_SHURIKEN:
                createHUD("Current Item", 0.07f, 0.07f, windmill, .48f, .93f);
                break;
            case ITEM_JUMP_SLASH:
                createHUD("Current Item", 0.07f, 0.07f, jumpSlash, .48f, .93f);
                break;
        }

    }

    private void createHUD(string gameObjectName, float localScaleX, float localScaleY, Texture2D textureUsed, float positionX, float positionY) {
        GameObject createdObject = new GameObject(gameObjectName);
        createdObject.AddComponent(typeof(GUITexture));
        createdObject.transform.position = new Vector3(positionX, positionY, 0);
        createdObject.transform.localScale = new Vector3(localScaleX, localScaleY, 1);
        createdObject.guiTexture.texture = textureUsed;
        createdObject.guiTexture.pixelInset = new Rect(0, 0, 0, 0);
        createdObject.transform.parent = GameObject.Find("Game HUD").transform;
    }
}
