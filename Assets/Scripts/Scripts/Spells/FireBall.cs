using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] private float velocity;
    [SerializeField] private float timeSpan;

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * velocity);
        Destroy(this.gameObject, timeSpan);
    }
}
