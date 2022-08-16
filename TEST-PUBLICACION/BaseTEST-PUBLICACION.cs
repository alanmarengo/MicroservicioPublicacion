using DATOS;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TEST_PUBLICACION
{
    public class BaseTEST_PUBLICACION
    {
        Contexto db = null;
        protected Contexto ConstruirContexto()
        {
            if (db==null) 
            {
                var opciones = new DbContextOptionsBuilder<Contexto>().UseSqlServer("Server=NTB707\\SQLEXPRESS;Database=PublicacionAPI;Uid=sa;Pwd='Emerix01';",
                options => { }).Options;
                db= new Contexto(opciones);
                return db;
            }
            return db;
        }
    }
}
