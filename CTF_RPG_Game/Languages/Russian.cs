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

        public override string ToString()
        {
            return "Russian";
        }

        public static ILanguage GetLanguage()
        {
            if (This == null)
                This = new Russian();
            return This; 
        }

        #region Nothing
        public string Nothing { get { return "Ничего"; } }
        public string None { get { return "Нет"; } }
        public string UnknownCommand { get { return "Команда не опознана."; } }
        public string BadNumber { get { return "Странное число"; } }
        #endregion

        #region Auth
        public string RegistrationOrLoginText { get { return "Вы уже зарегистрированы?\nВведите \"Y\", если да, или \"N\", если нет.\n>"; } }
        public string IncorrectSymbol { get { return "Замечен недопустимый символ. Попробуйте еще раз.\n"; } }

        public string AskForRegistrationLogin { get { return "Введите желаемый логин: "; } }
        public string AskForRegistrationPassword { get { return "Придумайте пароль (попросите всех отвернуться): "; } }
        public string AskForConfirmPassword { get { return "Подтвердите пароль: "; } }
        public string PassesAreNotEqual { get { return "Пароли не совпадают, попробуйте еще раз.\n"; } }
        public string UserAlreadyExist { get { return "Пользователь с таким логином уже существует. Попробуйте еще раз.\n"; } }
        
        public string AskForLogin { get { return "Ваш логин: "; } }
        public string AskForPassword { get { return "Ваш пароль: "; } }
        public string WrongLoginOrPassword { get { return "Неправильный логин или пароль.\n"; } }

        public string ChooseName { get { return "Введите имя персонажа: "; } } 
        public string NameHasIncorrectSymbols { get { return "Имя содержит недопустимые символы. Введите другое имя."; } }
        #endregion

        #region Skills
        public string BaseActiveSkillName { get { return "Базовое активное умение"; } }
        public string BaseActiveSkillDescription { get { return "Это умение абстрактно, его нельзя изучить и использовать."; } }

        public string BasePassiveSkillName { get { return "Базовое пассивное умение"; } }
        public string BasePassiveSkillDescription { get { return "Это умение абстрактно, оно не даёт вам бонусов и его нельзя использовать."; } }

        public string SkillTeleportationName { get { return "Телепортация"; } }
        public string SkillTeleportationDescription { get { return "Это умение позволяет телепортироваться."; } }
        public string SkillTeleportationUseDescription { get { return "Введите \"use telepor <название метки>\", чтобы переместиться на эту метку."; } }
        
        public string SkillJewellerName { get { return "Ювелир"; } }
        public string SkillJewellerDescription { get { return "Позволяет вам носить второе украшение"; } }
        #endregion

        #region Items
        public string ItemBranchName { get { return "Ветка"; } }
        public string ItemBranchDescription { get { return "Простая ветка, ничего особенного."; } }
        #endregion

        #region Moving
        public string CellIsNotPassable { get { return "Невозможно переместиться сюда."; } }
        #endregion
    }
}
