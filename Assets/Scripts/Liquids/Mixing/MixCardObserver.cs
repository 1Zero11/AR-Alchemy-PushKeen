using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class MixCardObserver : MonoBehaviour
{
    public float mixingRange = 0.5f;
    public float mixingTime = 2f;
    public LiquidMixer _liquidMixer;

    private float mixingRangeSquared;
    private List<Card> cards;

    public event EventHandler<Liquid[]> LiquidMixed;

    public GameObject effect;


    void Awake()
    {
        mixingRangeSquared = mixingRange * mixingRange;
        cards = new List<Card>(GameObject.FindGameObjectsWithTag("Card")
            .Select(card => card.GetComponent<Card>()));
    }

    void Start() {
        StartCoroutine(CheckMix());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CheckMix(){
        while(true){
            for (int i = 0; i < cards.Count; i++){
                for (int j = i + 1; j < cards.Count; j++){
                    Card card1 = cards[i];
                    Card card2 = cards[j];
                    if(!card1.LockedForMixing && !card2.LockedForMixing
                        && card1.enabled && card2.enabled){
                        //Debug.Log("Check");
                        if((card1.transform.position - card2.transform.position).sqrMagnitude < mixingRangeSquared)
                            StartCoroutine(Mixing(card1, card2));
                        
                    }
                }
            }
            
            yield return new WaitForSeconds(0.5f);
        }
    }


    IEnumerator Mixing(Card card1, Card card2){
        Debug.Log($"Mixing {card1.liquid.name} and {card2.liquid.name}");
        card1.LockedForMixing = card2.LockedForMixing = true;

        for (int i = 0; i < 4; i++)
        {
            if (!((card1.transform.position - card2.transform.position).sqrMagnitude < mixingRangeSquared)){
                card1.LockedForMixing = card2.LockedForMixing = false;
                yield break;
            }
            yield return new WaitForSeconds(mixingTime / 4f);
        }
        Liquid result = _liquidMixer.TryMix(card1.liquid, card2.liquid,
                card1.temperature, card2.temperature);

        StartCoroutine(ReturnOriginalLiquid(card1, card1.liquid));
        StartCoroutine(ReturnOriginalLiquid(card2, card2.liquid));
        
        if(result.name == "Spoiled"){
            Debug.Log($"Spoiled {card1.liquid.name} and {card2.liquid.name}!");

            card1.UpdateLiquid(result);
            card2.UpdateLiquid(result);
            //And maybe here...
        }else{
            card1.UseLiquid();
            card2.UseLiquid();
            LiquidMixed?.Invoke(this, new Liquid[]{card1.liquid, card2.liquid, result});
            //Put code here
            Instantiate(effect, (card1.transform.position + card2.transform.position)/2f, Quaternion.identity);
            Debug.Log($"Made {result.name}");
        }
    }

    IEnumerator ReturnOriginalLiquid(Card card, Liquid liquid){
        yield return new WaitForSeconds(3);
        //card.UpdateLiquid(liquid);
        //card.LockedForMixing = false;
    }
}
