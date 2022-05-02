using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TMP_Text derdaniumCountText;
    public TMP_Text toMakeText;
    public TMP_Text madeCountText;
    public Button sellButton;
    public TMP_Text goalText;
    public TMP_Text AvailableGoldText;
    public TMP_Text youWonText;
    public TMP_Text timerText;


    public InventoryManager inventoryManager;
    public TaskManager taskManager;
    public Ingridient Derdanium;

    public event EventHandler TrySell;

    private void Awake() {
        sellButton.onClick.AddListener(Sell);

        taskManager.newTaskGenerated += onNewTaskGenerated;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimeChange());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator TimeChange(){
        while (true)
        {
            timerText.SetText(FormatTime(taskManager.remainingTime));
            yield return new WaitForSeconds(1);
        }
    }

    public string FormatTime(float time){
        int intTime = (int)time;
        int minutes = intTime / 60;
        int seconds = intTime - minutes * 60;

        return minutes+ ":" + seconds;
    }

    public void SetNewGoal(Potion potion){
        toMakeText.SetText($"To make: {potion.name}");
        UpdateUI();
    }

    public void UpdateDerdaniumCount(int count){
        derdaniumCountText.SetText(count.ToString());
    }

    public void UpdateGoalCount(int count){
        madeCountText.SetText($"Made: {count}");
        if(count>0){
            sellButton.interactable = true;
        }else
            sellButton.interactable = false;
    }

    public void SetGlobalGoal(int gold){
        goalText.SetText("Goal: " + gold);
    }

    public void SetAvailableGold(int gold){
        AvailableGoldText.SetText("Available: " + gold);
    }

    public void Sell(){
        TrySell?.Invoke(this, null);
        UpdateUI();
    }

    public void UpdateUI(){
        int derdaniumCount = inventoryManager.CountLiquid(Derdanium);
        UpdateDerdaniumCount(derdaniumCount);

        int goalCount = inventoryManager.CountLiquid(taskManager.currentTask);
        UpdateGoalCount(goalCount);
    }

    public void onNewTaskGenerated(object sender, Potion potion){
        SetNewGoal(potion);
    }

    public void ShowYouWon(){
        youWonText.gameObject.SetActive(true);
    }
}
