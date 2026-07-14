using UnityEditor;
using UnityEditor.SceneManagement;

namespace Minigames.Survivor.Editor
{
    public static class PlayModeSceneSetter
    {
        [MenuItem("Tools/PlayModeSceneSetter/Set StartScene as first")]
        private static void SetStartScene()
        {
            var sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>("Assets/Scenes/StartScene.unity");
            EditorSceneManager.playModeStartScene = sceneAsset;
        }

        [MenuItem("Tools/PlayModeSceneSetter/Reset")]
        private static void Reset()
        {
            EditorSceneManager.playModeStartScene = null;
        }
    }
}
