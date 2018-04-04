using System.Collections;
using System.Collections.Generic;

namespace Kernal
{
    public class DialogDataMgr
    {
        private static DialogDataMgr _Instance;
        private static List<DialogDataFormat> _AllDialogData;
        private static List<DialogDataFormat> _CurrentDialogBuffer;
        private static int _curDialogSectionIndex;
        private static int _OriginalDialogSectionNum = 1;


        private DialogDataMgr()
        {
            _AllDialogData = new List<DialogDataFormat>();
            _CurrentDialogBuffer = new List<DialogDataFormat>();
            _curDialogSectionIndex = 0;
        }

        public static DialogDataMgr GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new DialogDataMgr();
            }
            return _Instance;
        }

        public bool LoadAllDialogData(List<DialogDataFormat> externalDialogData)
        {
            if (externalDialogData == null || externalDialogData.Count == 0) { return false; }

            if (_AllDialogData != null && _AllDialogData.Count == 0)
            {
                for (int i = 0; i < externalDialogData.Count; i++)
                {
                    _AllDialogData.Add(externalDialogData[i]);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public DialogDataFormat GetNextDialogRecord(int diaSectionNum)
        {
            DialogDataFormat defaultResult = new DialogDataFormat();
            defaultResult.DialogSide = DialogSide.None;
            defaultResult.DialogPerson = "[GetNextDialogRecord()/personName]";
            defaultResult.DialogContent = "[GetNextDialogRecord()/dialogContent]";
            defaultResult.DialogButton = false;
            if (diaSectionNum < 0) { return defaultResult; }


            if (diaSectionNum != _OriginalDialogSectionNum)
            {
                _curDialogSectionIndex = 0;
                _CurrentDialogBuffer.Clear();
                _OriginalDialogSectionNum = diaSectionNum;
            }

            if (_CurrentDialogBuffer != null && _CurrentDialogBuffer.Count >= 1)
            {
                if (_curDialogSectionIndex < _CurrentDialogBuffer.Count)
                {
                    ++_curDialogSectionIndex;
                }
                else
                {
                    return defaultResult;
                }
            }
            else
            {
                ++_curDialogSectionIndex;
            }
            return GetDialogRecord(diaSectionNum);
        }

        /// <summary>
        /// 得到对话信息
        /// 开发思路：
        ///     对于输入的“段落编号”，首先在“当前对话数据集合”中进行查询，
        ///     如果找到，直接返回结果。如果不能找到则在“全部对话数据集合”中进行查询。
        ///     ,且把当前段落数据，加入当前的缓存集合
        /// </summary>
        private DialogDataFormat GetDialogRecord(int diaSectionNum)
        {
            DialogDataFormat defaultResult = new DialogDataFormat();
            defaultResult.DialogSide = DialogSide.None;
            defaultResult.DialogPerson = "[GetDialogRecord()/personName]";
            defaultResult.DialogContent = "[GetDialogRecord()/dialogContent]";
            defaultResult.DialogButton = false;

            if (diaSectionNum <= 0) { return defaultResult; }

            //Log.Write(GetType() + "/GetDialogRecord()/  ------ 1: 在当前缓存集合中查询    --------");
            DialogDataFormat searchResult = SearchFromDialogData(_CurrentDialogBuffer, diaSectionNum);
            if (searchResult != null) return searchResult;


            //Log.Write(GetType() + "/GetDialogRecord()/  ------ 2: 在全部记录集合中查询    --------");
            searchResult = SearchFromDialogData(_AllDialogData, diaSectionNum);
            if (searchResult != null)
            {
                AddToCurrentDialogBufferBySectionNum(diaSectionNum);
                return searchResult;
            }

            return defaultResult;
        }

        private DialogDataFormat SearchFromDialogData(List<DialogDataFormat> dialogData, int diaSectionNum)
        {
            DialogDataFormat result = null;

            if (dialogData != null && dialogData.Count > 0)
            {
                foreach (var item in dialogData)
                {
                    if (item.DialogSecNum == diaSectionNum && item.SectionIndex == _curDialogSectionIndex)
                    {
                        result = new DialogDataFormat();
                        result.DialogSecNum = item.DialogSecNum;
                        result.DialogSecName = item.DialogSecName;
                        result.DialogSide = item.DialogSide;
                        result.DialogPerson = item.DialogPerson;
                        result.DialogContent = item.DialogContent;
                        result.DialogButton = item.DialogButton;
                    }
                }
            }
            return result;
        }

        private bool AddToCurrentDialogBufferBySectionNum(int sectionNum)
        {
            if (sectionNum <= 0) { return false; }

            if (_AllDialogData != null && _AllDialogData.Count >= 1)
            {
                _CurrentDialogBuffer.Clear();
                foreach (var item in _AllDialogData)
                {
                    if (item.DialogSecNum == sectionNum)
                    {
                        _CurrentDialogBuffer.Add(item);
                    }
                }
                return true;
            }
            return false;
        }

    }
}