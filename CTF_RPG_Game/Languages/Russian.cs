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

        #region Auth
        public string RegistrationOrLoginText { get { return "Вы уже зарегистрированы?\nВведите \"Y\", если да, или \"N\", если нет.\n\n>"; } }
        public string AskForRegistrationLogin { get { return "Введите желаемый логин: "; } }
        public string AskForRegistrationPassword { get { return "Придумайте пароль (попросите всех отвернуться): "; } }
        #endregion
    }
}
