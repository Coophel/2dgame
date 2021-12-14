using System.Collections;
using UnityEngine;

public class PageController : MonoBehaviour
{
    public static PageController Instance;

	public PageType entryPage;
	public Page[] pages;

	private Hashtable m_pages;

#region Unity Functions
	// 생성 후 게임 매니져에 등록.
	private void Start()
	{
		if (!Instance)
		{
			Instance = this;
			Gamemanager.Instance.PageController = this;
			m_pages = new Hashtable();
			RegisterAllPages();

			if (entryPage != PageType.None)
				TurnPageOn(entryPage);
		}
	}
#endregion

#region Public Functions
	public void TurnPageOn(PageType on)
	{
		if (on == PageType.None)
			return;
		
		if (!PageExists(on))
			return;

		Page page = GetPage(on);
		page.gameObject.SetActive(true);
	}

	public void TurnPageOff(PageType off, PageType on = PageType.None)
	{
		if (off == PageType.None)
			return;

		if (!PageExists(off))
			return;

		Page offpage = GetPage(off);

		if (offpage.gameObject.activeSelf)
		{
			offpage.gameObject.SetActive(false);
		}

		if (on != PageType.None)
		{
			Page onpage = GetPage(on);
			TurnPageOn(on);
		}
	}
#endregion

#region Private Functions
	private void RegisterAllPages()
	{
		foreach (var page in pages)
		{
			RegisterPage(page);
		}
	}

	private void RegisterPage(Page page)
	{
		if (PageExists(page.type))
			return ;

		m_pages.Add(page.type, page);
	}

	private Page GetPage(PageType type)
	{
		if (!PageExists(type))
			return null;

		return (Page)m_pages[type];
	}

	private bool PageExists(PageType pageType)
	{
		return m_pages.ContainsKey(pageType);
	}
#endregion
}
