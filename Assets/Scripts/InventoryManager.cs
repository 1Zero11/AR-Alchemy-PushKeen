using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public List<Liquid> liquidsInInventory = new List<Liquid>();
    public List<Liquid> infiniteLiquids;
    public event EventHandler stateChanged;

    private void Start() {
        Invoke("Reset", 0.1f);
    }

    public void AddLiquid(Liquid liquid){
        if (!infiniteLiquids.Contains(liquid))
            liquidsInInventory.Add(liquid);
        stateChanged?.Invoke(this, null);
    }

    public int CountLiquid(Liquid liquid){
        if(infiniteLiquids.Contains(liquid))
            return 1;
        return liquidsInInventory.Where(l => l == liquid).Count();
    }

    public void RemoveLiquid(Liquid liquid){
        if(liquidsInInventory.Contains(liquid))
            liquidsInInventory.Remove(liquid);
        stateChanged?.Invoke(this, null);
    }

    private void Reset(){
        liquidsInInventory = new List<Liquid>();
        stateChanged?.Invoke(this, null);
    }
}
