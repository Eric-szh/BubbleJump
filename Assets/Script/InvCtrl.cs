using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class InvCtrl : MonoBehaviour
{
    public void SetSprite(Sprite image) { 
        GetComponent<SpriteRenderer>().sprite = image;
    }
}
