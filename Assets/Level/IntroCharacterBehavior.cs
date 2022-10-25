using UnityEngine;

public class IntroCharacterBehavior : MonoBehaviour
{
    [SerializeField] private float bobTimer = .8f;
    [SerializeField] private float bobDistance = 10f;
    private float currentBob;
    private float mult = 1.0f;
    private AnimationCurve curve;

    void Start() {
        curve = AnimationCurve.EaseInOut(0, transform.localPosition.y, bobTimer, transform.localPosition.y + bobDistance);
        currentBob = Random.Range(0, bobTimer);
        SetPos();
    }

    void FixedUpdate() {
        if (currentBob < 0.0f || currentBob >= bobTimer) mult *= -1f;
        currentBob += mult * Time.deltaTime;
        SetPos();
    }

    void SetPos() {
        transform.localPosition = new Vector3(transform.localPosition.x, curve.Evaluate(currentBob), transform.localPosition.z);
    }
}
