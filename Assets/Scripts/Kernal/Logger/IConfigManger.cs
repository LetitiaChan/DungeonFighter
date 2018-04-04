using System.Collections.Generic;

namespace Kernal
{
    public interface IConfigManger
    {
        Dictionary<string, string> AppSetting { get; }

        int GetAppSettingMaxNumber();

    }
}
