using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Barnabus.CharacterAsset.EDITOR
{
    public class InfoEditWindow : EditorWindow
    {
        private Editor editor;

        [MenuItem("Tools/Character Information Editor")]
        public static void ShowWindow()
        {
            /*
            // This method is called when the user selects the menu item in the Editor
            InfoEditWindow window = GetWindow<InfoEditWindow>(false, "Character Info", true);

            window.editor = Editor.CreateEditor(CreateInstance<InfoEditor>());*/
        }

        public void CreateGUI()
        {
            //editor.OnInspectorGUI();
            /*
            var splitView = new TwoPaneSplitView(0, 250, TwoPaneSplitViewOrientation.Horizontal);

            // Add the view to the visual tree by adding it as a child to the root element
            rootVisualElement.Add(splitView);


            // A TwoPaneSplitView always needs exactly two child elements
            var leftPane = new VisualElement();
            splitView.Add(leftPane);
            var rightPane = new VisualElement();
            splitView.Add(rightPane);


            var leftPane = new ListView();
            splitView.Add(leftPane);

            leftPane.makeItem = () => new Label();
            leftPane.bindItem = (item, index) => { (item as Label).text = allObjects[index].name; };
            leftPane.itemsSource = allObjects; */
        }
    }
}