using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCoreWebShell.Entities;

namespace NetCoreWebShell
{
    public class Operations
    {
        public static void Create(Input input)
        {
            using (Context db = new Context())
            {
                db.Inputs.Add(input);
                db.SaveChanges();
            }
        }

        public static Input Read(int id)
        {
            using (Context db = new Context())
            {
                foreach (Input input in db.Inputs)
                {
                    if (input.Id == id) return input;
                }
            }
            return null;
        }
    }
}
