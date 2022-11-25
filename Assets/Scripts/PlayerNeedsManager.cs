using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNeedsManager : MonoBehaviour
{
    /*
     * Variables necesarias:
     * 1. Necesitamos una variable par guardar el valor actual:
     * -> Una varible distinta para cada barra
     * 2. Necesito especificar el valor de inicio:
     * -> Una variable para cada barra
     * 3. Valor maximo:
     * -> Una por barra
     */
    //Creamos los objeto de tipo Need
    public Need health;
    public Need hunger;
    public Need water;
    public Need sleep;
    
    //Variables especificas
    //Cantidad de vida de voy a perder
    //Cuando tenga sed y/o hambre
    public float hungerHealthDecay;
    public float waterHealthDecay;
    
    //VAriable GRadient para cambiar el color de la barra
    public Gradient colors;

    public DamageIndicatorController damageIndicator;
    
    
    void Start()
    {
        //Establecemos lo valores iniciales de cada Need
        health.curValue = health.startValue;
        hunger.curValue = hunger.startValue;
        water.curValue = water.startValue;
        sleep.curValue = sleep.startValue;

    }

    // Update is called once per frame
    void Update()
    {
        // Hambre, sed y sueño se desgasten por segundo
        hunger.Subtract(hunger.decayRate * Time.deltaTime);
        water.Subtract(water.decayRate * Time.deltaTime);
        sleep.Add(sleep.regenRate * Time.deltaTime);
        //Hambre y sed a cero 
        if (hunger.curValue == 0.0f)
            health.Subtract(hungerHealthDecay * Time.deltaTime);
        if(water.curValue == 0.0f)
            health.Subtract(waterHealthDecay * Time.deltaTime);
        //Compruebo si estoy muerto
        if (health.curValue == 0.0f)
        {
            Die();
        }
        
        //Actualizar el tamaño de las barras
        health.uiBar.fillAmount = health.GetPercentage();
        hunger.uiBar.fillAmount = hunger.GetPercentage();
        hunger.uiBar.color = colors.Evaluate(hunger.GetPercentage());
        water.uiBar.fillAmount = water.GetPercentage();
        sleep.uiBar.fillAmount = sleep.GetPercentage();

    }
    
    //Acciones
    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Eat(float amount)
    {
        hunger.Add(amount);
    }

    public void Drink(float amount)
    {
        water.Add(amount);
    }

    public void Sleep(float amount)
    {
        sleep.Subtract(amount);
    }

    public void Die()
    {
        Debug.Log("Estoy muerto");
    }

    public void TakeDamage(float amount)
    {
        //LLamo al indicador de daño
        damageIndicator.Fade();
        health.Subtract(amount);
    }
    
    
}//Final de PlayerNeedsManager


//Objeto Need
//Esto le dice a Unity entre otras cosas
// que pinte sus variable en el editor
[System.Serializable]
public class Need
{
    //Variables comunes
    //Valor actual de la barra
    [HideInInspector]
    public float curValue;
    //Valor maximo de la barra
    public float maxValue;
    //Valor inicial de la barra
    public float startValue;
    //Cantidad de regeneracion por segundo
    public float regenRate;
    //Cantidad de desgaste por segundo
    public float decayRate;
    // Objeto imagen de la barra
    // para poder hacer que suba o baje visualmente
    public Image uiBar;
    
    //Metodos comunes o accsiones comunes
    //Sumar
    public void Add(float amount)
    {
        curValue = Mathf.Min(curValue + amount, maxValue);
    }
    //Restar
    public void Subtract(float amount)
    {
        curValue = Mathf.Max(curValue - amount, 0.0f);
    }
    //Teniendo en cuenta que nuestra barra
    // funciona con porcentajes, 
    // Tenemos que pasar el valor actual a porcetanje
    public float GetPercentage()
    {
        return curValue / maxValue;
    }
}
