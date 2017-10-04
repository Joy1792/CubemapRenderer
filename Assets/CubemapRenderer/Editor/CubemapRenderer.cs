/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson Tech. Co., Ltd.
 *  FileName: CubemapRenderer.cs
 *  Author: Mogoson   Version: 0.1.0   Date: 8/31/2017
 *  Version Description:
 *    Internal develop version, mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.        CubemapRenderer           Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson     8/31/2017       0.1.0        Create this file.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

namespace Developer.CubemapRenderer
{
    public class CubemapRenderer : ScriptableWizard
    {
        #region Property and Field
        [Tooltip("Source camera to render Cubemap.")]
        public Camera renderCamera;

        [Tooltip("Width and height of a cube face in pixels.")]
        public int faceSize = 128;

        [Tooltip("Pixel data format to be used for the Cubemap.")]
        public TextureFormat textureFormat = TextureFormat.ARGB32;

        [Tooltip("Should mipmaps be created?")]
        public bool mipmap = false;
        #endregion

        #region Private Method
        [MenuItem("Tool/Cubemap Renderer &C")]
        private static void ShowEditor()
        {
            DisplayWizard("Cubemap Renderer", typeof(CubemapRenderer), "Render");
        }

        private void OnEnable()
        {
            renderCamera = Camera.main;
        }

        private void OnWizardUpdate()
        {
            if (renderCamera && faceSize > 0)
                isValid = true;
            else
                isValid = false;
        }

        private void OnWizardCreate()
        {
            var newRenderCubemap = new Cubemap(faceSize, textureFormat, mipmap);
            renderCamera.RenderToCubemap(newRenderCubemap);
            var newCubemapPath = EditorUtility.SaveFilePanelInProject("Save New Render Cubemap",
                "NewRenderCubemap", "cubemap", "Enter a file name to save the new render cubemap.");
            if (newCubemapPath == string.Empty)
                return;
            AssetDatabase.CreateAsset(newRenderCubemap, newCubemapPath);
            AssetDatabase.Refresh();
            Selection.activeObject = newRenderCubemap;
        }
        #endregion
    }
}