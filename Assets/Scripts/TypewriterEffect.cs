using UnityEngine;
using TMPro;
using System.Collections;

public class TypewriterEffect : MonoBehaviour
{
    private TMP_Text _textBox;

    //prototyping
    /*[Header("Test String")]
    [SerializeField] private string testText;*/

    //basic typewriter functionality
    private int _currentVisibleCharacterIndex;
    private Coroutine _typewriterCoroutine;
    private bool _readyForNewText = true;

    private WaitForSeconds _simpleDelay;
    private WaitForSeconds _interpunctuationDelay;

    [Header("Typewriter Settings")]
    [SerializeField] private float charactersPerSecond = 20;
    [SerializeField] private float interpunctuationDelay = 0.5f;

    //skipping functionality
   /* public bool CurrentlySkipping {  get; private set; }
    private WaitForSeconds _skipDelay;

    [Header("Skip options")]
    [SerializeField] private bool quickSkip;
    [SerializeField][Min(1)] private int skipSpeedup = 5;*/

    private void Awake()
    {
        _textBox = GetComponent<TMP_Text>();

        _simpleDelay = new WaitForSeconds(1/charactersPerSecond);
        _interpunctuationDelay = new WaitForSeconds(1/interpunctuationDelay);

        //_skipDelay = new WaitForSeconds(1 / (charactersPerSecond * skipSpeedup));
    }

    private void OnEnable()
    {
        TMPro_EventManager.TEXT_CHANGED_EVENT.Add(PrepareForNewText);
    }

    private void OnDisable()
    {
        TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(PrepareForNewText);
    }



    private void Update()
    {
        /*if (Input.GetMouseButtonDown(1))
        {
            if (_textBox.maxVisibleCharacters != _textBox.textInfo.characterCount - 1)
                Skip();
        }*/
    }

    public void PrepareForNewText(Object obj)
    {
        if (!_readyForNewText)
            return;

        _readyForNewText = false;

        
        _textBox.maxVisibleCharacters = 0;
        _currentVisibleCharacterIndex = 0;

        _typewriterCoroutine = StartCoroutine(routine: Typewriter());
    }

    private IEnumerator Typewriter()
    {
        TMP_TextInfo textInfo = _textBox.textInfo;

        while (_currentVisibleCharacterIndex < textInfo.characterCount + 1)
        {
            /*if(_currentVisibleCharacterIndex == lastCharacterIndex)
            {
                _readyForNewText = true;
            }*/

            char character = textInfo.characterInfo[_currentVisibleCharacterIndex].character;
            _textBox.maxVisibleCharacters++;

            if (
                (character == '?' || character == '.' || character == ',' || character == ':' ||
                 character == ';' || character == '!' || character == '-'))
            {
                yield return _interpunctuationDelay;
            }
            else
            {
                yield return _simpleDelay;
            }

            _currentVisibleCharacterIndex++;
        }
    }

    /*void Skip()
    {
        if (CurrentlySkipping)
            return;

        CurrentlySkipping = true;

        if (!quickSkip)
        {
            StartCoroutine(routine: SkipSpeedupReset());
            return;
        }

        StopCoroutine(_typewriterCoroutine);
        _textBox.maxVisibleCharacters = _textBox.textInfo.characterCount;
    }

    private IEnumerator SkipSpeedupReset()
    {
        yield return new WaitUntil(() => _textBox.maxVisibleCharacters == _textBox.textInfo.characterCount - 1);
        CurrentlySkipping = false;
    }*/
}
