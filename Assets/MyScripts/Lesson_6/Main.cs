using GeekBrains.Controllers;
using UnityEngine;
using GeekBrains;
using Helper;
using System;

public class Main : Singleton<Main>
{
	protected Main() { }

    private GameObject _allControllers;

    public FlashLighController FlashLighController { get; private set; }
    public WeaponController WeaponController { get; private set; }
    public ObjectManager ObjectManager { get; private set; }
    public BotController BotController { get; private set; }



    [System.Serializable]
	public struct SceneDate
	{
		public SceneField MainMenu;
		public SceneField Game;
	}

	public SceneDate Scenes;

	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
        InitGame();
    }

    internal void InitGame()
    {
        _allControllers = new GameObject("AllControllers");
        _allControllers.AddComponent<InputController>();
        FlashLighController = _allControllers.AddComponent<FlashLighController>();
        WeaponController = _allControllers.AddComponent<WeaponController>();
        ObjectManager = _allControllers.AddComponent<ObjectManager>();
        BotController = _allControllers.AddComponent<BotController>();
    }
}
