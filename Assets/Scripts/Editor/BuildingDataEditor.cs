using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using static UnityEngine.GraphicsBuffer;

[CustomEditor(typeof(BuildingData), false)]
[CanEditMultipleObjects]
[System.Serializable]
public class BuildingDataEditor : Editor
{
    private BuildingData BuildingDataInstance => target as BuildingData;

    public override void OnInspectorGUI()
    {
        DrawDataInputs();
        EditorGUILayout.Space();

        serializedObject.Update();
        ClearBoardButton();
        EditorGUILayout.Space();

        DrawColumnsInputFields();
        EditorGUILayout.Space();

        if (BuildingDataInstance.board != null && BuildingDataInstance._columns > 0 && BuildingDataInstance._rows > 0)
        {
            DrawBoardTable();
        }
        serializedObject.ApplyModifiedProperties();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(BuildingDataInstance);
        }
    }

    private void DrawDataInputs()
    {
        BuildingDataInstance._name = EditorGUILayout.TextField("Name", BuildingDataInstance._name);
        BuildingDataInstance._iconBuilding = (Sprite)EditorGUILayout.ObjectField("Building Icon",BuildingDataInstance._iconBuilding, typeof(Sprite), true);
        EditorGUILayout.Space();

        BuildingDataInstance._costGold = EditorGUILayout.IntField("Gold Cost", BuildingDataInstance._costGold);
        BuildingDataInstance._costGem = EditorGUILayout.IntField("Gem Cost", BuildingDataInstance._costGem);
        EditorGUILayout.Space();

        BuildingDataInstance._earnGold = EditorGUILayout.IntField("Gold Generate", BuildingDataInstance._earnGold);
        BuildingDataInstance._earnGem = EditorGUILayout.IntField("Gem Generate", BuildingDataInstance._earnGem);
        BuildingDataInstance._InSeconds = EditorGUILayout.IntField("In Seconds", BuildingDataInstance._InSeconds);
    }
    private void ClearBoardButton()
    {
        if (GUILayout.Button("Clear Board"))
        {
            BuildingDataInstance.Clear();
        }
    }
    private void DrawColumnsInputFields()
    {
        var columnsTemp = BuildingDataInstance._columns;
        var rowsTemp = BuildingDataInstance._rows;

        BuildingDataInstance._columns = EditorGUILayout.IntField("Columns", BuildingDataInstance._columns);
        BuildingDataInstance._rows = EditorGUILayout.IntField("Rows", BuildingDataInstance._rows);

        if ((BuildingDataInstance._columns != columnsTemp || BuildingDataInstance._rows != rowsTemp) && BuildingDataInstance._columns > 0 && BuildingDataInstance._rows > 0)
        {
            BuildingDataInstance.CreateNewBoard();
        }
    }
    private void DrawBoardTable()
    {
        var tableStyle = new GUIStyle("box");
        tableStyle.padding = new RectOffset(10, 10, 10, 10);
        tableStyle.margin.left = 32;

        var headerColumnStyle = new GUIStyle();
        headerColumnStyle.fixedWidth = 25;
        headerColumnStyle.alignment = TextAnchor.MiddleCenter;

        var rowStyle = new GUIStyle();
        rowStyle.fixedHeight = 25;
        rowStyle.alignment = TextAnchor.MiddleCenter;

        var dataFieldStyle = new GUIStyle(EditorStyles.miniButtonMid);
        dataFieldStyle.normal.background = Texture2D.grayTexture;
        dataFieldStyle.onNormal.background = Texture2D.whiteTexture;

        for (var row = 0; row < BuildingDataInstance._rows; row++)
        {
            EditorGUILayout.BeginHorizontal(headerColumnStyle);
            for (var column = 0; column < BuildingDataInstance._columns; column++)
            {
                EditorGUILayout.BeginHorizontal(rowStyle);
                var data = EditorGUILayout.Toggle(BuildingDataInstance.board[row].columns[column], dataFieldStyle);
                BuildingDataInstance.board[row].columns[column] = data;
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}
