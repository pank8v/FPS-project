using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
   [SerializeField] private Image fillImage;
   [SerializeField] private float smoothSpee = 5f;

   private float targetFill = 1f;
   private float currentFill = 1f;

   public void SetHealth(float health) {
      targetFill = Mathf.Clamp01(health);
   }

   private void Update() {
      currentFill = Mathf.Lerp(currentFill, targetFill, Time.deltaTime * smoothSpee);
      fillImage.fillAmount = currentFill;
   }

}
