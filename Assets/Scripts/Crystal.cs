using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    public Color color;
    public GameObject cube;
    public enum typeOfCrystal
    {
        Fire,
        Cold
    };
    public float rate = 0;

    public typeOfCrystal type;

    private void Awake() {
        var cubeRenderer = cube.GetComponent<Renderer>();
        
        if(type == typeOfCrystal.Fire){
            cubeRenderer.material.SetColor("_Color", Color.red);
            rate = 1f;
        }else{
            cubeRenderer.material.SetColor("_Color", Color.blue);
            rate = -1f;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float rad = 1f;
        IEnumerable<Card> cards = Physics.OverlapSphere(transform.position, rad).Where(x => x.tag == "Card")
            .Select(x => x.GetComponent<Card>());

        foreach(Card card in cards){
            card.temperature += rate * Time.deltaTime *
             (1f/(transform.position-card.gameObject.transform.position).sqrMagnitude);
        }
    }
}
