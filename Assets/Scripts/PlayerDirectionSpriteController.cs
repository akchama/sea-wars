using UnityEngine;

public class PlayerDirectionSpriteController : MonoBehaviour 
{
    [SerializeField] private Sprite upRightSprite;
    [SerializeField] private Sprite upLeftSprite;
    [SerializeField] private Sprite downRightSprite;
    [SerializeField] private Sprite downLeftSprite;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void UpdateSprite(Vector3 direction)
    {
        if(direction.x > 0 && direction.y > 0) 
        {
            spriteRenderer.sprite = upRightSprite;
        }
        else if(direction.x < 0 && direction.y > 0)
        {
            spriteRenderer.sprite = upLeftSprite;
        }
        else if(direction.x > 0 && direction.y < 0) 
        {
            spriteRenderer.sprite = downRightSprite;
        } 
        else 
        {
            spriteRenderer.sprite = downLeftSprite;
        }
    }
}