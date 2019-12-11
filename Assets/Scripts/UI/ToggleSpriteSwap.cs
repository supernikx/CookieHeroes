using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToggleSpriteSwap : MonoBehaviour
{
    [SerializeField]
    private Image toggleImage;
    [SerializeField]
    private Sprite deselectedSprite;
    [SerializeField]
    private Sprite selectedSprite;

    public void OnTargetToggleValueChanged(bool newValue)
    {
        if (toggleImage != null)
        {
            if (newValue)
                toggleImage.sprite = selectedSprite;
            else
                toggleImage.sprite = deselectedSprite;
        }
    }
}
