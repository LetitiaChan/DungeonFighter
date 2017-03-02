using UnityEngine;

public class AtlasData : MonoBehaviour
{
	//表示工具生成的纹理图集
	public Texture2D AtlasTexture;
	//由各纹理文件名组成的字符串数组
	public string[] TextureNames;
	//指向每个纹理坐标的指针
	public Rect[] UVs;
}
