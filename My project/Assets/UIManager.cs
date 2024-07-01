using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI decisionText;
    public Button leftButton;
    public Button rightButton;
    public Button finalButton;

    void Start()
    {
        // Inicialmente desativar o botão final
        finalButton.gameObject.SetActive(false);
    }

    // Métodos para configurar os textos dos botões
    public void SetLeftButtonText(string text)
    {
        leftButton.GetComponentInChildren<TextMeshProUGUI>().text = text;
    }

    public void SetRightButtonText(string text)
    {
        rightButton.GetComponentInChildren<TextMeshProUGUI>().text = text;
    }

    public void SetFinalButtonText(string text)
    {
        finalButton.GetComponentInChildren<TextMeshProUGUI>().text = text;
    }

    // Método para atualizar o texto da decisão
    public void SetDecisionText(string text)
    {
        decisionText.text = text;
    }

    // Métodos para ativar e desativar botões
    public void ActivateLeftButton(bool active)
    {
        leftButton.gameObject.SetActive(active);
    }

    public void ActivateRightButton(bool active)
    {
        rightButton.gameObject.SetActive(active);
    }

    public void ActivateFinalButton(bool active)
    {
        finalButton.gameObject.SetActive(active);
    }
}
