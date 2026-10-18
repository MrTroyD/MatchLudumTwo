using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class MatchTwoGame : MonoBehaviour
{
    enum GameState
    {
        Setup,
        WaitingForCardOne,
        WaitingForCardTwo,
        WrongAnimation,
        RightAnimation,
        Reset,
        Winning
    }
    [SerializeField]PlayingCardObject[] _cards;
    [SerializeField]Material[] _debugMaterials;

    [Header("UI")]
    [SerializeField]TMP_Text _status;

    PlayingCardObject _firstCard = null;
    PlayingCardObject _secondCard = null;

    bool _waiting;
    float _waitingTime; 

    GameState _currentState = GameState.Setup;

    public bool waiting => _waiting;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {

        Random.InitState(9291979);
        foreach (var card in _cards)
        {
            card.SetManager(this);
        }

        _status.text = "Setup!";
        Reset();

        _currentState = GameState.WaitingForCardOne;
        _status.text = "Choose card!";        

        print ("Ooooh how'd you get <color='red'>Here?</color>");                    


    }

    // Update is called once per frame
    void Update()
    {
        switch(_currentState)
        {
            case GameState.WrongAnimation:
            case GameState.RightAnimation:
                if (_waiting)
                {
                    _waitingTime -= Time.deltaTime;
                    if (_waitingTime < 0)
                    {

                        if (_firstCard.cardValue != _secondCard.cardValue)
                        {
                            _firstCard.Unflip();
                            _secondCard.Unflip();
                            _waitingTime = 1f;
                            _currentState = GameState.Reset; 
                            _status.text = "Nope!";                 
                        }
                        else
                        {
                            _waiting = false;
                            bool win = true;

                            foreach(PlayingCardObject card in _cards)
                            {
                                if (!card.isFlipped)
                                {
                                    win = false;
                                    break;
                                }
                            }

                            if (win)
                            {
                                _currentState = GameState.Winning;
                                _status.text = "Win!";
                            }
                            else
                            {
                                _currentState = GameState.WaitingForCardOne;    
                                _status.text = "Choose card!";                            
                            }
                        }

                        _firstCard = null;
                        _secondCard = null;
                    }
                }

                
                break;
            case GameState.Reset:
                if (_waitingTime > 0)
                {
                    _waitingTime -= Time.deltaTime;
                    if (_waitingTime < 0)
                    {
                        _waiting = false;
                        _currentState = GameState.WaitingForCardOne;
                        _status.text = "Choose card!";                            

                    }
                }
                break;

        }
    }

    public void Flip(PlayingCardObject card)
    {
        if (_firstCard == null)
        {
            _firstCard = card;
            _currentState = GameState.WaitingForCardTwo;
        }
        else
        {
            _secondCard = card;
            _waiting = true;

            if (_firstCard.cardValue == _secondCard.cardValue)
            {
                print ("Match!");
                _currentState = GameState.RightAnimation;
                _waitingTime = 2f;
                _status.text = "YES!";
            }
            else
            {
                print ("No Match!");
                _currentState = GameState.WrongAnimation;
                _status.text = "Nope!";
                _waitingTime = 2f;
            }
        }
    }

    public void Reset()
    {
        _firstCard = null;
        _secondCard = null;
        int cardValue = 0;

        List<PlayingCardObject> cardList = new List<PlayingCardObject>(_cards);
        while (cardList.Count > 1)
        {
            PlayingCardObject card1 = cardList[Random.Range(0, cardList.Count)];
            cardList.Remove(card1);

            PlayingCardObject card2 = cardList[Random.Range(0, cardList.Count)];
            cardList.Remove(card2);

            card1.SetValue(cardValue);
            card2.SetValue(cardValue);

            if (cardValue < _debugMaterials.Length)
            {
                card1.SetTexture(_debugMaterials[cardValue]);
                card2.SetTexture(_debugMaterials[cardValue]);
            }


            cardValue++;
        }
    }
}
