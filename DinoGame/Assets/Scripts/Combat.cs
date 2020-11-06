using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum combatStates { PlayersTurn, EnemiesTurn, Win, Loss }
public enum FightingWith { TRex }; // can be added on to for more combat scenarios
public class Combat : MonoBehaviour
{

    public Slider bossHealthSlider;
    public Slider playerHealthSlider;

    public GameObject Trex;

    bool stunned = false;

    public combatStates currentState;

    public FightingWith fighting;




    // Start is called before the first frame update
    void Start()
    {
        currentState = combatStates.PlayersTurn;
        if (GlobalVariables.FightingWith == "TRex") {
            setMaxBossHealth(10);
            Trex.GetComponent<Renderer>().enabled = true;   // used so the Trex sprite will render
        }
        
        setMaxPlayerHealth(10);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentState == combatStates.Win) {
            GlobalVariables.TRexDefeated = true;
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
            currentState = combatStates.EnemiesTurn;
            if (fighting == FightingWith.TRex)
                StartCoroutine(TRexTurn());
        }
    }

    IEnumerator TRexTurn() {
        yield return new WaitForSeconds(1f);
       
        if (!stunned) {
            setPlayerHealth(1); // default attack
        }


        if (playerHealthSlider.value <= 0) {
            currentState = combatStates.Loss;
        }
        else {
            currentState = combatStates.PlayersTurn;
        }
    }

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
        if (Random.Range(0, 100) < 90) 
            stunned = true;
    }

    public void FireBall() {
        if (currentState == combatStates.EnemiesTurn) {
            return;
        }
        PlayerAttacks(2);
    }
}
