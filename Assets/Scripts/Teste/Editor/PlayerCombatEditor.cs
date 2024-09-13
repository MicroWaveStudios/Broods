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
    SerializedProperty playerStats;

    SerializedProperty anim;

    SerializedProperty ordemCombo;
    SerializedProperty ordem;

    SerializedProperty rastrosAtaque;
    SerializedProperty attackGameObject;

    ReorderableList attackList;
    #endregion

    private void OnEnable()
    {
        playerMove = serializedObject.FindProperty("playerMove");
        playerAnimator = serializedObject.FindProperty("playerAnimator");
        playerStats = serializedObject.FindProperty("playerStats");

        anim = serializedObject.FindProperty("anim");

        ordemCombo = serializedObject.FindProperty("ordemCombo");
        ordem = serializedObject.FindProperty("ordem");

        rastrosAtaque = serializedObject.FindProperty("rastrosAtaque");
        attackGameObject = serializedObject.FindProperty("attackGameObject");

        DrawListOfAttacks();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();


        EditorGUILayout.PropertyField(playerMove);
        EditorGUILayout.PropertyField(playerAnimator);
        EditorGUILayout.PropertyField(playerStats);
        EditorGUILayout.PropertyField(anim);
        EditorGUILayout.PropertyField(ordemCombo);
        EditorGUILayout.PropertyField(ordem);
        EditorGUILayout.PropertyField(rastrosAtaque);
        EditorGUILayout.PropertyField(attackGameObject);

        EditorGUILayout.LabelField("Setup Attacks", EditorStyles.boldLabel);
        attackList.DoLayoutList();



        serializedObject.ApplyModifiedProperties();
    }

    void DrawListOfAttacks()
    {
        attackList = new ReorderableList(serializedObject, serializedObject.FindProperty("_AttackList"), true, true, true, true);

        attackList.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(CalculateColumnAttacks(rect, 1, 10, 0), "Attack Name");
            EditorGUI.LabelField(CalculateColumnAttacks(rect, 2, 50, 0), "Attack Range");
            EditorGUI.LabelField(CalculateColumnAttacks(rect, 3, 85, 0), "Damage");
            EditorGUI.LabelField(CalculateColumnAttacks(rect, 4, 120, 0), "Move Damage");
        };
        attackList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {

            var element = attackList.serializedProperty.GetArrayElementAtIndex(index);

            rect.y += 3;

            EditorGUI.PropertyField(CalculateColumnAttacks(rect, 1, 0, 3), element.FindPropertyRelative("AttackName"), GUIContent.none);
            EditorGUI.PropertyField(CalculateColumnAttacks(rect, 2, 40, 3), element.FindPropertyRelative("AttackRange"), GUIContent.none);
            EditorGUI.PropertyField(CalculateColumnAttacks(rect, 3, 80, 3), element.FindPropertyRelative("Damage"), GUIContent.none);
            EditorGUI.PropertyField(CalculateColumnAttacks(rect, 4, 120, 3), element.FindPropertyRelative("MoveDamage"), GUIContent.none);
        };
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
                xPosition = rect.x + rect.width / 6 + xPadding;
                break;
            case 3:
                xPosition = rect.x + rect.width / 3 + xPadding;
                break;
            case 4:
                xPosition = rect.x + rect.width / 2 + xPadding;
                break;
        }


        return new Rect(xPosition, rect.y, rect.width / 4 - xWidth, EditorGUIUtility.singleLineHeight);
    }

    //void DrawListOfAttacks()
    //{
    //    attackList = new ReorderableList(serializedObject, serializedObject.FindProperty("_AttackList"), true, true, true, true);

    //    attackList.drawHeaderCallback = (Rect rect) =>
    //    {
    //        EditorGUI.LabelField(CalculateColumnAttacks(rect, 1, 10, 0), "AttackName");
    //        EditorGUI.LabelField(CalculateColumnAttacks(rect, 2, 50, 0), "AttackRange");
    //        EditorGUI.LabelField(CalculateColumnAttacks(rect, 3, 85, 0), "Damage");
    //    };
    //    attackList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {

    //        var element = attackList.serializedProperty.GetArrayElementAtIndex(index);

    //        rect.y += 3;

    //        EditorGUI.PropertyField(CalculateColumnAttacks(rect, 1, 0, 3), element.FindPropertyRelative("AttackName"), GUIContent.none);
    //        EditorGUI.PropertyField(CalculateColumnAttacks(rect, 2, 40, 3), element.FindPropertyRelative("AttackRange"), GUIContent.none);
    //        EditorGUI.PropertyField(CalculateColumnAttacks(rect, 3, 80, 3), element.FindPropertyRelative("Damage"), GUIContent.none);
    //    };
    //}


    //Rect CalculateColumnAttacks(Rect rect, int columnNumber, float xPadding, float xWidth)
    //{
    //    float xPosition = rect.x;
    //    switch (columnNumber)
    //    {
    //        case 1:
    //            xPosition = rect.x + xPadding;
    //            break;
    //        case 2:
    //            xPosition = rect.x + rect.width / 4 + xPadding;
    //            break;
    //        case 3:
    //            xPosition = rect.x + rect.width / 2 + xPadding;
    //            break;
    //    }


    //    return new Rect(xPosition, rect.y, rect.width / 3 - xWidth, EditorGUIUtility.singleLineHeight);
    //}
}
