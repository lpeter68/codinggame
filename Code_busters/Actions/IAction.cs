using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_busters
{
    public interface IAction
    {
        /// <summary>
        /// Ecrit la commande de sortie correspondante
        /// </summary>
        void Do(string message = "");
    }
}
