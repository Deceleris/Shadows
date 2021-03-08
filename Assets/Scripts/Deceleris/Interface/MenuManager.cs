using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{


    // ============================================================= VARIABLES

    [Header("REFERENCES")]
    [SerializeField] Menu firstToOpen;

    // ********* BOOKMARKERS
    [Header("BOOKMARKERS")]
    public List<Menu> menus = new List<Menu>();
    public List<Menu> openedMenus = new List<Menu>();
    public Menu currentMenu;

    // ============================================================= LOCAL

    void Awake()
    {
        if (AppManager.current == null) SceneManager.LoadScene(0);

        foreach (Menu menu in menus) {
            if (menu == firstToOpen) OpenMenu(menu);
            else menu.InstantClose();
        }
    }

    // ============================================================= CORPS

    public void OpenMenu (Menu menu)
    {
        if (!openedMenus.Contains(menu)) {
            menu.SetOpen(currentMenu, false);
            openedMenus.Add(menu);
            currentMenu = menu;
        }
    }

    public void CloseMenu (Menu menu)
    {
        if (openedMenus.Contains(menu)) {
            menu.SetClosed();
            openedMenus.Remove(menu);   
        } 
    }

    static MenuManager _current;
    public static MenuManager current { get {
            if (_current == null) _current = FindObjectOfType<MenuManager>();
            return _current;
        }
    }

}