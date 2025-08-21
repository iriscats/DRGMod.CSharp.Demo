using UE4SSL.Framework;
using UE4SSL.Test.DRGSDK;



namespace UE4SSL.Test
{


    class MyMod : DRGMod
    {

        public MyMod()
        {
            OnInitRigSpaceCallback += OnInitRigSpace;
            OnInitCaveCallback += OnInitCave;
        }

        public new void Update()
        {
            base.Update();

            if (Hotkeys.IsPressed(Keys.K))
            {
                Debug.Log(LogLevel.Warning, "IsPressed K");
                TestModifyWeapon();
            }
        }

        public void OnInitCave()
        {
            Debug.Log(LogLevel.Warning, "OnInitCave");
        }


        public void OnInitRigSpace()
        {
            Debug.Log(LogLevel.Warning, "OnInitRigSpace");
        }


        public void TestModifyWeapon()
        {
            var ptr = ObjectReference.Find("/Game/WeaponsNTools/Autocannon/WPN_Autocannon.Default__WPN_Autocannon_C");
            var autoCannon = new AutoCannon(ptr!.Pointer);
            if (autoCannon is not null)
            {
                autoCannon.MaxAmmo = autoCannon.MaxAmmo * 10;
                Debug.Log(LogLevel.Warning, "" + autoCannon.MaxAmmo);

                // 查找机炮的伤害组件
                var ptr1 = ObjectReference.Find("/Game/WeaponsNTools/Autocannon/WPN_Autocannon.WPN_Autocannon_C:Damage_GEN_VARIABLE");
                // 构造 DamageComponent 类
                var damage = new DamageComponent(ptr1!.Pointer);
                // 修改数值
                damage.Damage = 100;
            }
        }


    }

    public static class Main
    {
        private static MyMod mod = new MyMod();

        public static void Update()
        {
            mod.Update();
        }

        public static void StartMod()
        {
            mod.StartMod();
        }

    }


}