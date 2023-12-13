using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Obvious.Soap.Editor
{
    public class CreateTypePopUpWindow : PopupWindowContent
    {
        private readonly GUISkin _skin;
        private readonly Rect _position;
        private string _typeText = "YourType";
        private bool _variable = true;
        private bool _event = true;
        private bool _eventListener = true;
        private bool _list = true;
        private bool _invalidTypeName;
        private string _path;
        private readonly Vector2 _dimensions = new Vector2(300, 300);
        private readonly GUIStyle _bgStyle;

        public override Vector2 GetWindowSize() => _dimensions;

        public CreateTypePopUpWindow(Rect origin)
        {
            _position = origin;
            _skin = Resources.Load<GUISkin>("GUISkins/SoapWizardGUISkin");
            _bgStyle = new GUIStyle(GUIStyle.none);
            _bgStyle.normal.background = SoapInspectorUtils.CreateTexture(SoapEditorUtils.SoapColor);
        }

        public override void OnGUI(Rect rect)
        {
            editorWindow.position = SoapInspectorUtils.CenterInWindow(editorWindow.position, _position);

            DrawTitle();
            EditorGUI.BeginChangeCheck();
            _typeText = EditorGUILayout.TextField(_typeText, _skin.textField);
            if (EditorGUI.EndChangeCheck())
            {
                _invalidTypeName = !IsTypeNameValid();
            }

            var guiStyle = new GUIStyle(EditorStyles.label);
            guiStyle.normal.textColor = _invalidTypeName ? SoapEditorUtils.SoapColor : Color.white;
            guiStyle.fontStyle = FontStyle.Bold;
            var errorMessage = _invalidTypeName ? "Invalid type name." : "";
            EditorGUILayout.LabelField(errorMessage, guiStyle);

            DrawTypeToggles();

            GUILayout.Space(10);
            EditorGUILayout.LabelField("Selected path:", EditorStyles.boldLabel);
            guiStyle = new GUIStyle(EditorStyles.label);
            guiStyle.fontStyle = FontStyle.Italic;
            _path = SoapFileUtils.GetSelectedFolderPathInProjectWindow();
            EditorGUILayout.LabelField($"{_path}", guiStyle);

            DrawButtons();
        }

        private void DrawTypeToggles()
        {
            EditorGUILayout.BeginVertical();

            _variable = GUILayout.Toggle(_variable, "ScriptableVariable");
            GUILayout.Space(5);
            _event = GUILayout.Toggle(_event, "ScriptableEvent");
            GUILayout.Space(5);
            _eventListener = GUILayout.Toggle(_eventListener && _event, "EventListener");
            GUILayout.Space(5);
            _list = GUILayout.Toggle(_list, "ScriptableList");

            EditorGUILayout.EndVertical();
        }

        private void DrawTitle()
        {
            GUILayout.BeginVertical(_bgStyle);
            var titleStyle = _skin.customStyles.ToList().Find(x => x.name == "title");
            EditorGUILayout.LabelField("Create new Type", titleStyle);
            GUILayout.EndVertical();
        }

        private void DrawButtons()
        {
            if (GUILayout.Button("Create", GUILayout.ExpandHeight(true)))
            {
                if (!IsTypeNameValid())
                    return;

                TextAsset newFile = null;
                var progress = 0f;
                EditorUtility.DisplayProgressBar("Progress", "Start", progress);

                if (_variable)
                {
                    if (!CreateVariable(out newFile))
                    {
                        Close();
                        return;
                    }
                }

                progress += 0.25f;
                EditorUtility.DisplayProgressBar("Progress", "Generating...", progress);

                if (_event)
                {
                    if (!CreateFromTemplate("ScriptableEventTemplate.cs", out newFile))
                    {
                        Close();
                        return;
                    }
                }

                progress += 0.25f;
                EditorUtility.DisplayProgressBar("Progress", "Generating...", progress);

                if (_eventListener)
                {
                    if (!CreateFromTemplate("EventListenerTemplate.cs", out newFile))
                    {
                        Close();
                        return;
                    }
                }

                progress += 0.25f;
                EditorUtility.DisplayProgressBar("Progress", "Generating...", progress);

                if (_list)
                {
                    if (!CreateFromTemplate("ScriptableListTemplate.cs", out newFile))
                    {
                        Close();
                        return;
                    }
                }

                progress += 0.25f;
                EditorUtility.DisplayProgressBar("Progress", "Completed!", progress);

                EditorUtility.DisplayDialog("Success", $"{_typeText} was created!", "OK");
                Close(false);
                EditorGUIUtility.PingObject(newFile);
            }

            if (GUILayout.Button("Cancel", GUILayout.ExpandHeight(true)))
            {
                editorWindow.Close();
            }
        }

        private void Close(bool hasError = true)
        {
            EditorUtility.ClearProgressBar();
            editorWindow.Close();
            if (hasError)
                EditorUtility.DisplayDialog("Error", $"Failed to create {_typeText}", "OK");
        }

        private bool IsTypeNameValid()
        {
            var valid = System.CodeDom.Compiler.CodeGenerator.IsValidLanguageIndependentIdentifier(_typeText);
            return valid;
        }

        private bool CreateVariable(out TextAsset newFile)
        {
            var templateName = "Templates/ScriptableVariableTemplate.cs";
            var fileName = $"{_typeText}Variable.cs";
            newFile = SoapEditorUtils.CreateNewClass(templateName, _typeText, fileName, _path);
            return newFile != null;
        }

        private bool CreateFromTemplate(string template, out TextAsset newFile)
        {
            var folderName = "Templates/";
            folderName += template;
            var fileName = template.Replace("Template", _typeText);
            newFile = SoapEditorUtils.CreateNewClass(folderName, _typeText, fileName, _path);
            return newFile != null;
        }
    }
}