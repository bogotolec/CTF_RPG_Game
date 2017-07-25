using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTF_RPG_Game.Languages
{
    interface ILanguage
    {
        #region Other
        string Nothing { get; }
        string None { get; }
        string UnknownCommand { get; }
        string BadNumber { get; }
        string ImpossibleCommand { get; }
        #endregion

        #region Auth
        string RegistrationOrLoginText { get; }
        string IncorrectSymbol { get; }

        string AskForRegistrationLogin { get; }
        string AskForRegistrationPassword { get; }
        string AskForConfirmPassword { get; }
        string PassesAreNotEqual { get; }
        string UserAlreadyExist { get; }

        string AskForLogin { get; }
        string AskForPassword { get; }
        string WrongLoginOrPassword { get; }

        string ChooseName { get; }
        string NameHasIncorrectSymbols { get; }
        #endregion

        #region Skills
        string BaseActiveSkillName { get; }
        string BaseActiveSkillDescription { get; }

        string BasePassiveSkillName { get; }
        string BasePassiveSkillDescription { get; }

        string SkillTeleportationName { get; }
        string SkillTeleportationDescription { get; }
        string SkillTeleportationUseDescription { get; }

        string SkillJewellerName { get; }
        string SkillJewellerDescription { get; }
        #endregion

        #region Items
        string ItemBranchName { get; }
        string ItemBranchDescription { get; }
        #endregion

        #region Moving
        string CellIsNotPassable { get; }
        #endregion

        #region terms
        string Equiped { get; }
        string Backpacked { get; }
        string Page { get; }
        string Head { get; }
        string Body { get; }
        string LHand { get; }
        string Rhand { get; }
        string Boots { get; }
        string Jewelerry { get; }
        string Gold { get; }
        string Health { get; }
        string Empty { get; }
        string Locked { get; }
        #endregion
    }
}
