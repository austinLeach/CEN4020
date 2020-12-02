using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public enum combatStates { PlayersTurn, EnemiesTurn, Win, Loss }
public enum FightingWith { TRex }; // can be added on to for more combat scenarios
public class Combat : MonoBehaviour
{

    public Slider bossHealthSlider;
    public Slider playerHealthSlider;
    public GameObject FireBallImage;
    bool isFireBall = false;
    float fireBallTimer = 1f;

    public GameObject Trex; // will be one of these for each combatant
    public GameObject Herb;
    public GameObject Bird;

    public TMP_Text dialogue;

    bool stunned = false;
    bool charging = false;

    public combatStates currentState;

    public FightingWith fighting;




    // Start is called before the first frame update
    void Start()
    {
        Trex.GetComponent<Renderer>().enabled = false; 
        Herb.GetComponent<Renderer>().enabled = false;
        Bird.GetComponent<Renderer>().enabled = false;
        FireBallImage.GetComponent<Renderer>().enabled = false;

        currentState = combatStates.PlayersTurn;    //currently only TRex is an option so it will always load the TRex fight
        if (GlobalVariables.FightingWith == "TRex") {
            setMaxBossHealth(12);
            Trex.GetComponent<Renderer>().enabled = true;   // used so the Trex sprite will render
        }
        else if (GlobalVariables.FightingWith == "Herb") {
            setMaxBossHealth(10);
            Herb.GetComponent<Renderer>().enabled = true;   
        }
        if (GlobalVariables.FightingWith == "Bird") {
            setMaxBossHealth(15);
            Bird.GetComponent<Renderer>().enabled = true;   
        }
        
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
            SceneManager.LoadScene("SampleScene");
        }
        if (currentState == combatStates.Loss) {
            SceneManager.LoadScene("Combat");
        }
        
    }

    void PlayerAttacks(int damage) {
        setBossHealth(damage);

        if (bossHealthSlider.value <= 0) {
            currentState = combatStates.Win;
        }
        else {
            // changes to the enemies turn
            currentState = combatStates.EnemiesTurn;
            if (GlobalVariables.FightingWith == "TRex")
                StartCoroutine(TRexTurn());
            else if (GlobalVariables.FightingWith == "Herb")
                StartCoroutine(HerbTurn());
            else if (GlobalVariables.FightingWith == "Bird")
                StartCoroutine(BirdTurn());
        }
    }

    IEnumerator HerbTurn() {
        yield return new WaitForSeconds(1f);
       
        if (!stunned) {
            setPlayerHealth(1); // default attack
            dialogue.text = "Triceratops stomped";
        }
        else {
            dialogue.text = "Triceratops was not able to attack";
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
    IEnumerator BirdTurn() {
        yield return new WaitForSeconds(1f);
       
        if (!stunned) {
            if (!charging) {
                charging = true;
                dialogue.text = "Pterodactyl is charging up an attack";
            }
            else {
                charging = false;
                setPlayerHealth(4); //charged attack
                dialogue.text = "Pterodactyl swooped in";
            }
        }
        else {
            dialogue.text = "Pterodactyl was not able to attack. Canceled charging attack.";
            stunned = false;
            charging = false;
        }


        if (playerHealthSlider.value <= 0) {
            currentState = combatStates.Loss;
        }
        else {
            // changes back to the players turn and waits for a option to be selected
            currentState = combatStates.PlayersTurn;
        }
    }
    IEnumerator TRexTurn() {
        yield return new WaitForSeconds(1f);
       
        if (!stunned) {
            if (charging) {
                setPlayerHealth(5);
                charging = false;
                dialogue.text = "TRex charged in.";
            }
            else if (Random.Range(0, 100) < 50 && !charging) {
                charging = true;
                dialogue.text = "TRex is charging an attack.";
                
            }
            else if (!charging) {
                setPlayerHealth(2); // default attack
                dialogue.text = "TRex stomped";
            }
            
            
        }
        else {
            dialogue.text = "TRex was not able to attack. Canceled charging attack.";
            charging = false;
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
    }

    public void setBossHealth(int health) {
        bossHealthSlider.value -= health;
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
        if (Random.Range(0, 100) < 80) {
            stunned = true;
            dialogue.text = "Stunned";
        }
        else {
            dialogue.text = "Stun Missed";
        }
        PlayerAttacks(0);
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
