using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CardDeck _cardDeck;
    
    private PlayingCard _holdCard;
    private Camera _camera;
    private LayerMask _layerMask;
    
    private Vector3 _offset;

    public event System.Action<CardType, int> OnAddToMain;
    public event System.Action<CardType, int> OnRemoveFromMain;
    private void Awake()
    {
        _camera = FindObjectOfType<Camera>();
        _cardDeck = FindObjectOfType<CardDeck>();
        _layerMask = LayerMask.GetMask("PlayingCard");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryHoldCard();
        }

        if (Input.GetMouseButton(0) && _holdCard != null)
        {
            MoveHoldCard();
        }

        if (Input.GetMouseButtonUp(0) && _holdCard != null)
        {
            ReleaseCard();
        }
    }

    private void TryHoldCard()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, int.MaxValue, _layerMask))
        {
            var card = hit.collider.gameObject.GetComponent<PlayingCard>();
           if (card != null)
           {
               _holdCard = card;
               _holdCard.transform.Translate(Vector3.back * 0.5f, Space.World);
               Vector3 worldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
               worldPosition.z = _holdCard.transform.position.z;
               _offset = worldPosition - _holdCard.transform.position;
           }
        }
    }

    private void MoveHoldCard()
    {
        Vector3 worldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = _holdCard.transform.position.z;
        _holdCard.transform.position = worldPosition - _offset;
    }

    private void ReleaseCard()
    {
        Ray ray = new Ray(_holdCard.transform.position, Vector3.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, int.MaxValue, _layerMask))
        {
            CardPlace cardPlace = hit.collider.gameObject.GetComponent<CardPlace>();
            if (cardPlace != null && cardPlace.IsCanConnected(_holdCard))
            {
                _holdCard.transform.position = cardPlace.transform.position - new Vector3(0, 0, 0.1f);
                if (cardPlace.IsMain)
                {
                    OnAddToMain?.Invoke(_holdCard.Type,_holdCard.Value);
                }

                if (_holdCard.IsMain && !cardPlace.IsMain)
                {
                    OnRemoveFromMain?.Invoke(_holdCard.Type,_holdCard.Value);
                }

                if (_holdCard.IsInDeck)
                {
                    _cardDeck.ExcludeCurrentCard();
                }
                _holdCard.SetParent(cardPlace);
            }
            else
            {
                _holdCard.SetParent();
            }
        }
        else
        {
            _holdCard.SetParent();
        }

        _holdCard = null;
    }
}
