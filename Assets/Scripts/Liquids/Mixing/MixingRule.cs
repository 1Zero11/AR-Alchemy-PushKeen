using UnityEngine;

[CreateAssetMenu(fileName = "MixingRule", menuName = "Liquids/Mixing_Rule", order = 0)]
public class MixingRule : ScriptableObject {
    public Liquid Liquid1;
    public Liquid Liquid2;
    public Vector2 temperatureRange1;
    public Vector2 temperatureRange2;

    public Liquid result;
}