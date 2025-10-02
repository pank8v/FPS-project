using TMPro;
using UnityEngine;

public class HealCount : MonoBehaviour
{
   [SerializeField] private UIController uiController;
   [SerializeField] private TextMeshProUGUI healCountText;

   private void OnEnable() {
      uiController.OnHealChanged += UpdateHealCount;
   }
   
   private void OnDisable() {
      uiController.OnHealChanged += UpdateHealCount;
   }

   public void UpdateHealCount(int healCount) {
      healCountText.text = healCount.ToString();
      Debug.Log(healCount);
   }
}
