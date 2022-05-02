using UnityEngine;

public class LiquidMixer : MonoBehaviour
{
    public MixingRule[] mixingRules;
    public Potion spoiledPotion;

    public Liquid TryMix(Liquid l1, Liquid l2, float temp1, float temp2)
    {
        foreach (var rule in mixingRules)
        {
            if (CheckRule(rule, l1, l2, temp1, temp2) || CheckRule(rule, l2, l1, temp2, temp1))
                return rule.result;
        }
        return spoiledPotion;
    }

    private bool CheckRule(MixingRule rule, Liquid l1, Liquid l2, float temp1, float temp2)
    {
        if (rule.Liquid1 == l1 && rule.Liquid2 == l2)
        {
            if (rule.temperatureRange1.x <= temp1 && rule.temperatureRange1.y >= temp1
                && rule.temperatureRange2.x <= temp2 && rule.temperatureRange2.y >= temp2)
            {
                return true;
            }
        }
        return false;
    }
    
}