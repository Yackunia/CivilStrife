using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class MaintainAspectRatio : MonoBehaviour
{
    private Image imageComponent;

    void Start()
    {
        imageComponent = GetComponent<Image>();
        UpdateAspectRatio();
    }

    private void LateUpdate()
    {
        UpdateAspectRatio();
    }

    void UpdateAspectRatio()
    {
        if (imageComponent.sprite != null)
        {
            float spriteAspectRatio = imageComponent.sprite.rect.width / imageComponent.sprite.rect.height;
            imageComponent.rectTransform.sizeDelta = new Vector2(imageComponent.rectTransform.rect.height * spriteAspectRatio, imageComponent.rectTransform.rect.height);
        }
    }
}
