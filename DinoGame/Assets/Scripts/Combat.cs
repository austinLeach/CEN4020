using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum combatStates { PlayersTurn, EnemiesTurn, Win, Loss }
public enum FightingWith { dino }; // can be added on to for more combat scenarios
public class Combat : MonoBehaviour
{
    public Slider dinoHealthSlider;
    public Slider playerHealthSlider;

    public GameObject dino;

    bool stunned = false;

    public combatStates currentState;

    public FightingWith fighting;

    // Start is called before the first frame update
    void Start()
    {
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

        setMaxPlayerHealth(10);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentState == combatStates.Win) {
            GlobalVariables.dinoDefeated = true;
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
            currentState = combatStates.EnemiesTurn;
            if (fighting == FightingWith.dino)
                StartCoroutine(dinoTurn());
        }
    }

    IEnumerator dinoTurn() {
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
        PlayerAttacks(2);
    }
}
