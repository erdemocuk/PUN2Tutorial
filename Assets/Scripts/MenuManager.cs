using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    [SerializeField] private Menu[] menus;

    private void Awake()
    {
        instance = this;
    }
    public void OpenMenu(string menuName)
    {
        foreach(Menu menu in menus)
        {
            if(menu.menuName == menuName)
            {
                menu.Open();
            }
            else if(menu.open)
            {
                menu.Close();
            }
        }
    }

    public void OpenMenu(Menu menu)
    {
        foreach (Menu m in menus)
        {
            if (m.open)
            {
                CloseMenu(m);
            }
        }
        menu.Open();
    }

    public void CloseMenu(Menu menu)
    {
        menu.Close();
    }
}
