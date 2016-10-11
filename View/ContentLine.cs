using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class ContentLine : UIDragDropItem 
{
	public AnalyzeSystem.Content content;

	public UILabel titleDisplay;
	private string finalTitle;

	void Awake ()
	{
		base.Awake();
		content.content = "gawetsdf!";
		titleDisplay = GetComponent<UILabel>();
	}

	public void Initialize (AnalyzeSystem.Content con)
	{
		content.type = con.type;
		content.title = con.title;
		content.content = con.content;
		content.index = con.index;
		content.higher = con.higher;
		content.another2 = con.another2;
		content.another = con.another;
		finalTitle = "";
		switch (con.type)
		{
			case AnalyzeSystem.ContentType.Reminder:
				if (!DialogueLua.GetVariable(Consts.VariableName.reminderReadState + content.index).AsBool)
					finalTitle = "[[FFFF66]NEW[-]]";
				else
					finalTitle = "";
				break;
			case AnalyzeSystem.ContentType.Conclusion:
				if (!DialogueLua.GetVariable(Consts.VariableName.conclusionReadState + content.index).AsBool)
					finalTitle = "[[FFFF66]NEW[-]]";
				else
					finalTitle = "";
				break;
			case AnalyzeSystem.ContentType.Truth:
				if (!DialogueLua.GetVariable(Consts.VariableName.truthReadState + content.index).AsBool)
					finalTitle = "[[FFFF66]NEW[-]]";
				else
					finalTitle = "";
				break;
			default:
				break;
		}
		finalTitle += content.title;
		titleDisplay.text = finalTitle;
	}

	public void ReadContent ()
	{
		switch (content.type)
		{
			case AnalyzeSystem.ContentType.Reminder:
				DialogueLua.SetVariable(Consts.VariableName.reminderReadState + content.index, true);
				break;
			case AnalyzeSystem.ContentType.Conclusion:
				DialogueLua.SetVariable(Consts.VariableName.conclusionReadState + content.index, true);
				break;
			case AnalyzeSystem.ContentType.Truth:
				DialogueLua.SetVariable(Consts.VariableName.truthReadState + content.index, true);
				break;
			default:
				break;
		}
		finalTitle = content.title;
		titleDisplay.text = finalTitle;
	}

	void OnTooltip(bool show)
	{
		ReadContent();
		if (show)
			UITooltip.Show(content.content);
		else
			UITooltip.Hide();
	}
}
