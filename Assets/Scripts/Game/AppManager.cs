
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AppManager : MonoBehaviour
{

    // ============================================================= VARIABLES

    [Header("MAIN MENU")]
    [SerializeField] GameObject splashScreen;
    [SerializeField] GameObject mainMenu;
    [SerializeField] StarterManager starterManager;

    [Header("GLOBAL")]
    [SerializeField] Image globalMask;
    [SerializeField] GameSet gameSet;
    [SerializeField] SceneReference mainMenuScene;

    SceneReference currentScene;

    // ============================================================= LOCAL

    void Awake()
    {
        GameSet.SetGameSet(gameSet);
        DontDestroyOnLoad(this);
        LoadMainMenu(true);
    }

    void InitialiseMainMenu (bool showSplashScreen)
    {
        mainMenu.SetActive(false);
        splashScreen.SetActive(false);
        FadeOut(SettingsSet.current.fadingDuration);

        if (showSplashScreen) {
            splashScreen.SetActive(true);
            Functions.CallAfter(SettingsSet.current.splashDuration - SettingsSet.current.fadingDuration / 2, () => FadeInOut(SettingsSet.current.fadingDuration));
            Functions.CallAfter(SettingsSet.current.splashDuration, () => CloseSplah());
        }
        else {
            CloseSplah();
        }

        void CloseSplah ()
        {
            splashScreen.SetActive(false);
            mainMenu.SetActive(true);
            starterManager.Initialise();
        }
    }

    // ============================================================= STATIC

    static AppManager _current;
    public static AppManager current {
        get {
            if (_current == null) _current = FindObjectOfType<AppManager>();
            return _current;
        }
    }

    public static void LoadMainMenu(bool showSplashScreen, bool transition = true)
    {
        if (SceneManager.GetActiveScene().name == "MainMenu") {
            current.currentScene = current.mainMenuScene;
        }
        else {
            LoadScene(current.mainMenuScene, transition);
        }

        current.InitialiseMainMenu(showSplashScreen);
    }

    static void LoadScene(SceneReference scene, bool transition)
    {
        if (transition) {
            current.globalMask.PopUpAsTransition(Color.black, SettingsSet.current.transitionTime / 2, SettingsSet.current.transitionCurve, current.currentScene.ScenePath != string.Empty, true, () => {
                if (current.currentScene.ScenePath != string.Empty) SceneManager.UnloadSceneAsync(current.currentScene);
                SceneManager.LoadScene(scene, LoadSceneMode.Additive);
                current.currentScene = scene;
            });
        }
        else {
            if (current.currentScene.ScenePath != string.Empty) SceneManager.UnloadSceneAsync(current.currentScene);
            SceneManager.LoadScene(scene, LoadSceneMode.Additive);
            current.currentScene = scene;
        }
    }

    public static void FadeOut (float duration)
    {
        current.StartCoroutine (current.globalMask.FadeImage(Color.black, Color.black.WithA(0), duration, SettingsSet.current.fadingCurve));
    }

    public static void FadeIn(float duration)
    {
        current.StartCoroutine(current.globalMask.FadeImage(Color.black.WithA (0), Color.black, duration, SettingsSet.current.fadingCurve));
    }

    public static void FadeInOut (float duration)
    {
        FadeIn(duration / 2);
        Functions.CallAfter(duration / 2, () => FadeOut(duration / 2));
    }
}