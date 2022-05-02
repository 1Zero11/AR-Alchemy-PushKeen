using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    public GameObject liquidOnScene;
    public TextMeshPro nameText;
    public TextMeshPro tempText;
    public Liquid originalLiquid;
    public Liquid liquid;
    public bool LockedForMixing = false;
    public bool used = false;

    public float _temperature = 293f;


    public float temperature{
        get { return _temperature; }
        set { _temperature = Mathf.Clamp(value, 0f, Mathf.Infinity); }
    }

    void Awake()
    {
        var cubeRenderer = liquidOnScene.GetComponent<Renderer>();
        cubeRenderer.material.SetColor("_Color", liquid.color);
        nameText.SetText(liquid.name);
    }

    // Update is called once per frame
    void Update()
    {
        tempText.SetText("T: " + Mathf.Floor(temperature).ToString()+"K");
    }

    public void UpdateLiquid(Liquid newLiquid){
        liquid = newLiquid;

        var cubeRenderer = liquidOnScene.GetComponent<Renderer>();
        cubeRenderer.material.SetColor("_Color", liquid.color);
        nameText.SetText(liquid.name);
    }

    public void UseLiquid(){
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);

        foreach (var component in rendererComponents)
            component.enabled = false;

        // Enable colliders:
        foreach (var component in colliderComponents)
            component.enabled = false;

        LockedForMixing = true;
        used = true;


    }
}
