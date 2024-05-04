using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SetGameButton))]
[CanEditMultipleObjects]
[System.Serializable]

public class SetGameButtonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SetGameButton myScript = target as SetGameButton;

        switch(myScript.ButtonType)
        {
            case SetGameButton.EButtonType.PairNumberBtn:
                myScript.PairNumber = (GameSettings.EPairNumber)EditorGUILayout.EnumPopup("Pair Numbers", myScript.PairNumber);
                break;
            case SetGameButton.EButtonType.PuzzleCategoryBtn:
                myScript.Puzzlecategories = (GameSettings.EPuzzleCategories)EditorGUILayout.EnumPopup("Puzle Categories", myScript.Puzzlecategories);
                break;
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }
}
