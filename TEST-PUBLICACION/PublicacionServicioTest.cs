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
    public class PublicacionServicioTest:BaseTEST_PUBLICACION
    {
        Contexto db;
        GenericsRepository genericsRepository;
        PublicacionServicio publicacionServicio;
        Mock<IQueryPublicacion> Query;

        [SetUp]
        public void Setup()
        {
            db = ConstruirContexto();
            genericsRepository = new GenericsRepository(db);
            Query = new Mock<IQueryPublicacion>();
            Query.Setup(a => a.GetPublicaciones()).Returns(new List<Publicacion> 
            {
                new Publicacion{ID=1,ProductoID=1 },
                new Publicacion{ID=2, ProductoID=2 },
                new Publicacion{ID=3, ProductoID=3 }

            });
            publicacionServicio = new PublicacionServicio(genericsRepository, Query.Object,db);
        }


        [Test]
        public void CrearPublicacion()
        {
            using (var trans = db.Database.BeginTransaction())
            {
                var publication = publicacionServicio.CrearPublicacion(new DOMINIO.DTOS.InsertarPublicacionDto { productoID=1});
                Assert.IsNotNull(publication);
                trans.Rollback();
            }
        }

        [Test]
        public void GetPublicaciones()
        {
           var publications = publicacionServicio.GetPublicaciones();
           Assert.IsNotNull(publications);
        }




    }
}
