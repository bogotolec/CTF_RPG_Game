using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTF_RPG_Game.Languages
{
    interface ILanguage
    {
        #region Auth
        string RegistrationOrLoginText { get; }
        string AskForRegistrationLogin { get; }
        string AskForRegistrationPassword { get; }
        #endregion
    }
}
