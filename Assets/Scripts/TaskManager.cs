using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour, ITaskManager
{
    public Potion[] AvailablePotions;
    public Potion currentTask;

    public float remainingTime;

    public event EventHandler<Potion> newTaskGenerated;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        remainingTime -= Time.deltaTime;
        if(remainingTime<0)
            GetNewTask();
    }

    public Potion GetNewTask(){
        currentTask = AvailablePotions[UnityEngine.Random.Range(0, AvailablePotions.Length)];
        remainingTime = 60*3f;
        newTaskGenerated?.Invoke(this, currentTask);
        return currentTask;
    }

    public Potion GetCurrentTask(){
        return currentTask;
    }

    public TaskCompletedArgs CompleteTask(){
        int gold = currentTask.price;
        GetNewTask();
        return new TaskCompletedArgs { lastPotionPrice = gold, newPotion = currentTask };
    }
}
