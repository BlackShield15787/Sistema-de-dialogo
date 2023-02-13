# SISTEMA DE DIALOGO

# Índice

este proyecto contiene

- [ ]  Índice
- [ ]  Instalación
- creación de nueva escena
- [ ]  Contenido
- [ ]  recomendaciones

# Instalación

- [ ]  tienes que importar la carpeta de assets y utilizar la carpeta preestablecida de sistema de dialogo ya que esta tiene todos las carpetas necesarias para que pueda funcionar el sistema.
- [ ]  o si prefieres puedes crear una nueva escena y crear el tuyo pero para eso tendrás que seguir una serie de pasos que explicare adelante

# Configuración de una nueva escena

- [ ]  al empezar una nueva escena vacía tendrás que crear un objeto vacío y ponerle el nombre player, a este objeto tendrás que añadirle un componente Sprite render y colocar el Sprite de character y después tendrás que añadirle el script de player ya que este posee todo el código para que el personaje se pueda mover con las flechas direccionales.

- [ ]  después tendrás que añadir el prefab de NPC y añadirle el script dialogue.

- [ ]  seguido de esto  tendrás que añadirles los componentes Box Collider 2d a los objetos de npc y al player.

- [ ]  al objeto de npc tendrás que añadirle un segundo componente Box Collider para que el script de dialogo funcione de manera correcta ya que este hará que el player no traspase al npc y que pueda saltar la advertencia de dialogo.

- [ ]  en el panel y en el TextMeshPro deberás de seleccionar que el script aparezca en la parte inferior esto se realiza desde la parte del inspector.

- [ ]  con esto el proyecto debería funcionar bien pero necesita otro detalle en el objeto main cámara tendrás que añadirle el scrpit de followPlayer y en el inspector añadirle en player donde lo indica para que pueda seguir al personaje.

- [ ]  en el dialogue panel en el apartado de dialogue text tendrás que borrar el new text para que no se muestre en pantalla  y tendrás que apagarlo desde el inspector junto al dialogue panel.

- [ ]  en inspector del NPC en el apartado del zorro tendrás que marcar el texto que se va mostrar en el dialogue lines y para activarlo al acercarte a un zorro tendrás que darle clic derecho.

- [ ]  luego en el inspector del npc en el apartado de script deberás añadir el Sprite Exclamation donde lo indica y apagarlo para que aparezca en el momento de acercarse al zorro y también tendrás que añadir los objetos de dialogue panel y el dialogue text para que funcione a la perfección.

# Contenido

Este proyecto contiene 3 scripts diferentes en referencia a 

- [ ]  seguir al personaje

```
public class FollowPlayer : MonoBehaviour
{
[SerializeField] private GameObject player;

 private void LateUpdate()
 {
     transform.position = new Vector3(player.transform.position.x, player.transform.posi tion.y, transform.position.z);
 }
}
```

- [ ]  movimiento del personaje

```
public class Player : MonoBehaviour
{
[SerializeField] private float speed;
private Rigidbody2D playerRb;
private Vector2 moveInput;
private Animator playerAnimator;
void Start()
{
    playerRb = GetComponent<Rigidbody2D>();
    playerAnimator = GetComponent<Animator>();
}
void Update()
{
    float moveX = Input.GetAxisRaw("Horizontal");
    float moveY = Input.GetAxisRaw("Vertical");
    moveInput = new Vector2(moveX, moveY).normalized;

    playerAnimator.SetFloat("Horizontal", moveX);
    playerAnimator.SetFloat("Vertical", moveY);
    playerAnimator.SetFloat("Speed", moveInput.sqrMagnitude);
}
private void FixedUpdate()
{
    playerRb.MovePosition(playerRb.position + moveInput * speed * Time.fixedDeltaTime);
}
}
```

- [ ]  y el dialogo de los personajes

```
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
```

En referente a lo visual posee:

- [ ]  el jugador
- [ ]  los zorros
- [ ]  un cartel
- [ ]  un símbolo de exclamación que aparece al momento de acercarte
- [ ]  el panel de los diálogos
- [ ]  los diálogos de personajes

# Recomendaciones

no se recomienda modificar los scrips ya que esto puede provocar varios errores como que no cierra el dialogo o te puedas mover durante el dialogo

Los puntos relevantes son que se puede modificar los diálogos desde el inspector de los NPC para que puedas realizar las interacciones que quieras