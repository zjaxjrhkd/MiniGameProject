using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, Iitem_Run
{
    public enum ItemType
    {
        Coin,   
        Fire,   
        Food    
    }
    public ItemType itemType;

    public float speed = 20f;
    private void Update()
    {
        MoveItem();

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                TakeItem();
                gameObject.SetActive(false);
                break;
            case "BackWall":
                gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }

    public void MoveItem()
    {
        this.transform.Translate(Vector3.left * speed * Time.deltaTime);

    }
    public void TakeItem()
    {
        switch (itemType)
        {
            case ItemType.Coin:
                UIManager_Run.Instance.TakeCoin();
                break;
            case ItemType.Fire:
                UIManager_Run.Instance.TakeFire();
                break;
            case ItemType.Food:
                UIManager_Run.Instance.TakeFood();
                break;
            default:
                break;
        }
    }
}
