﻿using System;
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
        public string ImpossibleCommand { get { return "Эту команду нельзя использовать сейчас."; } }
        public string TooBigPage { get { return "У вас нет столько предметов, чтобы перейти на эту страницу"; } }
        public string YourBackpackHas { get { return "Количество предметов в вашем рюкзаке"; } }
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
        #endregion

        #region Moving
        public string CellIsNotPassable { get { return "Невозможно переместиться сюда."; } }
        #endregion

        #region terms
        public string Equiped { get { return "НАДЕТО"; } }
        public string Backpacked { get { return "РЮКЗАК"; } }
        public string Page { get { return "Страница"; } }
        public string Head { get { return "Голова"; } }
        public string Body { get { return "Тело"; } }
        public string LHand { get { return "Левая рука"; } }
        public string Rhand { get { return "Правая рука"; } }
        public string Boots { get { return "Обувь"; } }
        public string Jewelerry { get { return "Драгоценность"; } }
        public string Gold { get { return "Золото"; } }
        public string Health { get { return "Здоровье"; } }
        public string Empty { get { return "Пусто"; } }
        public string Locked { get { return "Заблокировано"; } }
        #endregion

        #region Tasks
        public string AlreadySolved { get { return "Задание уже выполнено"; } }
        public string WrongFlag { get { return "Неверный флаг"; } }
        public string CorrectFlag { get { return "Правильно! Вы выполнили задание!"; } }
        public string Category { get { return "Категория"; } }
        public string GoldForSolve { get { return "Золота за решение"; } }
        #endregion
    }
}
