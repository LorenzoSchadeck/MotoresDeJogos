using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DecisionTree : MonoBehaviour
{
    private DecisionNode root;
    private DecisionNode currentNode;

    public TextMeshProUGUI decisionText;
    public Button leftButton;
    public Button rightButton;
    public Button finalButton;

    void Start()
    {
        root = new DecisionNode(
            "Você está de frente para uma passagem secreta que leva a uma masmorra, o que você faz?",
            "Entrar",
            "Esperar"
        );
        
        root.Left = new DecisionNode(
            "Você entrou, e foi surprendido por um esqueleto hostil, o que você faz?",
            "Atacar",
            "Bloquear"
        );
        
        root.Right = new DecisionNode(
            "Você decidiu esperar, entâo um esqueleto tenta assutá-lo, o que você faz em seguida?",
            "Desviar",
            "Esperar"
        );
        
        root.Left.Left = new DecisionNode("O esqueleto se quebra por completo, e você vai embora da masmorra",
            "",
            ""
        );

        root.Left.Right = new DecisionNode("Após ser bloqueado, o esqueleto decide deixá-lo passar e o apunhá-la pelas costas.", 
            "", 
            ""
        );

        root.Right.Left = new DecisionNode("Você desvia e o esqueleto recua.", 
            "", 
            ""
        );
        root.Right.Right = new DecisionNode("Você espera e impaciente, o esqueleto explode.", 
            "", 
            ""
        );

        currentNode = root;

        UpdateUI();

        leftButton.onClick.AddListener(() => OnChoiceMade(true));
        rightButton.onClick.AddListener(() => OnChoiceMade(false));
    }

     void UpdateUI()
    {
        decisionText.text = currentNode.DecisionText;

        if (currentNode.Left != null && currentNode.Right != null)
        {
            leftButton.GetComponentInChildren<TextMeshProUGUI>().text = currentNode.LeftChoiceText;
            rightButton.GetComponentInChildren<TextMeshProUGUI>().text = currentNode.RightChoiceText;

            leftButton.gameObject.SetActive(true);
            rightButton.gameObject.SetActive(true);
            finalButton.gameObject.SetActive(false);
        }
        else
        {
            leftButton.gameObject.SetActive(false);
            rightButton.gameObject.SetActive(false);
            finalButton.gameObject.SetActive(true);
        }
    }

    void OnChoiceMade(bool leftChoice)
    {
        currentNode = leftChoice ? currentNode.Left : currentNode.Right;

        if (currentNode.Left == null && currentNode.Right == null)
        {
            UpdateUI();
            return;
        }

        UpdateUI();
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    void NavigateTree(DecisionNode node)
    {
        // Base case: verificar se o nó atual é um nó final
        if (node.Left == null && node.Right == null)
        {
            finalButton.gameObject.SetActive(true);
            leftButton.gameObject.SetActive(false);
            rightButton.gameObject.SetActive(false);
            return;
        }

        decisionText.text = node.DecisionText;

        if (node.Left != null)
        {
            leftButton.GetComponentInChildren<TextMeshProUGUI>().text = node.LeftChoiceText;
        }
        else
        {
            leftButton.gameObject.SetActive(false);
        }

        if (node.Right != null)
        {
            rightButton.GetComponentInChildren<TextMeshProUGUI>().text = node.RightChoiceText;
        }
        else
        {
            rightButton.gameObject.SetActive(false);
        }

        leftButton.gameObject.SetActive(node.Left != null);
        rightButton.gameObject.SetActive(node.Right != null);
        finalButton.gameObject.SetActive(false);

        leftButton.onClick.RemoveAllListeners();
        rightButton.onClick.RemoveAllListeners();
        leftButton.onClick.AddListener(() => NavigateTree(node.Left));
        rightButton.onClick.AddListener(() => NavigateTree(node.Right));
    }
}

[System.Serializable]
public class DecisionNode
{
    public string DecisionText;
    public string LeftChoiceText;
    public string RightChoiceText;
    public DecisionNode Left;
    public DecisionNode Right;

    public DecisionNode(string decisionText, string leftChoiceText = null, string rightChoiceText = null)
    {
        DecisionText = decisionText;
        LeftChoiceText = leftChoiceText;
        RightChoiceText = rightChoiceText;
    }
}
