using UnityEngine;

public class PaperCtrl : MonoBehaviour
{
    int paperIndex = -1;
    public bool isProtected = false;
    
    private void Start()
    {
        if (isProtected)
        {
            paperIndex = GameStateManager.Instance.RegisterPaperWall(gameObject);
        }
    }

    public void TryDestoryPaper()
    {
        if (isProtected)
        {
            Debug.Log("Paper is protected");
            GameStateManager.Instance.DestoryPaperWall(paperIndex);
            gameObject.SetActive(false);
        }
        
        Destroy(gameObject);
    }
}
