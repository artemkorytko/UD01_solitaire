using UnityEngine;

public class CardPlace : MonoBehaviour
{
    [SerializeField] protected int nextCardValue = -1;
    [SerializeField] protected float onGameZOffset = 1;
    [SerializeField] protected CardColor nextCardColor = CardColor.Any;
    [SerializeField] protected CardType nextCardType = CardType.Any;
    [SerializeField] protected Transform cardContainer;
    [SerializeField] protected bool isMain;

    protected bool isOpen = true;

    public bool IsMain { get => isMain; }
    public bool IsOpen { get => isOpen; }
    public Transform CardParent { get => cardContainer;}

    public bool IsCanConnect(Card card)
    {
        if (!isOpen)
            return false;
        

        if (card.Value != nextCardValue && nextCardValue != -1)
            return false;
        

        if (nextCardColor != CardColor.Any && card.Color != nextCardColor)
            return false;

        if(nextCardType != CardType.Any && card.nextCardType != nextCardType)
            return false;

        Vector3 position = card.CardParent.transform.position;
        position.z = IsMain ? 0 : onGameZOffset;
        card.CardParent.localPosition = position;

        return true;
    }
}
