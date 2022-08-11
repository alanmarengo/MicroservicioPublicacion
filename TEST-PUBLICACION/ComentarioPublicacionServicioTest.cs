using DATOS;
using DATOS.COMANDOS;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using APLICACION.SERVICIOS;
using Moq;
using DOMINIO.QUERYS;
using DOMINIO.DTOS;
using DOMINIO.ENTIDADES;
using Microsoft.EntityFrameworkCore;

namespace TEST_PUBLICACION
{
    [TestFixture]
    public class ComentarioPublicacionServicioTest
    {
        Contexto db;
        GenericsRepository genericsRepository;
        ComentarioPublicacionServicio comentarioPublicacionServicio;
        Mock<IQueryComentarioPublicacion> Query;

        [SetUp]
        public void Setup()
        {
            db = new Contexto();
            genericsRepository = new GenericsRepository(db);
            Query = new Mock<IQueryComentarioPublicacion>();
            Query.Setup(a=>a.GetComentarioPublicaciones()).Returns(new List<ComentariosdePublicacion> 
            { 
                new ComentariosdePublicacion
                {   
                    publicacionID=1, 
                    comentarios = new List<Comentario>{ new Comentario {Comentarios="Proyecto", Fecha=DateTime.Now,Id=1 } }, 
                    Fecha=DateTime.Now 
                },
                new ComentariosdePublicacion
                { 
                    publicacionID=1, 
                    comentarios = new List<Comentario>{ new Comentario {Comentarios="Software", Fecha=DateTime.Now,Id=2 } }, 
                    Fecha=DateTime.Now 
                }
            });
            Query.Setup(a=> a.GetComentarioPublicacionesID(1)).Returns(new ComentariosdePublicacion 
            {
                comentarios = new List<Comentario> { new Comentario { Comentarios = "Proyecto", Fecha = DateTime.Now, Id = 1 } },
                Fecha=DateTime.Now,
                publicacionID=1
            });
            comentarioPublicacionServicio = new ComentarioPublicacionServicio(genericsRepository,Query.Object);
        }


        [Test]
        public void CrearComentarioPublicacion()
        {
            using (var trans = db.Database.BeginTransaction())
            {
                var comentaryPublication = comentarioPublicacionServicio.CrearComentarioPublicacion(new ComentarioPublicacionDTO 
                {
                    ComentariosID=1, PublicacionID=1 
                });
                Assert.IsNotNull(comentaryPublication);
                trans.Rollback();
            }

        }

        [Test]
        public void GetComentariodePublicaciones()
        {
            var comentariosdepublicacion = comentarioPublicacionServicio.GetComentarioPublicaciones();
            Assert.IsNotNull(comentariosdepublicacion);
        }



        [Test]
        public void GetComentarioPublicacionesID()
        {
            var comentariosdepublicacion = comentarioPublicacionServicio.GetComentarioPublicacionesID(1);
            Assert.IsNotNull(comentariosdepublicacion);
        }







    }
}
