using Toolkit.UI;
using UnityEngine;
using UnityEngine.UI;

public class ScreenRevive : UIView
{
   [SerializeField] private RectTransform _contentPanel;
   [SerializeField] private Button _reviveButton;
   [SerializeField] private Button _noThanksButton;

   private void OnEnable()
   {
      _reviveButton.onClick.AddListener(OnReviveButtonClick);
      _noThanksButton.onClick.AddListener(OnNoThanksButtonClick);
   }

   private void OnDisable()
   {
      _reviveButton.onClick.RemoveAllListeners();
      _noThanksButton.onClick.RemoveAllListeners();
   }

   public override void Init()
   {
      base.Init();
   }

   public override void Show()
   {
      base.Show();
   }

   public override void Hide()
   {
      base.Hide();
   }

   private void OnReviveButtonClick()
   {
      Hide();
      UIManager.Instance.GetView<ScreenTapToContinue>().Show();
   }

   private void OnNoThanksButtonClick()
   {
      Hide();
      UIManager.Instance.GetView<ScreenLose>().Show();
   }
}
