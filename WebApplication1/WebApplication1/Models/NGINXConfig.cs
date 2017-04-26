using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.DataAccess;

namespace WebApplication1.Models
{
    public class NGINXConfig
    {
      
        public NGINXConfig(){ }

        public string GenerateConfig(AllRepositories allRep)
        {
            var strb = new StringBuilder();

            foreach (var up in allRep.UpstreamRep.GetAll())
            {
                strb.AppendLine(up.GenerateConfig(allRep));
            }

            return strb.ToString();
        }

    }
}
