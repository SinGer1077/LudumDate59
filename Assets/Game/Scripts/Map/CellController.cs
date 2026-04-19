using DG.Tweening;
using UnityEngine;

public enum ECellSprite
{
    Default,
    Chosen,
    Path,
    Fight
}

public class CellController : MonoBehaviour
{
    public SpriteRenderer sprite;

    public Sprite[] cellSprites; // 0 default 1 green 2 yellow 3 red

    public Cell cell;

    private Tween bounceTween;
    [HideInInspector]
    public bool isChosen;

    [HideInInspector]
    public ECellSprite currentType;

    private Vector3 initScale;

    private void Start()
    {
        initScale = transform.localScale;
    }

    public void SetSprite(ECellSprite spriteToSet)
    {
        sprite.sprite = cellSprites[(int)spriteToSet];
        currentType = spriteToSet;
    }

    public void SetChoose()
    {
        if (!isChosen)
        {
            isChosen = true;
            bounceTween = transform.DOScale(1.3f, 0.9f)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        }
    }

    public void ResetChoose()
    {
        if (isChosen)
        {
            isChosen = false;
            bounceTween.Kill();
            transform.localScale = initScale;
        }
    }
}
