using UnityEngine;
using System.Collections;

//This script serves as an override of UIGrid which is also capable of detecting the change on its child.
public class AnalyzeSystemGrid : UIGrid 
{
	private AnalyzeSystemGridController gridController;

	void Awake ()
	{
		gridController = transform.parent.GetComponent<AnalyzeSystemGridController>();
	}

	void ChildGet ()
	{
		ChildChange();
		gridController.RetargetGrid();
	}

	void ChildLose ()
	{
		ChildChange();
		gridController.RetargetGrid();
	}

	void ChildChange ()
	{
		gridController.FillAnalyzeSystemSlots();
		AnalyzeSystem.AnalyzeContent();
	}
}
