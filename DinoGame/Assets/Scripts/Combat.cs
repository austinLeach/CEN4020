using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public enum combatStates { PlayersTurn, EnemiesTurn, Win, Loss }
public enum FightingWith { dino }; // can be added on to for more combat scenarios
public class Combat : MonoBehaviour
{
    public Slider dinoHealthSlider;
    public Slider playerHealthSlider;
    public GameObject FireBallImage;
    bool isFireBall = false;
    float fireBallTimer = 1f;

<<<<<<< HEAD
    public GameObject Trex; // will be one of these for each combatant
    public GameObject Herb;
    public GameObject Bird;
    public GameObject Mario;

    public TMP_Text dialogue;
=======
    public GameObject dino;
>>>>>>> 2059c36c5cbf46320b129e95d472c7f67bb06825

    bool stunned = false;

    public combatStates currentState;

    public FightingWith fighting;

    // Start is called before the first frame update
    void Start()
    {
<<<<<<< HEAD
        Trex.GetComponent<Renderer>().enabled = false; 
        Herb.GetComponent<Renderer>().enabled = false;
        Bird.GetComponent<Renderer>().enabled = false;
        Mario.GetComponent<Renderer>().enabled = false; 
        FireBallImage.GetComponent<Renderer>().enabled = false;
        
        Debug.Log(GlobalVariables.FightingWith);

        currentState = combatStates.PlayersTurn;    //currently only TRex is an option so it will always load the TRex fight
        if (GlobalVariables.FightingWith == "TRex") {
            setMaxBossHealth(10);
            Trex.GetComponent<Renderer>().enabled = true;   // used so the Trex sprite will render
        }
        else if (GlobalVariables.FightingWith == "Herb") {
            setMaxBossHealth(10);
            Herb.GetComponent<Renderer>().enabled = true;   
            Debug.Log("fdhas");
        }
        if (GlobalVariables.FightingWith == "Bird") {
            setMaxBossHealth(10);
            Bird.GetComponent<Renderer>().enabled = true;   
        }
        if (GlobalVariables.FightingWith == "Mario") {
            setMaxBossHealth(10);
            Mario.GetComponent<Renderer>().enabled = true;  
        }
        
=======
        currentState = combatStates.PlayersTurn;
        switch (GlobalVariables.FightingWith){
          case "TRex":
            setMaxDinoHealth(25);
            dino.GetComponent<Renderer>().enabled = true;
            break;
          case "Tricera":
            setMaxDinoHealth(20);
            dino.GetComponent<Renderer>().enabled = true;
            break;
          case "Veloc":
            setMaxDinoHealth(10);
            dino.GetComponent<Renderer>().enabled = true;
            break;
          default:
            break;
        }

>>>>>>> 2059c36c5cbf46320b129e95d472c7f67bb06825
        setMaxPlayerHealth(10);
    }

    void Update() {
        Timer(ref isFireBall, ref fireBallTimer);
        if (!isFireBall) {
            FireBallImage.GetComponent<Renderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // checks if won or loss
        if (currentState == combatStates.Win) {
            if (GlobalVariables.FightingWith == "TRex") {
                GlobalVariables.TRexDefeated = true;
            }
            else if (GlobalVariables.FightingWith == "Herb") {
                GlobalVariables.HerbDefeated = true;
            }
            if (GlobalVariables.FightingWith == "Bird") {
                GlobalVariables.BirdDefeated = true; 
            }
            if (GlobalVariables.FightingWith == "Mario") {
                GlobalVariables.MarioDefeated = true;  
            }
            SceneManager.LoadScene("SampleScene");
        }
        if (currentState == combatStates.Loss) {
            SceneManager.LoadScene("Combat");
        }

    }

    void PlayerAttacks(int damage) {
        setDinoHealth(damage);

        if (dinoHealthSlider.value <= 0) {
            currentState = combatStates.Win;
        }
        else {
            // changes to the enemies turn
            currentState = combatStates.EnemiesTurn;
            if (GlobalVariables.FightingWith == "TRex")
                StartCoroutine(TRexTurn());
            else if (GlobalVariables.FightingWith == "Herb")
                StartCoroutine(TRexTurn());
            else if (GlobalVariables.FightingWith == "Bird")
                StartCoroutine(TRexTurn());
            else if (GlobalVariables.FightingWith == "Mario")
                StartCoroutine(TRexTurn());
        }
    }

    IEnumerator dinoTurn() {
        yield return new WaitForSeconds(1f);

        if (!stunned) {
            setPlayerHealth(1); // default attack
            dialogue.text = "TRex stomped";
        }
        else {
            dialogue.text = "TRex was not able to attack";
            stunned = false;
        }


        if (playerHealthSlider.value <= 0) {
            currentState = combatStates.Loss;
        }
        else {
            // changes back to the players turn and waits for a option to be selected
            currentState = combatStates.PlayersTurn;
        }
    }

    // functions are used to manipulate health and the healthbar on screen
    public void setMaxBossHealth(int health) {
        bossHealthSlider.maxValue = health;
        bossHealthSlider.value = health;
    public void setMaxDinoHealth(int health) {
        dinoHealthSlider.maxValue = health;
        dinoHealthSlider.value = health;
    }

    public void setDinoHealth(int health) {
        dinoHealthSlider.value -= health;
    }

    public void setMaxPlayerHealth(int health) {
        playerHealthSlider.maxValue = health;
        playerHealthSlider.value = health;
    }

    public void setPlayerHealth(int health) {
        playerHealthSlider.value -= health;
    }

    public void Stun() {
        // 90% accuracy
        if (currentState == combatStates.EnemiesTurn) {
            return;
        }
        if (Random.Range(0, 100) < 90) {
            stunned = true;
            dialogue.text = "Stunned";
        }
        else {
            dialogue.text = "Stun Missed";
        }
        PlayerAttacks(0);
      float s = Random.Range(0, 100);
      switch (GlobalVariables.FightingWith){
        case "TRex":
          if(s < 50){
            stunned = true;
          }
          break;
        case "Tricera":
          if(s < 40){
            stunned = true;
          }
          break;
        case "Veloc":
          if(s < 80){
            stunned = true;
          }
          break;
        default:
          break;
      }
    }

    public void FireBall() {
        if (currentState == combatStates.EnemiesTurn) {
            return;
        }
        isFireBall=true;
        fireBallTimer = 0.5f;
        FireBallImage.GetComponent<Renderer>().enabled = true;
        dialogue.text = "Fireball used";
        PlayerAttacks(2);   // deals 2 damage to the enemy
    }
    public bool Timer(ref bool isChanging, ref float timer)
    {
        if (isChanging)
        {
          timer -= Time.deltaTime;
          if (timer < 0)
          {
            isChanging = false;
          }
        }
        return isChanging;
    }
}
