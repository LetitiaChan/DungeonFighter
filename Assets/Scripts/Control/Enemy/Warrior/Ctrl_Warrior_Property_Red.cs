namespace Control
{
    public class Ctrl_Warrior_Property_Red : Ctrl_BaseEnemyProperty
    {
        public int curHeroExpenrence = 20;
        public int curATK = 10;
        public int curDEF = 3;
        public int curMaxHealth = 50;


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
