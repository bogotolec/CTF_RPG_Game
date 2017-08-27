using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTF_RPG_Game.Languages
{
    class English : ILanguage
    {
        private static ILanguage This;

        private English()
        { }

        public override string ToString()
        {
            return "English";
        }

        public static ILanguage GetLanguage()
        {
            if (This == null)
                This = new English();
            return This;
        }

        #region Nothing
        public string Nothing { get { return "Nothing"; } }
        public string None { get { return "No"; } }
        public string UnknownCommand { get { return "Unknown command."; } }
        public string BadNumber { get { return "Weird number"; } }
        public string ImpossibleCommand { get { return "You cannot use this command now."; } }
        public string TooBigPage { get { return "You have not so much items to go on that page"; } }
        public string YourBackpackHas { get { return "Items count in your backpack: "; } }
        #endregion

        #region Auth
        public string RegistrationOrLoginText { get { return "Have you already registered?\nType \"Y\" or \"N\".\n>"; } }
        public string IncorrectSymbol { get { return "It contains forbidden symbol. Try again.\n"; } }

        public string AskForRegistrationLogin { get { return "Input nickname: "; } }
        public string AskForRegistrationPassword { get { return "Input password: "; } }
        public string AskForConfirmPassword { get { return "Input password again: "; } }
        public string PassesAreNotEqual { get { return "Passwords are not equal. Try again.\n"; } }
        public string UserAlreadyExist { get { return "User with same nickname has already created. Try again.\n"; } }

        public string AskForLogin { get { return "Your nickname: "; } }
        public string AskForPassword { get { return "Your password: "; } }
        public string WrongLoginOrPassword { get { return "Wrong nickname or password.\n"; } }

        public string ChooseName { get { return "Input character name: "; } }
        public string NameHasIncorrectSymbols { get { return "Name contains forbidden symbols. Input another name."; } }
        #endregion

        #region Skills
        public string BaseActiveSkillName { get { return "Base active skill"; } }
        public string BaseActiveSkillDescription { get { return "It is abstract skill. You cannot use it."; } }

        public string BasePassiveSkillName { get { return "Base passive skill"; } }
        public string BasePassiveSkillDescription { get { return "It is abstract skill. It is not give your any bonuses."; } }
        #endregion

        #region Moving
        public string CellIsNotPassable { get { return "You cannot move here."; } }
        #endregion

        #region terms
        public string Equiped { get { return "EQUIPED"; } }
        public string Backpacked { get { return "BACKPACK"; } }
        public string Page { get { return "Page"; } }
        public string Head { get { return "Head"; } }
        public string Body { get { return "Body"; } }
        public string LHand { get { return "Left hand"; } }
        public string Rhand { get { return "Right hand"; } }
        public string Boots { get { return "Boots"; } }
        public string Jewelerry { get { return "Jewelerry"; } }
        public string Gold { get { return "Gold"; } }
        public string Health { get { return "Health"; } }
        public string Empty { get { return "Empty"; } }
        public string Locked { get { return "Locked"; } }
        #endregion

        #region Tasks
        public string AlreadySolved { get { return "This task has already solved"; } }
        public string WrongFlag { get { return "Wrong flag"; } }
        public string CorrectFlag { get { return "Correct! You solved the task!"; } }
        public string Category { get { return "Category"; } }
        public string GoldForSolve { get { return "Gold for solve"; } }
        #endregion
    }
}
