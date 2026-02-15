using UnityEngine;

public class Pocket : MonoBehaviour
{
    [SerializeField] private GameObject displayObj;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<Popcorn>(out var popcorn))
        {
            displayObj.SetActive(true);
        }
    }
}
