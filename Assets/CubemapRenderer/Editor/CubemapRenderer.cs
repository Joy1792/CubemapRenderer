/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson tech. Co., Ltd.
 *  FileName: CubemapRenderer.cs
 *  Author: Mogoson   Version: 1.0   Date: 8/31/2017
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.       vCubemapRenderer           Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson     8/31/2017       1.0        Build this file.
 *************************************************************************/

namespace Developer.CubemapRenderer
{
    using UnityEditor;
    using UnityEngine;

    public class CubemapRenderer : ScriptableWizard
    {
        #region Property and Field
        [Tooltip("Target render camera.")]
        public Camera renderCamera;
        #endregion

        #region Private Method
        [MenuItem("Tool/Cubemap Renderer &C")]
        private static void ShowEditor()
        {
            DisplayWizard("Cubemap Renderer", typeof(CubemapRenderer), "Render");
        }

        void OnWizardUpdate()
        {
            if (renderCamera)
                isValid = true;
            else
                isValid = false;
        }

        void OnWizardCreate()
        {
            var newRenderCubemap = new Cubemap(64, TextureFormat.ARGB32, false);
            renderCamera.RenderToCubemap(newRenderCubemap);
            var newCubemapPath = EditorUtility.SaveFilePanelInProject("Save New Render Cubemap", "NewRenderCubemap", "cubemap",
                "Enter a file name to save the new render cubemap.");
            if (newCubemapPath == string.Empty)
                return;
            AssetDatabase.CreateAsset(newRenderCubemap, newCubemapPath);
            AssetDatabase.Refresh();
        }
        #endregion
    }
}