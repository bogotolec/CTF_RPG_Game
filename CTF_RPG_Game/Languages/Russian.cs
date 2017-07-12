using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTF_RPG_Game.Languages
{
    class Russian : ILanguage
    {
        public string RegistrationOrLoginText { get { return "Вы уже зарегистрированы?\nВведите \"Y\", если да, или \"N\", если нет."; } }
    }
}
