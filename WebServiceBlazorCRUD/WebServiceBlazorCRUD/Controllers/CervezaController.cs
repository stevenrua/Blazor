using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServiceBlazorCRUD.Models;
using WebServiceBlazorCRUD.Models.Request;
using WebServiceBlazorCRUD.Models.Response;

namespace WebServiceBlazorCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CervezaController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            Respuesta oRespuesta = new Respuesta();

            try
            {
                using (BlazorCrudContext db = new BlazorCrudContext())
                {
                    var lst = db.Cervezas.ToList();
                    oRespuesta.Exito = 1;
                    oRespuesta.Data = lst;
                }
            }catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }

            return Ok(oRespuesta);
        }

        [HttpPost]
        public IActionResult Add(CervezaRequest model)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (BlazorCrudContext db = new BlazorCrudContext())
                {
                    Cerveza oCerveza = new Cerveza();
                    oCerveza.Marca = model.Marca;
                    oCerveza.Nombre = model.Nombre;
                    db.Cervezas.Add(oCerveza);
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }

        [HttpPut]
        public IActionResult Edit(CervezaRequest model)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (BlazorCrudContext db = new BlazorCrudContext())
                {
                    Cerveza oCerveza = db.Cervezas.Find(model.Id);
                    oCerveza.Marca = model.Marca;
                    oCerveza.Nombre = model.Nombre;
                    db.Entry(oCerveza).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }

        [HttpDelete("{id}")]
        public IActionResult Dele(int Id)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (BlazorCrudContext db = new BlazorCrudContext())
                {
                    Cerveza oCerveza = db.Cervezas.Find(Id);
                    db.Remove(oCerveza);
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }
    }
}
