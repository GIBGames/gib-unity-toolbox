using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Converts a Unity UI component into its equivalent Text Mesh Pro UGUI object.
/// Requires the TextMeshPro addon to be installed.
/// </summary>
public class TextToTMP : Editor
{
    /// <summary>
    /// A quick and dirty container for Text Mesh Pro settings.
    /// </summary>
    public class TextMeshProSettings
    {
        public bool Enabled;
        public float FontSize;
        public float FontSizeMin;
        public float FontSizeMax;
        public float LineSpacing;
        public bool EnableRichText;
        public bool EnableAutoSizing;
        public bool WrappingEnabled;
        public TextAlignmentOptions TextAlignment;
        public TextOverflowModes TextOverflow;
        public FontStyles FontStyle;
        public Color TextColor;
        public bool IsRaycastTarget;
        public string Text;
    }

    [MenuItem("GIB/Utilities/Text To TextMeshPro", false, 3000)]

    private static void ReplaceWithTMP()
    {
        if (TMPro.TMP_Settings.defaultFontAsset == null)
        {
            EditorUtility.DisplayDialog("Error", "No Default font assigned!", "OK", "");
        }

        foreach (GameObject go in Selection.gameObjects) TMPConvert(go);
    }

    private static void TMPConvert(GameObject target)
    {
        TextMeshProSettings settings = GetTextMeshProSettings(target);

        // Yeet the text
        DestroyImmediate(target.GetComponent<Text>());

        TextMeshProUGUI tmp = target.AddComponent<TextMeshProUGUI>();
        tmp.enabled = settings.Enabled;
        tmp.fontStyle = settings.FontStyle;
        tmp.fontSize = settings.FontSize;
        tmp.fontSizeMin = settings.FontSizeMin;
        tmp.fontSizeMax = settings.FontSizeMax;
        tmp.lineSpacing = settings.LineSpacing;
        tmp.richText = settings.EnableRichText;
        tmp.enableAutoSizing = settings.EnableAutoSizing;
        tmp.alignment = settings.TextAlignment;
        tmp.enableWordWrapping = settings.WrappingEnabled;
        tmp.overflowMode = settings.TextOverflow;
        tmp.text = settings.Text;
        tmp.color = settings.TextColor;
        tmp.raycastTarget = settings.IsRaycastTarget;
    }

    private static TextMeshProSettings GetTextMeshProSettings(GameObject gameObject)
    {
        Text targetText = gameObject.GetComponent<Text>();
        if (targetText == null)
        {
            EditorUtility.DisplayDialog("ERROR!", "You must select a Unity UI Text Object to convert.", "OK", "");
            return null;
        }

        return new TextMeshProSettings
        {
            Enabled = targetText.enabled,
            FontSize = targetText.fontSize,
            FontSizeMin = targetText.resizeTextMinSize,
            FontSizeMax = targetText.resizeTextMaxSize,
            LineSpacing = targetText.lineSpacing,
            EnableRichText = targetText.supportRichText,
            EnableAutoSizing = targetText.resizeTextForBestFit,
            FontStyle = FontStyleToFontStyles(targetText.fontStyle),
            TextAlignment = TextAnchorToTextAlignmentOptions(targetText.alignment),
            WrappingEnabled = HorizontalWrapModeToBool(targetText.horizontalOverflow),
            TextOverflow = VerticalWrapModeToTextOverflowModes(targetText.verticalOverflow),
            TextColor = targetText.color,
            IsRaycastTarget = targetText.raycastTarget,
            Text = targetText.text
        };
    }

    static bool HorizontalWrapModeToBool(HorizontalWrapMode overflow) =>
        overflow == HorizontalWrapMode.Wrap;

    static TextOverflowModes VerticalWrapModeToTextOverflowModes(VerticalWrapMode verticalOverflow) =>
        verticalOverflow == VerticalWrapMode.Truncate ? TextOverflowModes.Truncate : TextOverflowModes.Overflow;

    static FontStyles FontStyleToFontStyles(FontStyle fontStyle)
    {
        // This handles most basic font styles
        switch (fontStyle)
        {
            case FontStyle.Bold:
                return FontStyles.Bold;
            case FontStyle.Normal:
                return FontStyles.Normal;
            case FontStyle.BoldAndItalic:
                return FontStyles.Bold | FontStyles.Italic;
            case FontStyle.Italic:
                return FontStyles.Italic;
        }

        Debug.LogWarning("Unrecognized font style: " + fontStyle);
        return FontStyles.Normal;
    }

    static TextAlignmentOptions TextAnchorToTextAlignmentOptions(TextAnchor textAnchor)
    {
        switch (textAnchor)
        {
            case TextAnchor.UpperLeft:
                return TextAlignmentOptions.TopLeft;
            case TextAnchor.UpperCenter:
                return TextAlignmentOptions.Top;
            case TextAnchor.UpperRight:
                return TextAlignmentOptions.TopRight;
            case TextAnchor.MiddleLeft:
                return TextAlignmentOptions.Left;
            case TextAnchor.MiddleCenter:
                return TextAlignmentOptions.Center;
            case TextAnchor.MiddleRight:
                return TextAlignmentOptions.Right;
            case TextAnchor.LowerLeft:
                return TextAlignmentOptions.BottomLeft;
            case TextAnchor.LowerCenter:
                return TextAlignmentOptions.Bottom;
            case TextAnchor.LowerRight:
                return TextAlignmentOptions.BottomRight;
            default:
                break;
        }

        Debug.LogWarning($"Unhandled text anchor: {textAnchor}! Defaulting to Top Left.");
        return TextAlignmentOptions.TopLeft;
    }
}
