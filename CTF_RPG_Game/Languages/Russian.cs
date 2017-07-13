using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTF_RPG_Game.Languages
{
    class Russian : ILanguage
    {
        private static ILanguage This;

        private Russian()
        { }

        public static ILanguage GetLanguage()
        {
            if (This == null)
                This = new Russian();
            return This; 
        }

        #region Nothing
        public string Nothing { get { return "Ничего"; } }
        public string None { get { return "Нет"; } }
        #endregion

        #region Auth
        public string RegistrationOrLoginText { get { return "Вы уже зарегистрированы?\nВведите \"Y\", если да, или \"N\", если нет.\n\n>"; } }
        public string AskForRegistrationLogin { get { return "Введите желаемый логин: "; } }
        public string AskForRegistrationPassword { get { return "Придумайте пароль (попросите всех отвернуться): "; } }
        #endregion

        #region Skills
        public string BaseActiveSkillName { get { return "Базовое активное умение"; } }
        public string BaseActiveSkillDescription { get { return "Это умение абстрактно, его нельзя изучить и использовать."; } }

        public string BasePassiveSkillName { get { return "Базовое пассивное умение"; } }
        public string BasePassiveSkillDescription { get { return "Это умение абстрактно, оно не даёт вам бонусов и его нельзя использовать."; } }

        public string SkillTeleportationName { get { return "Телепортация"; } }
        public string SkillTeleportationDescription { get { return "Это умение позволяет телепортироваться."; } }
        public string SkillTeleportationUseDescription { get { return "Введите \"use telepor <название метки>\", чтобы переместиться на эту метку."; } }
        #endregion

        #region Items
        public string ItemBranchName { get { return "Ветка"; } }
        public string ItemBranchDescription { get { return "Простая ветка, ничего особенного."; } }
        #endregion
    }
}
