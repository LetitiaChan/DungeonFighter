namespace Control
{
    public class Ctrl_Warrior_Property_Green : Ctrl_BaseEnemyProperty
    {
        public int curHeroExpenrence = 5;
        public int curATK = 2;
        public int curDEF = 2;
        public int curMaxHealth = 20;


        void Start()
        {
            base.heroExpenrence = curHeroExpenrence;
            base.ATK = curATK;
            base.DEF = curDEF;
            base.maxHealth = curMaxHealth;

            base.RunMethodInChilden();
        }

    }
}