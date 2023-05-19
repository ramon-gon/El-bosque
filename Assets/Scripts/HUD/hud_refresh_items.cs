using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class hud_refresh_items : MonoBehaviour
{
[SerializeField] TMP_Text itemsHud;
[SerializeField] int items_totales, items_actuales;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        itemsHud.text = string.Format("{0}/{1}",items_actuales,items_totales);
    }

    public void sumarItem(){
        items_actuales += 1;

        if(items_actuales >= items_totales){
            //Ganar partida
        }
    }
}
