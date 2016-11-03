using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using PixelCrushers.DialogueSystem.NGUI;

public class QuestWindow : QuestLogWindow 
{
	#region Public Variables

	/// <summary>
	/// The specific panel for quests. 
	/// </summary>
	public GameObject mainPanel;

	/// <summary>
	/// The Table which contains the quest. Please note that it is obligated to introduce such a component for the dialogue system. 
	/// </summary>
	public UITable questTable;

	/// <summary>
	/// The prefab for quest template. Refer to the gameObject in the hierarchy is ok. Prevents introducing new objects in prefabs ... 
	/// Any way it is still good to have a constant view component in the hierarchy. 
	/// </summary>
	public NGUIQuestTemplate questTemplate;

	#endregion

	#region MonoBehaviours

	private void Start ()
	{
		if (questTemplate != null)
			NGUITools.SetActive(questTemplate.gameObject, false);
	}

	#endregion

	#region Public Override Methods

	public override void OnQuestListUpdated ()
	{
		ClearTable();
		if (Quests.Length == 0)
		{
			AddQuestToTable(new QuestInfo(string.Empty, new FormattedText(NoQuestsMessage), FormattedText.empty, new FormattedText[0], new QuestState[0], false, false, false));
		}
		else
		{
			AddQuestsToTable();
		}
	}

	#endregion

	#region Private Methods

	private void ClearTable ()
	{
		
	}

	/// <summary>
	/// Please do make sure that THERE ARE ALWAYS EXACTLY ONE QUEST ACTIVE! 
	/// </summary>
	private void AddQuestsToTable() {
		if (questTable == null) return;
		foreach (var questInfo in Quests) 
		{
			AddQuestToTable(questInfo);
		}
		questTable.Reposition();
	}

	private void AddQuestToTable(QuestInfo questInfo) {
		if ((questTable == null) || (questTemplate == null)) return;
		GameObject child = NGUITools.AddChild(questTable.gameObject, questTemplate.gameObject);
		NGUIQuestTemplate item = child.GetComponent<NGUIQuestTemplate>();
		NGUITools.SetActive(child, true);
		item.heading.text = questInfo.Heading.text;
		string fullDescription = string.Empty;
		if (questHeadingSource == QuestHeadingSource.Name) fullDescription += questInfo.Description.text;
		for (int i = 0; i < questInfo.Entries.Length; i++) 
		{
			if (questInfo.EntryStates[i] != QuestState.Unassigned) 
			{
				fullDescription += "\nSubTask " + (i + 1)+ " " + questInfo.Entries[i].text;
			}
		}
		item.description.text = fullDescription;
	}

	#endregion
}
