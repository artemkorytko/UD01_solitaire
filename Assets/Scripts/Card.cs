using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Card : CardPlace
{
    [SerializeField] private Transform openContainer;
    [SerializeField] private Transform closeContainer;

    private MeshRenderer _meshRenderer;
    private int _value;
    private CardPlace _parent;
    private CardType _type;
    private CardColor _color;

    public CardType Type => _type;
    public CardColor Color => _color;
    public int Value => _value;

    public bool IsInDeck { get; set; } = true;
    
    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void Reset()
    {
        SetParent();
        Close();
        SetAtMain(false);
        _parent = null;
    }

    public void Initialize(int value, CardColor color, CardType type, Material material)
    {
        _value = value;
        _color = color;
        _type = type;
        _meshRenderer.material = material;
        nextCardValue = _value + 1;
        nextCardType = CardType.Any;
        nextCardColor = _color == CardColor.Black ? CardColor.Red : CardColor.Black;
    }

    public void Open()
    {
        if (isOpen) return;

        isOpen = true;
        cardContainer = openContainer;
        transform.Rotate(Vector3.forward * 180, Space.Self);
    }
    public void Close()
    {
        if (!isOpen) return;

        isOpen = false;
        cardContainer = closeContainer;
        transform.Rotate(Vector3.forward * -180, Space.Self);
    }

    public void SetParent(CardPlace parent = null)
    {
        if (parent == null)
        {
            transform.position = Vector3.zero;
        }
        else
        {
            transform.SetParent(parent.CardParent);
            transform.position = Vector3.zero;
            SetAtMain(parent.IsMain);

            if (_parent is Card card)
            {
                card.Open();
            }

            _parent = parent;
        }
    }

    private void SetAtMain(bool isMain)
    {
        if(isMain)
        {
            nextCardColor = _color;
            nextCardType = _type;
            nextCardValue = _value + 1;
        }
        else
        {
            nextCardColor = _color == CardColor.Black ? CardColor.Red : CardColor.Black;
            nextCardType = CardType.Any;
            nextCardValue = _value - 1;
        }

        this.isMain = isMain;
    }
}
