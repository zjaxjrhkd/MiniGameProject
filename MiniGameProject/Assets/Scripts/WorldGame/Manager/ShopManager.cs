using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    
    public Dictionary<string, string> spriteItems = new Dictionary<string, string>()
    {
        { "Sprite", "Player_Other" }
    };
    public Dictionary<string, string> objectItems = new Dictionary<string, string>()
    {
        { "Object", "±≤¿Â«—_¿Â∫Ò" }
    };
    public Dictionary<string, string> effectItems = new Dictionary<string, string>()
    {
        { "Effect", "±≤¿Â«—_¿Â∫Ò" }
    };

    public void OpenShop()
    {
        UIManager_World.Instance.ShopUI.SetActive(true);
    }
    public void OnChangeSprite()
    {
        GameManager.Instance.player_Object.GetComponent<Player>().OnClothesChange(spriteItems);
    }

    public void OnChangeObject()
    {
        GameManager.Instance.player_Object.GetComponent<Player>().OnClothesChange(objectItems);
    }

    public void OnChangeEffect()
    {
        GameManager.Instance.player_Object.GetComponent<Player>().OnClothesChange(effectItems);
    }

}
