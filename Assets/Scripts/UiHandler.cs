using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class UiHandler : MonoBehaviour
{
    public static UiHandler Instance;

    [SerializeField] private PopupBase[] pages;

    private AllPages _previousPage;
    
    private AllPages _currentPage;

    private readonly List<PopupBase> _popups = new List<PopupBase>();

    public void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ShowPopup(AllPages.MenuPage, AllPages.MenuPage);
    }

    public void ShowPopup(AllPages previousPage,AllPages currentPage)
    {
        foreach (var page in pages)
        {
            if(page.CurrentPage == previousPage && page.IsActive)
            {
                _previousPage = previousPage;
                _popups.Add(page);
                page.Close();
            }
        }

        _currentPage = currentPage;

        pages[(int)currentPage].Open();
    }

    public void CheckAndClosePopup()
    {
        if (_currentPage == AllPages.MenuPage || _currentPage == AllPages.Ingame)
            return;

        pages[(int)_currentPage].Close();
        _popups[_popups.Count - 1].Open();

        _currentPage = _popups[_popups.Count - 1].CurrentPage;
        _popups.Remove(_popups[_popups.Count - 1]);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CheckAndClosePopup();
        }
    }

    public AllPages CurrentActivePage => _currentPage;
}