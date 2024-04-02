using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IPointerEnterHandler
{
    private Vector3 pointerPreviousPosition;
    private Vector3 pointerPosition;
    [SerializeField] private Player player;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private float strength = 1f;
    [SerializeField] private float leftEdge = -2f;
    [SerializeField] private float rightEdge = 2f;

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 direction = new Vector3(pointerPosition.x * strength, 0, 0);
        if (eventData.position.x < pointerPreviousPosition.x && player.transform.position.x > leftEdge)
        {
            player.transform.position -= direction;
            foreach(GameObject go in player.GetList()) 
            {
                go.transform.position -= direction;
            }
        }
        if(eventData.position.x > pointerPreviousPosition.x && player.transform.position.x < rightEdge) 
        {
            player.transform.position += direction;
            foreach (GameObject go in player.GetList())
            {
                go.transform.position += direction;
            }
        }
        pointerPreviousPosition = eventData.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (gameManager.GetState() == GameManager.State.Pause)
        {
            gameManager.Play();
        }
        pointerPosition = eventData.position;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }
}
