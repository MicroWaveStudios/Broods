using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditorInternal;
using System;
using Unity.VisualScripting;
using Cinemachine.Editor;

[CustomEditor(typeof(PlayerCombat))]
public class PlayerCombatEditor : Editor
{
    #region SerializedProperites
    SerializedProperty playerMove;
    SerializedProperty playerAnimator;

    SerializedProperty anim;

    SerializedProperty ordemCombo;
    SerializedProperty ordem;

    SerializedProperty rastrosAtaque;

    ReorderableList attackList;
    ReorderableList comboList;
    #endregion

    private void OnEnable()
    {
        playerMove = serializedObject.FindProperty("playerMove");
        playerAnimator = serializedObject.FindProperty("playerAnimator");

        anim = serializedObject.FindProperty("anim");

        ordemCombo = serializedObject.FindProperty("ordemCombo");
        ordem = serializedObject.FindProperty("ordem");

        rastrosAtaque = serializedObject.FindProperty("rastrosAtaque");

        DrawListOfAttacks();
        DrawListOfCombos();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();


        EditorGUILayout.PropertyField(playerMove);
        EditorGUILayout.PropertyField(playerAnimator);
        EditorGUILayout.PropertyField(anim);
        EditorGUILayout.PropertyField(ordemCombo);
        EditorGUILayout.PropertyField(ordem);
        EditorGUILayout.PropertyField(rastrosAtaque);

        EditorGUILayout.LabelField("Setup Attacks", EditorStyles.boldLabel);
        attackList.DoLayoutList();
        EditorGUILayout.LabelField("Setup Combos", EditorStyles.boldLabel);
        comboList.DoLayoutList();



        serializedObject.ApplyModifiedProperties();
    }

    void DrawListOfAttacks()
    {
        attackList = new ReorderableList(serializedObject, serializedObject.FindProperty("_AttackList"), true, true, true, true);

        attackList.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(CalculateColumnAttacks(rect, 1, 10, 0), "AttackName");
            EditorGUI.LabelField(CalculateColumnAttacks(rect, 2, 50, 0), "AttackRange");
            EditorGUI.LabelField(CalculateColumnAttacks(rect, 3, 85, 0), "Damage");
        };
        attackList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {

            var element = attackList.serializedProperty.GetArrayElementAtIndex(index);

            rect.y += 3;

            EditorGUI.PropertyField(CalculateColumnAttacks(rect, 1, 0, 3), element.FindPropertyRelative("AttackName"), GUIContent.none);
            EditorGUI.PropertyField(CalculateColumnAttacks(rect, 2, 40, 3), element.FindPropertyRelative("AttackRange"), GUIContent.none);
            EditorGUI.PropertyField(CalculateColumnAttacks(rect, 3, 80, 3), element.FindPropertyRelative("Damage"), GUIContent.none);
        };
    }

    void DrawListOfCombos()
    {
        comboList = new ReorderableList(serializedObject, serializedObject.FindProperty("_ComboList"), true, true, true, true);

        comboList.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(CalculateColumnCombos(rect, 1, 10, 0), "Quantidade de Botões");
            EditorGUI.LabelField(CalculateColumnCombos(rect, 5, 10, 0), "Ordem Combo");
        };
        comboList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {

            var element = comboList.serializedProperty.GetArrayElementAtIndex(index);

            rect.y += 6;

            EditorGUI.PropertyField(CalculateColumnCombos(rect, 1, 0, 5), element.FindPropertyRelative("QtdBotao"), GUIContent.none);

            EditorGUI.PropertyField(CalculateColumnCombos(rect, 2, 1, 1), element.FindPropertyRelative("OrdemCombo1"), GUIContent.none);
            EditorGUI.PropertyField(CalculateColumnCombos(rect, 2, 2, 1), element.FindPropertyRelative("OrdemCombo2"), GUIContent.none);
            EditorGUI.PropertyField(CalculateColumnCombos(rect, 4, 3, 1), element.FindPropertyRelative("OrdemCombo3"), GUIContent.none);
            EditorGUI.PropertyField(CalculateColumnCombos(rect, 5, 4, 1), element.FindPropertyRelative("OrdemCombo4"), GUIContent.none);
            EditorGUI.PropertyField(CalculateColumnCombos(rect, 6, 5, 1), element.FindPropertyRelative("OrdemCombo4"), GUIContent.none);
        };
    }

    Rect CalculateColumnCombos(Rect rect, int columnNumber, float xPadding, float xWidth)
    {
        float xPosition = rect.x;
        switch (columnNumber)
        {
            case 1:
                xPosition = rect.x + xPadding;
                break;
            case 2:
                xPosition = rect.x + rect.width / 10 + xPadding;
                break;
            case 3:
                xPosition = rect.x + rect.width / 8 + xPadding;
                break;
            case 4:
                xPosition = rect.x + rect.width / 6 + xPadding;
                break;
            case 5:
                xPosition = rect.x + rect.width / 4 + xPadding;
                break;
            case 6:
                xPosition = rect.x + rect.width / 2 + xPadding;
                break;

        }


        return new Rect(xPosition, rect.y, rect.width / 5 - xWidth, EditorGUIUtility.singleLineHeight);
    }

    Rect CalculateColumnAttacks(Rect rect, int columnNumber, float xPadding, float xWidth)
    {
        float xPosition = rect.x;
        switch (columnNumber)
        {
            case 1:
                xPosition = rect.x + xPadding;
                break;
            case 2:
                xPosition = rect.x + rect.width / 4 + xPadding;
                break;
            case 3:
                xPosition = rect.x + rect.width / 2 + xPadding;
                break;
        }


        return new Rect(xPosition, rect.y, rect.width / 3 - xWidth, EditorGUIUtility.singleLineHeight);
    }
}
