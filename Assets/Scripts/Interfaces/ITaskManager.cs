using System;
using UnityEngine;

public interface ITaskManager
{
    public Potion GetNewTask();
    public TaskCompletedArgs CompleteTask();
    public Potion GetCurrentTask();
}

public class TaskCompletedArgs
{
    public Potion newPotion { get; set; }
    public int lastPotionPrice { get; set; }
}
