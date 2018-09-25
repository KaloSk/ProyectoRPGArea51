using UnityEngine;

public class ResizeGrid : MonoBehaviour {

    public UnityEngine.UI.GridLayoutGroup GridLayoutView;
    bool ReActive;
    int FrameCount = 0;
    public float ChangeSize = 0;
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (FrameCount <= 0)
        {
            FrameCount++;
        }
        else
        {
            if (!ReActive)
            {
                RectTransform rectTransform = (RectTransform)gameObject.transform;
                if (ChangeSize == 0)
                    GridLayoutView.cellSize = new Vector2(rectTransform.rect.width - (rectTransform.rect.width * 0.03f), rectTransform.rect.width / 4.5f);
                else if (ChangeSize != -1)
                    GridLayoutView.cellSize = new Vector2(rectTransform.rect.width / (ChangeSize + 0.1f), rectTransform.rect.width / (ChangeSize + 0.6f));
                ReActive = true;
            }
        }
    }
}
