using UnityEngine;

public class Door : MonoBehaviour,IInteractable
{

    [SerializeField] private string interactionText;
    public string InteractionText => interactionText;

    [SerializeField] private GameObject door;
    [SerializeField] private float openHeight = 4f;
    [SerializeField] private float speed = 10f;
    private bool isOpened = false;
    private Vector3 closedPosition;
    private Vector3 openedPosition;
    private Vector3 targetPosition;


    private void Start() {
        closedPosition = door.transform.localPosition;
        openedPosition = closedPosition + new Vector3(0, openHeight, 0);
        targetPosition = closedPosition;
    }
    
    private void Update() {
        door.transform.localPosition = Vector3.Lerp(door.transform.localPosition, targetPosition, speed * Time.deltaTime);
    }
    
    public void Interact(IInteractor interactor) {
       isOpened = !isOpened;
       targetPosition = isOpened ? openedPosition : closedPosition;
    }
}
