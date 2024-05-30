using TMPro;
using UnityEngine;

namespace UI.MainMenu
{
    public class MainMenuButton : ButtonBase
    {
        [SerializeField] private TextMeshProUGUI _buttonText;
        
        public int ButtonID { get; private set; }
        
        public void Initialize(MainMenuButtonData data)
        {
            _buttonText.text = data.Text;
            ButtonID = data.ID;
        }
    }
}
