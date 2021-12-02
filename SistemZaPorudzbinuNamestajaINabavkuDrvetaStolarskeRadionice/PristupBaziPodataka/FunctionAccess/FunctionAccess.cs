using BazaPodataka;
using BazaPodataka.Model.Vise_ViseEntiteti;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PristupBaziPodataka
{
    public class FunctionAccess
    {
        public List<ReturnTable> PronadjiPonude(int stolarskaRadionicaId)
        {
            using(var context = new DatabaseContext())
            {
                return context.Database.SqlQuery<ReturnTable>(
                    "select * from pronadji_ponude(@srId)",
                    new SqlParameter("@srId", stolarskaRadionicaId)).ToList();
            }
        }
    }
}
