using System.Collections;
using System.Linq;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using PlayerDataInput;

public class DetailManager : EditorWindow
{
    static DetailManager window;

    DetailData[] PlayerDetailData;
    PlayerDetail[] PlayerDetails;
    Dictionary<DetailData, string[]> DetailDataFields =
        new Dictionary<DetailData, string[]>();

    Vector2 scrolPos;

    Color sideBarColour = Color.blue;

    Rect sideBarRect, detailRect;

    const int windowSizeX = 800, windowSizeY = 600;

    bool detailsLoaded;

    [MenuItem("PlayerInputSettings/DetailManager")]
    static void OpenDetailManager()
    {
        window = (DetailManager)GetWindow(typeof(DetailManager));
        window.minSize = new Vector2(windowSizeX, windowSizeY);
    }

    void OnEnable()
    {
        InitDetailData();
    }

    private void OnDisable()
    {
        detailsLoaded = false;
    }

    private void InitDetailData()
    {
        FindDetails();

        SortDetailData();

        detailsLoaded = true;
    }

    private void FindDetails()
    {
        PlayerDetails =
            Resources.LoadAll<ScriptableObject>("PlayerDetails/")
            .Cast<PlayerDetail>()
            .ToArray();
    }

    private void SortDetailData()
    {
        PlayerDetailData = new DetailData[PlayerDetails.Length];

        for (int i = 0; i < PlayerDetailData.Length; i++)
        {
            var detailFeilds =
                typeof(DetailData)
                .GetFields()
                .Where(field => field.FieldType == typeof(string))
                .Select(field => field.GetValue(PlayerDetails[i].GetDetailData()))
                .Cast<string>()
                .ToArray();

            if (!DetailDataFields.ContainsKey(PlayerDetails[i].GetDetailData()))
                DetailDataFields.Add(PlayerDetails[i].GetDetailData(), detailFeilds);

            PlayerDetailData[i] = PlayerDetails[i].GetDetailData();
        }
    }

    private void OnGUI()
    {
        if (detailsLoaded)
        {
            DrawLayouts();
        }
    }

    void DrawLayouts()
    {
        CalculateRects();

        DrawSideBarRect();

        DrawDetailsRect();
    }

    private void CalculateRects()
    {
        if (window != null)
        {
            var sideBarSize =
                new Vector2(window.position.width, window.position.height / 8);
            sideBarRect = new Rect(Vector2.zero, sideBarSize);

            var detailSize =
                new Vector2(window.position.width, window.position.height - sideBarRect.height);
            detailRect = new Rect(new Vector2(0, sideBarRect.height), detailSize);
        }
    }

    void DrawSideBarRect()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 35;
        style.alignment = TextAnchor.MiddleCenter;

        GUILayout.BeginArea(sideBarRect);
        GUI.DrawTexture(sideBarRect, GetSideBarTexture());
        GUI.Box(sideBarRect, "Detail Manager", style);

        GUILayout.EndArea();
    }

    void DrawDetailsRect()
    {
        GUILayout.BeginArea(detailRect);
        GUILayout.Label("Amount of details found: " + PlayerDetailData.Count());
        scrolPos = GUILayout.BeginScrollView(scrolPos);

        if (PlayerDetailData.Count() > 0)
        {
            GUILayout.Label("-------------------------------------------------------");
            for (int i = 0; i < PlayerDetailData.Length; i++)
            {
                GUILayout.Label("Detail Found: " + PlayerDetails[i].name);

                DrawDetailData(i);

                GUILayout.Label("-------------------------------------------------------");
            }
        }

        GUILayout.EndScrollView();
        GUILayout.EndArea();
    }

    private void DrawDetailData(int detailIndex)
    {
        var DetailProperties =
            typeof(DetailData)
            .GetFields()
            .Where(field => field.FieldType == typeof(string)).ToArray();

        for (int i = 0; i < DetailProperties.Length; i++)
        {
            ShowDetailEditedLabel
                (DetailDataFields[PlayerDetailData[detailIndex]][i],
                DetailProperties[i].Name,
                detailIndex);

            DetailDataFields[PlayerDetailData[detailIndex]][i] =
                    GUILayout.TextField(DetailDataFields[PlayerDetailData[detailIndex]][i]);
        }
    }

    private void ShowDetailEditedLabel(string detail, string detailPropertyName, int detailIndex)
    {
        var isDetailEdited = detail == GetOrignalDetailValue(detailPropertyName, detailIndex);

        ReturnEditedLabel(isDetailEdited, detailPropertyName);
    }

    string GetOrignalDetailValue(string detailPropertyName, int detailIndex)
    {
        var orignalDetail = PlayerDetails[detailIndex]
            .GetType()
            .GetField(detailPropertyName)
            .GetValue(PlayerDetails[detailIndex]);

        if (orignalDetail != null)
            return orignalDetail.ToString();
        else
            return "NoValue";
    }

    void ReturnEditedLabel(bool editValidator, string lableText)
    {
        lableText = " -" + lableText;

        if (editValidator)
            GUILayout.Label(lableText);
        else
            GUILayout.Label(lableText + " (Edited)");
    }

    Texture2D GetSideBarTexture()
    {
        Texture2D sideBarTexture = new Texture2D(1, 1);
        sideBarTexture.SetPixel(0, 0, sideBarColour);
        sideBarTexture.Apply();

        return sideBarTexture;
    }
}
