using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

internal sealed class CustomAssetImporter : AssetPostprocessor {

    #region Methods

    //Pre-processing
    private void OnPreprocessTexture()
    {
        var importer = assetImporter as TextureImporter;

        
        importer.isReadable = false;
        importer.filterMode = FilterMode.Point;
        importer.anisoLevel = 0;
        importer.textureCompression = TextureImporterCompression.Uncompressed;
    }

    #endregion
}
