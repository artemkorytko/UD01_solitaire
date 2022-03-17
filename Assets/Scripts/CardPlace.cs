using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlace : MonoBehaviour
{
  [SerializeField] protected int nextCardValue = -1;
  [SerializeField] protected float onGameZOffset = -1;
  [SerializeField] protected CardColor nextCardColor = CardColor.Any;
  [SerializeField] protected CardType nextCardType = CardType.Any;
  [SerializeField] protected Transform cardContainer;
  [SerializeField] protected bool isMain;

  protected bool isOpen = true;


  public Transform CardParent => cardContainer;

  public bool IsMain => isMain;

  public bool IsOpen => isOpen;

  public bool IsCanConnected(PlayingCard playingCard)
  {
    if (!isOpen)
    {
      return false;
    }

    if (playingCard.Value != nextCardValue || nextCardValue != -1)
    {
      return false;
    }

    if (nextCardColor != CardColor.Any && playingCard.Color != nextCardColor)
    {
      return false;
    }

    if (nextCardType != CardType.Any && playingCard.Type != nextCardType)
    {
      return false;
    }

    Vector3 position = playingCard.CardParent.transform.position;
    position.z = IsMain ? 0f : onGameZOffset;
    playingCard.CardParent.localPosition = position;
    
    return true;
  }
}
