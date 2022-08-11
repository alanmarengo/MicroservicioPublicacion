using APLICACION.SERVICIOS;
using DATOS;
using DATOS.COMANDOS;
using DOMINIO.ENTIDADES;
using DOMINIO.QUERYS;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TEST_PUBLICACION
{
    [TestFixture]
    public class ComentarioServicioTest
    {
        Contexto db;
        GenericsRepository genericsRepository;
        ComentarioServicio comentarioServicio;
        Mock<IQueryComentario> Query;

        [SetUp]
        public void Setup()
        {
            db = new Contexto();
            genericsRepository = new GenericsRepository(db);
            Query = new Mock<IQueryComentario>();
            Query.Setup(a => a.GetComentario()).Returns(
                new List<Comentario> 
                { 
                    new Comentario{Id=1, Comentarios="PROYECTO SOFTWARE 1",Fecha=DateTime.Now },
                    new Comentario{Id=1, Comentarios="PROYECTO SOFTWARE 2",Fecha=DateTime.Now },
                    new Comentario{Id=1, Comentarios="PROYECTO SOFTWARE 3",Fecha=DateTime.Now }
                });
            comentarioServicio = new ComentarioServicio(genericsRepository, Query.Object);
        }


        [Test]
        public void CrearComentario()
        {
            using (var trans = db.Database.BeginTransaction())
            {
                var comentary = comentarioServicio.CrearComentario("Proyecto Software");
                Assert.IsNotNull(comentary);
                trans.Rollback();
            }

        }

        [Test]
        public void GetComentarios()
        {
                var comentarys = comentarioServicio.GetComentario();
                Assert.IsNotNull(comentarys);        
        }

    }
}
