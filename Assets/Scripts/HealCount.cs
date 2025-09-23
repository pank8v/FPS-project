using TMPro;
using UnityEngine;

public class HealCount : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI healCountText;

   public void UpdateHealCount(int healCount) {
      healCountText.text = healCount.ToString();
      Debug.Log(healCount);
   }
}
