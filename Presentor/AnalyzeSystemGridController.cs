using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnalyzeSystemGridController : UIDragDropContainer 
{
	private List<AnalyzeSystemGrid> gridList;

	void Awake ()
	{
		gridList = new List<AnalyzeSystemGrid>(transform.GetComponentsInChildren<AnalyzeSystemGrid>());
		reparentTarget = gridList[0].transform;
	}

	public void RetargetGrid ()
	{
		foreach (AnalyzeSystemGrid grid in gridList)
		{
			if (grid.GetChildList().Count == 0)
			{
				reparentTarget = grid.transform;
				return;
			}
		}
		reparentTarget = AnalyzeSystem.GetCurrentGrid().transform;
	}

	public void FillAnalyzeSystemSlots ()
	{
		List<ContentLine> contentLines = new List<ContentLine>();
		foreach (AnalyzeSystemGrid grid in gridList)
		{
			List<Transform> gridChildren = grid.GetChildList();
			if (gridChildren.Count == 0)
				continue;
			contentLines.Add(gridChildren[0].GetComponent<ContentLine>());
		}
		AnalyzeSystem.FillSlots(contentLines);
	}
}
