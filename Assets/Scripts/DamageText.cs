using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [HideInInspector] public string targetName;
    private TextMeshProUGUI _textMeshProUGUI;
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        targetName = transform.parent.name;
    }

    public void SetText(float force,string text)
    {
        _textMeshProUGUI.color = _gameManager.gradient.Evaluate(force);
        _textMeshProUGUI.text = text;
    }

    public void DeleteAllText() { _textMeshProUGUI.text = ""; }
}