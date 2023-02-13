using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : BaseMenu
{
    public GameObject ReferenceMenu;

    public ButtonUi NewGame, Continue, Options, Quit;

    enum MainMenuItems
	{
		NewGame,
		Continue,
		Options,
		Quit
	}

	private void Start()
	{
        Init();

    }

    public void Init()
    {
        NewGame.GetText.text = LangManager.Instance.Text("MainMenuItems", "NewGame");
        NewGame.GetControl.onClick.AddListener(delegate
        {
            LoadNewGame(Main.Instance.Scenes.Game.SceneName);
        });

        Continue.GetText.text = LangManager.Instance.Text("MainMenuItems", "Continue");
        Continue.Interactable(false);

        Options.GetText.text = LangManager.Instance.Text("MainMenuItems", "Options");
        Options.GetControl.onClick.AddListener(delegate
        {
            ShowOptions();
        });

        Quit.GetText.text = LangManager.Instance.Text("MainMenuItems", "Quit");
        Quit.GetControl.onClick.AddListener(delegate
        {
            Interface.QuitGame();
        });
    }

	public override void Hide()
	{
		if (!IsShow) return;       
        ReferenceMenu.SetActive(false);
        IsShow = false;
	}

	public override void Show()
	{
		if (IsShow) return;       
        ReferenceMenu.SetActive(true);
        IsShow = true;
	}

	private void ShowOptions()
	{
		Hide();
		Interface.Execute(InterfaceObject.OptionsMenu);
	}

	private void LoadNewGame(string lvl)
	{
		Hide();
		SceneManager.sceneLoaded += delegate { Main.Instance.InitGame(); }; // подписываемся на событие загрузки сцены
		Interface.LoadSceneAsync(lvl);
	}
}
