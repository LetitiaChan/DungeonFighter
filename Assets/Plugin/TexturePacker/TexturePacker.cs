#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Collections;

public class TexturePacker : ScriptableWizard
{
    //指向一系列纹理的指针
    public Texture2D[] Textures;

    //纹理图集的名字
    public string AtlasName = "Atlas_Texture";
    //加入到纹理图集中的纹理之间的间隔
    public int Padding = 4;

    [MenuItem("MyPlugin/Atlas Texture")]
    static void CreateWizard()
    {
        ScriptableWizard.DisplayWizard("Create Atlas", typeof(TexturePacker));
    }
    void OnEnable()
    {
        //创建一个新的纹理列表
        List<Texture2D> TextureList = new List<Texture2D>();

        //Loop through objects selected in editor
        if (Selection.objects != null && Selection.objects.Length > 0)
        {
            Object[] objects = EditorUtility.CollectDependencies(Selection.objects);
            foreach (Object o in objects)
            {
                //Get selected object as texture
                Texture2D tex = o as Texture2D;
                //Is texture asset?
                if (tex != null)
                {
                    //Add to list
                    TextureList.Add(tex);
                }
            }
        }
        //Check count. If >0, then create array
        if (TextureList.Count > 0)
        {
            Textures = new Texture2D[TextureList.Count];
            for (int i = 0; i < TextureList.Count; i++)
            {
                Textures[i] = TextureList[i];
            }
        }
    }
    void OnWizardCreate()
    {
        GenerateAtlas();
    }
    public void ConfigureForAtlas(string TexturePath)
    {
        //1--获取指定路径下的纹理
        TextureImporter TexImport = AssetImporter.GetAtPath(TexturePath) as TextureImporter;
        TexImport.textureType = TextureImporterType.Default;
        //2--修改此纹理的设置
        TextureImporterSettings tiSettings = new TextureImporterSettings();
        TexImport.ReadTextureSettings(tiSettings);
        tiSettings.mipmapEnabled = false;
        tiSettings.readable = true;
        tiSettings.maxTextureSize = 4096;
        tiSettings.textureFormat = TextureImporterFormat.ARGB32;
        tiSettings.filterMode = FilterMode.Point;
        tiSettings.wrapMode = TextureWrapMode.Clamp;
        tiSettings.npotScale = TextureImporterNPOTScale.None;
        TexImport.SetTextureSettings(tiSettings);
        //3--重新把纹理导入到Unity中
        AssetDatabase.ImportAsset(TexturePath, ImportAssetOptions.ForceUpdate);
        AssetDatabase.Refresh();
    }
    //生成纹理图集的函数
    public void GenerateAtlas()
    {
        //生成图集对象
        GameObject AtlasObject = new GameObject("obj_" + AtlasName);
        AtlasData AtlasComp = AtlasObject.AddComponent<AtlasData>();
        AtlasComp.TextureNames = new string[Textures.Length];
        //使用循环配置每一个要加入到图集的纹理
        for (int i = 0; i < Textures.Length; i++)
        {
            //获取纹理在资源中的路径
            string TexturePath = AssetDatabase.GetAssetPath(Textures[i]);
            //修改纹理的设置信息
            ConfigureForAtlas(TexturePath);
            //将所有纹理的名字都加入到列表中
            AtlasComp.TextureNames[i] = TexturePath;
        }


        //生成纹理图集
        Texture2D tex = new Texture2D(1, 1, TextureFormat.ARGB32, false);
        //PackTextures()用于打包多个纹理为一个纹理
        AtlasComp.UVs = tex.PackTextures(Textures, Padding, 4096);
        //生成唯一的资源路径
        string AssetPath = AssetDatabase.GenerateUniqueAssetPath("Assets/" + AtlasName + ".png");
        //把纹理图集保存成文件
        byte[] bytes = tex.EncodeToPNG();
        System.IO.File.WriteAllBytes(AssetPath, bytes);
        bytes = null;
        //删除生成的纹理图集
        UnityEngine.Object.DestroyImmediate(tex);
        //导入纹理资源
        AssetDatabase.ImportAsset(AssetPath);
        //获取导入的纹理
        AtlasComp.AtlasTexture = AssetDatabase.LoadAssetAtPath(AssetPath, typeof(Texture2D)) as Texture2D;
        //配置纹理图集
        ConfigureForAtlas(AssetDatabase.GetAssetPath(AtlasComp.AtlasTexture));

        AssetPath = AssetDatabase.GenerateUniqueAssetPath("Assets/atlasdata_" + AtlasName + ".prefab");
        //创建预置对象
        Object prefab = PrefabUtility.CreateEmptyPrefab(AssetPath);
        //更新、保存预置对象
        PrefabUtility.ReplacePrefab(AtlasObject, prefab, ReplacePrefabOptions.ConnectToPrefab);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        //销毁原来的对象
        DestroyImmediate(AtlasObject);
    }
}
#endif