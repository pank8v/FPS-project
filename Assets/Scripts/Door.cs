using UnityEngine;

public class Door : MonoBehaviour,IInteractable
{
    public void Interact() {
        transform.localPosition = new Vector3(transform.localPosition.x + 4f, transform.localPosition.y, transform.localPosition.z);
    }
}
