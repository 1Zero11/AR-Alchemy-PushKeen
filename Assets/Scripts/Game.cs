using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public readonly int goalGold = 100;
    public int availableGold = 0;
    public ITaskManager taskManager;
    public UIManager uiManager;
    public InventoryManager inventoryManager;
    public MixCardObserver mixCardObserver;

    private void Awake() {
        taskManager = GetComponent<TaskManager>();
        uiManager = GetComponent<UIManager>();

        mixCardObserver.LiquidMixed += onLiquidMixed;

        uiManager.inventoryManager = inventoryManager;
        uiManager.TrySell += onSell;

        inventoryManager.stateChanged += onInventoryStateChanged;
    }

    void Start()
    {
        Potion task = taskManager.GetNewTask();
        uiManager.SetNewGoal(task);
        uiManager.SetGlobalGoal(goalGold);
    }

    void Update()
    {
        
    }

    void onLiquidMixed(object sender, Liquid[] liquids){
        inventoryManager.AddLiquid(liquids[2]);
        uiManager.UpdateUI();
    }

    void onSell(object sender, EventArgs e){
        inventoryManager.RemoveLiquid(taskManager.GetCurrentTask());
        var args = taskManager.CompleteTask();
        availableGold += args.lastPotionPrice;
        uiManager.SetNewGoal(args.newPotion);
        uiManager.SetAvailableGold(availableGold);

        if(availableGold >= goalGold)
            YouWon();

    }

    void onInventoryStateChanged(object sender, EventArgs e)
    {
        uiManager.UpdateUI();
    }

    void YouWon(){
        uiManager.ShowYouWon();
    }
}
