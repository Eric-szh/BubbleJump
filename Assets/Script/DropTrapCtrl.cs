using UnityEngine;

public class DropTrapCtrl : MonoBehaviour
{
    public GameObject dropPrefab;
    public Transform dropPoint;

    public void Drop()
    {
        Instantiate(dropPrefab, dropPoint.position, Quaternion.identity);
    }
}
