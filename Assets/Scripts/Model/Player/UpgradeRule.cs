using Global;

namespace Model
{
    public class UpgradeRule
    {
        private static UpgradeRule _instance;

        private UpgradeRule() { }

        public static UpgradeRule GetInstance()
        {
            if (_instance == null)
            {
                _instance = new UpgradeRule();
            }
            return _instance;
        }

        public void UpgradeCondition(int exp)
        {
            int currentLevel = 0;
            currentLevel = PlayerExternalDataProxy.GetInstance().GetLevel();

            bool reachLv1 = exp >= 100 && exp < 200 && currentLevel == 0;
            bool reachLv2 = exp >= 200 && exp < 300 && currentLevel == 1;
            bool reachLv3 = exp >= 300 && exp < 500 && currentLevel == 2;
            bool reachLv4 = exp >= 500 && exp < 700 && currentLevel == 3;
            bool reachLv5 = exp >= 700 && exp < 1000 && currentLevel == 4;
            bool reachLv6 = exp >= 1000 && exp < 3000 && currentLevel == 5;
            if (reachLv1 || reachLv2 || reachLv3 || reachLv4 || reachLv5 || reachLv6)
            {
                PlayerExternalDataProxy.GetInstance().AddLevel();
            }
        }

        public void UpgradeOperation(LevelName lvName)
        {
            switch (lvName)
            {
                case LevelName.Level_0:
                    UpgradeRuleOperation(0, 0, 20, 1, 10);
                    break;
                case LevelName.Level_1:
                    UpgradeRuleOperation(100, 100, 40, 1, 10);
                    break;
                case LevelName.Level_2:
                    UpgradeRuleOperation(200, 100, 40, 1, 10);
                    break;
                case LevelName.Level_3:
                    UpgradeRuleOperation(200, 100, 50, 1, 10);
                    break;
                case LevelName.Level_4:
                    UpgradeRuleOperation(500, 100, 100, 1, 10);
                    break;
                case LevelName.Level_5:
                    UpgradeRuleOperation(500, 100, 100, 1, 10);
                    break;
                case LevelName.Level_6:
                    UpgradeRuleOperation(500, 100, 0, 1, 10);
                    break;
                case LevelName.Level_7:
                    UpgradeRuleOperation(500, 100, 0, 1, 10);
                    break;
                case LevelName.Level_8:
                    UpgradeRuleOperation(500, 100, 0, 1, 10);
                    break;
                case LevelName.Level_9:
                    UpgradeRuleOperation(500, 100, 0, 1, 10);
                    break;
                case LevelName.Level_10:
                    UpgradeRuleOperation(500, 100, 0, 1, 10);
                    break;
                default:
                    break;
            }
        }

        public void UpgradeRuleOperation(float maxhp, float maxmp, float maxATK, float maxDEF, float maxDEX)
        {
            //处理核心最大数值增加
            PlayerKernalDataProxy.GetInstance().IncreaseMaxHealth(maxhp);
            PlayerKernalDataProxy.GetInstance().IncreaseMaxMagic(maxmp);
            PlayerKernalDataProxy.GetInstance().IncreaseMaxATK(maxATK);
            PlayerKernalDataProxy.GetInstance().IncreaseMaxDEF(maxDEF);
            PlayerKernalDataProxy.GetInstance().IncreaseMaxDEX(maxDEX);

            //hp mp加满为最大数值
            PlayerKernalDataProxy.GetInstance().IncreaseHealthValues(PlayerKernalDataProxy.GetInstance().GetMaxHealth());
            PlayerKernalDataProxy.GetInstance().IncreaseMagicValues(PlayerKernalDataProxy.GetInstance().GetMaxMagic());
        }
    }
}