using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private GameObject DialogueMark;// referencia seteable desde el inspector
    [SerializeField, TextArea(4,6)] private string[] dialogueLines;//para modificar y añadir dialogos
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text DialogueText;

    private float TypingTime = 0.05f;
    private bool isPlayerInRange;
    private bool didDialogueStart;
    private int lineIndex;
   
    void Update()
    {
        if(isPlayerInRange && Input.GetButtonDown("Fire1"))//para que se active el dialogo al hacer clic derecho
        {
            if (!didDialogueStart)
            {
              StarDialogue();
            }
            else if (DialogueText.text == dialogueLines[lineIndex])
            {
                NextDialogueLine();
            }
            else 
            {
            StopAllCoroutines();
            DialogueText.text = dialogueLines[lineIndex]; //detener los dialogos
            }
        }
    }

    private void StarDialogue()//para mostrar los dialogos de manera correcta
    {
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        DialogueMark.SetActive(false);//para desaparecer la marca al iniciar conversacion
        lineIndex = 0; // desde que linea empezar el dialogo
        Time.timeScale = 0f;
        StartCoroutine(ShowLine());
    }
    private void NextDialogueLine()//para empezar otros dialogos
    {
        lineIndex++;
        if (lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
            DialogueMark.SetActive(true);
            Time.timeScale = 1f;
        }
    }

    private IEnumerator ShowLine()
    {
        DialogueText.text = string.Empty;

        foreach(char ch in dialogueLines[lineIndex])
        {
            DialogueText.text += ch;//para concatenar caracteres
            yield return new WaitForSecondsRealtime(TypingTime);//tiempo de espera para aparicion de caracteres 
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) //para verificar la colision de objetos
    {
        if (collision.gameObject.CompareTag("player"))//para que la colision se active
        {                                             //con el player
            isPlayerInRange = true;
            DialogueMark.SetActive(true);
            
        }
       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player"))//para que la colision se niegue
        {                                             //con el player
            isPlayerInRange = false;
            DialogueMark.SetActive(false);
            
        }
    }
}
